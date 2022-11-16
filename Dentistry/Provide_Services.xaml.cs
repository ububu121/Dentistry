using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dentistry
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Provide_Services : Window
    {
        private Записи_На_Прием запись = null;
        string str;
        string[] FIO = new string[3];
        DateTime? date = new DateTime();
        int IdDiag, IdDoc, IdCard, IdRec, IdServ, IdPos;
        string Fname, Lname, Patronymic;
        public Provide_Services(Записи_На_Прием запись)
        {
            InitializeComponent();

            if (запись != null)
            {
                this.запись = запись;
                str = запись.Фамилия + " " + запись.Имя + " " + запись.Отчество;
                date = запись.Дата_Приема;
                lblFIO.Content = str;
                IdDoc = (int)(запись.Код_Врача);
                Fname = запись.Фамилия;
                Lname = запись.Имя;
                Patronymic = запись.Отчество;
                IdCard = (int)запись.Номер_Карты;
            }
            FillCmbDiagnoz();
            FillCmbServices();
        }


        private void FillCmbDiagnoz()
        {
            List<string> lstDiagn = new List<string>();
            var diagnoz = Instances.db.Диагноз.OrderBy(q=>q.Диагноз1).ToList();
            
            foreach(var diagn in diagnoz)
            {
                lstDiagn.Add(diagn.Диагноз1);
            }
            cmbDiagnoz.ItemsSource = lstDiagn;
            cmbDiagnoz.SelectedIndex = 0;
        }

        private void FillCmbServices()
        {
            List<string> lstServices = new List<string>();
            var services = Instances.db.Услуги.OrderBy(q => q.Наименование_Услуги).ToList();

            foreach (var serv in services)
            {
                lstServices.Add(serv.Наименование_Услуги);
            }
            cmbServices.ItemsSource = lstServices;
            cmbServices.SelectedIndex = 0;
        }

        private void Btn_Card_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Cards().ShowDialog();
            Show();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {

            var diagnoz = cmbDiagnoz.SelectedValue;
            var yslyga = cmbServices.SelectedValue;
            string Description = new TextRange(txt_Description.Document.ContentStart, txt_Description.Document.ContentEnd).Text;

            var aaa = Instances.db.Карта.FirstOrDefault(q => q.Фамилия == Fname && q.Имя == Lname && q.Отчество == Patronymic);
            aaa.Диагноз = (string)diagnoz;
            aaa.Описание_приема = Description;
            aaa.Предоставленные_услуги = (string)yslyga;

            //Записи_На_Прием запись = Instances.db.Записи_На_Прием.FirstOrDefault(q => q.Фамилия == Fname && q.Имя == Lname && q.Отчество == Patronymic);
            //Instances.db.Записи_На_Прием.Remove(запись);

            Приемы прием = new Приемы();
            прием.Код_Врача = IdDoc;
            прием.Дата_Приема = date;
            Instances.db.Приемы.Add(прием);

            Instances.db.SaveChanges();

            IdRec = прием.Код_записи;

            var diag = Instances.db.Диагноз.FirstOrDefault(q => q.Диагноз1 == (string)diagnoz);
            var ysl = Instances.db.Услуги.FirstOrDefault(q => q.Наименование_Услуги == (string)yslyga);
            IdDiag = diag.Код_диагноза;
            IdServ = ysl.Код_Услуги;

            Посещения посещение = new Посещения();
            посещение.Код_Диагноза = IdDiag;
            посещение.Код_Карты = IdCard;
            посещение.Код_Записи = IdRec;
            посещение.Код_Услуги = IdServ;
            Instances.db.Посещения.Add(посещение);

            Instances.db.SaveChanges();

            var pos = Instances.db.Посещения.FirstOrDefault(q => q.Код_Карты == IdCard);
            IdPos = pos.Код_Посещения;
            Оказанные_Услуги оказанные = new Оказанные_Услуги();
            оказанные.Код_Услуги = IdServ;
            оказанные.Код_Посещения = IdPos;
            Instances.db.Оказанные_Услуги.Add(оказанные);

            Instances.db.SaveChanges();

            MessageBox.Show("Данные о приеме сохранены!");
            Close();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
