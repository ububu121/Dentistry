using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EditTable.xaml
    /// </summary>
    public partial class EditTable : Window
    {
        string[] Fio = new string[3];
        string Fname, Lname, Patronymic;
        int idDoc;

        public EditTable(string FIO)
        {
            InitializeComponent();
            if (FIO != null)
            {
                this.txtFIO.Text = FIO;
                Fio = FIO.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Fname = Fio[0];
                Lname = Fio[1];
                Patronymic = Fio[2];
                FillAll();
            }
        }

        private void FillAll()
        {
            var aaa = Instances.db.Врачи.FirstOrDefault(q => q.Фамилия == Fname && q.Имя == Lname && q.Отчество == Patronymic);
            idDoc = aaa.Код_врача;
            var bbb = Instances.db.Расписание.FirstOrDefault(q => q.Код_Врача == idDoc);
            txtSpec.Text = aaa.Специальность;
            txtKab.Text = bbb.Кабинет;
            txtTime.Text = bbb.Пн;
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var bbb = Instances.db.Расписание.FirstOrDefault(q => q.Код_Врача == idDoc);
            bbb.Кабинет = txtKab.Text;
            bbb.Пн = txtTime.Text;
            Instances.db.SaveChanges();
            MessageBox.Show("Изменения сохранены");
        }
    }
}
