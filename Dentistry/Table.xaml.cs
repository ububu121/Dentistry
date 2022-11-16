using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dentistry
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        string FIO;
        public Table()
        {
            InitializeComponent();
            FillData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void FillData()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Врачи.Специальность, (Врачи.Фамилия + ' ' + Врачи.Имя + ' ' + Врачи.Отчество) AS ФИО, Расписание.Кабинет, Расписание.Пн FROM Врачи INNER JOIN Расписание ON Врачи.Код_врача = Расписание.Код_Врача";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Расписание");
                sda.Fill(dt);
                lstTable.ItemsSource = dt.DefaultView;
            }
        }

        private void LstTable_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)lstTable.SelectedItem;
            if (drv != null)
            {
                FIO = drv["ФИО"].ToString();
                btnEdit.IsEnabled = true;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new EditTable(FIO).ShowDialog();
            Show();
            FillData();
        }
    }
}
