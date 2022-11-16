using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
            FillData();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void FillData()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Роли.Роль, Пользователи.Фамилия_Пользователя +' '+ Пользователи.Имя_Пользователя+ ' '+ Пользователи.Отчество_Пользователя AS ФИО, Пользователи.Логин_Пользователя, Пользователи.Пароль_Пользователя FROM Пользователи INNER JOIN Роли ON Пользователи.Роль_Пользователя = Роли.Код_Роли";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Пользователи");
                sda.Fill(dt);
                if (txtSearch.Text.Length != 0)
                {
                    dt.DefaultView.RowFilter = string.Format("ФИО LIKE '%{0}%'", txtSearch.Text);
                }
                lstUsers.ItemsSource = dt.DefaultView;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new NewUser().ShowDialog();
            Show();
        }
    }
}
