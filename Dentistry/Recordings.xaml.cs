using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Recordings.xaml
    /// </summary>
    public partial class Recordings : Window
    {

        private Пользователи user = null;
        int Id_vr;
        public Recordings(Пользователи user)
        {
            InitializeComponent();

            if (user != null)
            {
                this.user = user;
                var query = Instances.db.Врачи.Where(p => p.Фамилия == user.Фамилия_Пользователя && p.Имя == user.Имя_Пользователя && p.Отчество == user.Отчество_Пользователя).ToList();
                foreach(var a in query)
                {
                    Id_vr = Convert.ToInt32(a.Код_врача);
                }
            }
            FillData();
        }

        private void FillData()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Фамилия + ' ' + Имя + ' ' + Отчество AS ФИО, Дата_Приема, Время_Приема, Код_Записи FROM Записи_На_Прием WHERE Записи_На_Прием.Код_Врача IN ('" + Id_vr + "') ";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Записи");
                sda.Fill(dt);
                if (txtSearch.Text.Length != 0)
                {
                    dt.DefaultView.RowFilter = string.Format("ФИО LIKE '%{0}%'", txtSearch.Text);
                }
                listViewRecordings.ItemsSource = dt.DefaultView;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(sender as Button).Tag;
            Записи_На_Прием запись = Instances.db.Записи_На_Прием.FirstOrDefault(q => q.Код_Записи == id);
            Instances.db.Записи_На_Прием.Remove(запись);
            Instances.db.SaveChanges();
            FillData();
            MessageBox.Show("Запись удалена");
        }

        private void Cb_priem_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as CheckBox).Tag;
            Записи_На_Прием запись;

            if ((запись = Instances.db.Записи_На_Прием.FirstOrDefault(q => q.Код_Записи == id)) != null)
            {
                Hide();
                new Provide_Services(запись).ShowDialog();
                Show();
                Close();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так!");
            }

        }
    }
}
