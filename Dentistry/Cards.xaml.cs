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
    /// Логика взаимодействия для Cards.xaml
    /// </summary>
    public partial class Cards : Window
    {
        public Cards()
        {
            InitializeComponent();

            cmbSort.ItemsSource = new List<string>() { "А-Я", "Я-А" };
            cmbSort.SelectedIndex = 0;
            FillCards();
        }

        private void FillCards()
        {
            var data = Instances.db.Карта.ToList();

            if (cmbSort.SelectedItem != null)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        data = data.OrderBy(q => q.Фамилия).ToList();
                        break;
                    case 1:
                        data = data.OrderByDescending(q => q.Фамилия).ToList();
                        break;
                }
            }
            if (txtSearch.Text.Length != 0)
            {
                data = data.Where(q => q.Фамилия.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }
            Карта карт = new Карта();
            listViewCards.ItemsSource = data;
        }



        private void FillData()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Фамилия + ' ' + Имя + ' ' + Отчество AS ФИО, Дата_рождения, Телефон, Страховой_Полис, Описание_приема FROM Карта";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Записи");
                sda.Fill(dt);
                if (txtSearch.Text.Length != 0)
                {
                    dt.DefaultView.RowFilter = string.Format("ФИО LIKE '%{0}%'", txtSearch.Text);
                }
                listViewCards.ItemsSource = dt.DefaultView;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillCards();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCards();
        }
    }
}
