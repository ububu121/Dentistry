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
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Window
    {
        public Services()
        {
            InitializeComponent();
            
            cmbSort.ItemsSource = new List<string>() { "А-Я", "Я-А" };
            cmbSort.SelectedIndex = 0;
            load();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void load()
        {
            var data = Instances.db.Услуги.ToList();

            if (cmbSort.SelectedItem != null)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        data = data.OrderBy(q => q.Наименование_Услуги).ToList();
                        break;
                    case 1:
                        data = data.OrderByDescending(q => q.Наименование_Услуги).ToList();
                        break;
                }
            }
            if (txtSearch.Text.Length != 0)
            {
                data = data.Where(q => q.Наименование_Услуги.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }
            lstServices.ItemsSource = data;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            load();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            load();
        }
    }
}
