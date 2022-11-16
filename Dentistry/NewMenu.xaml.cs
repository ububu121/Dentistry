using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для NewMenu.xaml
    /// </summary>
    public partial class NewMenu : Window
    {
        public ObservableCollection<ButtonViewModel> Buttons { get; set; } = new ObservableCollection<ButtonViewModel>();
        public NewMenu()
        {
            InitializeComponent();
            DataContext = this;

            FillCmb();
            FillData();
        }
        
        List<string> timeBlocks = new List<string> { };
        string selectedTime;
        string dayOfWeek;
        string value1;
        string[] FIO = new string [3];
        string kab;
        int codeDoctor;
        DateTime one;
        DateTime two;
        string updatedDate;
        int id_card;


        private void FillCmb()                                                                                      //заполнение специальностей
        {
            List<string> lstSpec = new List<string>();
            var specialnost = Instances.db.Врачи.ToList();

            foreach (var sp in specialnost)
            {
                lstSpec.Add(sp.Специальность);
            }
            var unique = lstSpec.Distinct();
            cmbSpec.ItemsSource = unique;
            cmbSpec.SelectedIndex = 0;
        }

        private void FillData()                                                                                  //заполнение списка врачей
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            var a = cmbSpec.SelectedItem.ToString();
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT (Врачи.Фамилия + ' ' + Врачи.Имя + ' ' + Врачи.Отчество) AS ФИО, Врачи.Код_врача FROM Врачи WHERE Специальность IN ('" + a + "'); ";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Расписание");
                sda.Fill(dt);
                sda.Dispose();
                lstDoctors.DisplayMemberPath = "ФИО";
                //string code = string.Join(Environment.NewLine, dt.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
                //var cods = code.Split(new char[] { ';'}, StringSplitOptions.RemoveEmptyEntries);
                //codeDoctor = Convert.ToInt32(cods[5].ToString());
                lstDoctors.ItemsSource = dt.DefaultView;
            }
        }

        
        private void LstDoctors_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)                 //выбор врача в списке
        {
            dtPic.IsEnabled = true;
            label4.IsEnabled = true;
            DataRowView drv = (DataRowView)lstDoctors.SelectedItem;
            if (drv != null)
            {
                value1 = drv["ФИО"].ToString();
                FIO = value1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Buttons.Clear();
                codeDoctor = Convert.ToInt32(drv["Код_врача"]);
            }
            
            
        }
        
        private void FillRaspisanie()                                                                           //получение данных о расписании выбранного врача
        {
            Pacient();
            Buttons.Clear();
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Расписание. , Расписание.Кабинет FROM Врачи INNER JOIN Расписание ON Врачи.Код_врача = Расписание.Код_Врача WHERE Врачи.Фамилия IN ('"+ FIO[0] + "') And Врачи.Имя IN ('" + FIO[1] + "') And Врачи.Отчество IN ('" + FIO[2] + "')  ; ";
                CmdString = CmdString.Insert(18,dayOfWeek);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Квадраты");
                sda.Fill(dt);
                sda.Dispose();
                var rres = string.Join(Environment.NewLine, dt.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
                var cods = rres.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string res = Convert.ToString(cods[0].ToString());
                kab = Convert.ToString(cods[1].ToString());

                timeBlocks.Clear();
                if (res != "Выходной" && res!= "Выходной ")
                {
                    string[] times = res.Split('-');
                    timeBlocks.Add(times[0]);
                    try
                    {
                        one = DateTime.Parse(times[0]);
                        two = DateTime.Parse(times[1]);
                        while (one < two)
                        {
                            one = one.AddMinutes(30);
                            timeBlocks.Add(one.ToShortTimeString());
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Выберите врача");
                    }
                }
                else
                {
                    Buttons.Clear();
                    MessageBox.Show("В этот день нет записи, врач выходной");
                }
                if (timeBlocks.Any()) timeBlocks.RemoveAt(timeBlocks.Count - 1);
                DellTime();
                FillTime();
            }
        }

        private void FillTime()                                                             //заполнение кнопок
        {
            
            int a = 0, b = 0;
            for (int i = 0; i < timeBlocks.Count; i++)
            {
                if (b == 4) { a++; b = 0; }
                Buttons.Add(new ButtonViewModel(timeBlocks[i], a, b));
                b++;
            }
        }


        private void DtPic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)                          //выбираем день, получаем день недели
        {
            DateTime? selectedDate = dtPic.SelectedDate;
            if (selectedDate.HasValue)
            {
                dayOfWeek = selectedDate.Value.ToString("ddd");
                updatedDate = selectedDate.Value.ToString("d");
            }
            FillRaspisanie();
            
        }

        private void CmbSpec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillData();
            Buttons.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = (sender as Button);
            var query = (sender as Button).DataContext as ButtonViewModel;
            selectedTime = query.Content;
            Buttons.Clear();
            if (!timeBlocks.Any()) { MessageBox.Show("Свободного времени нет, выберите другой день!"); }
            FillTime();
        }
        private void DellTime()
        {
            var zapisi = Instances.db.Записи_На_Прием.ToList();
            foreach(var time in zapisi)
            {
                if ((time.Дата_Приема.ToString("dd.MM.yyyy")) == updatedDate && (timeBlocks.Contains(time.Время_Приема)) && time.Код_Врача == codeDoctor){
                    timeBlocks.Remove(time.Время_Приема);
                }
            }
        }
        
        private void Pacient()
        {
            var query = Instances.db.Карта.Where(p => p.Фамилия == txtFName.Text && p.Имя == txtLName.Text && p.Отчество == txtPatronymic.Text).ToList();
            if (query != null)
            {
                foreach (var qr in query)
                {
                    id_card = qr.Код_Карты;
                }
            }
            else MessageBox.Show("Карта не заведена");
        }

        private void Zapisb_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "" && txtPatronymic.Text != "")
            {
                DateTime updDate = (DateTime.Parse(updatedDate)).Date;
                Записи_На_Прием запись = new Записи_На_Прием();
                запись.Фамилия = txtFName.Text;
                запись.Имя = txtLName.Text;
                запись.Отчество = txtPatronymic.Text;
                запись.Время_Приема = selectedTime;
                запись.Дата_Приема = updDate;
                запись.Код_Врача = codeDoctor;
                запись.Кабинет = Convert.ToInt32(kab);
                if (id_card != 0) { запись.Номер_Карты = id_card; }
                Instances.db.Записи_На_Прием.Add(запись);
                Instances.db.SaveChanges();
                DellTime();
                FillRaspisanie();
                MessageBox.Show("Запись на прием успешно произведена");
            }
            else MessageBox.Show("Заполните все поля!");
            
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Раздел в разработке..");
        }

        private void TxtFName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < 'а' || inp > 'я')
                if (inp < 'А' || inp > 'Я')
                    e.Handled = true;
        }

        private void TxtLName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < 'а' || inp > 'я')
                if (inp < 'А' || inp > 'Я')
                    e.Handled = true;
        }

        private void TxtPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < 'а' || inp > 'я')
                if (inp < 'А' || inp > 'Я')
                    e.Handled = true;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
