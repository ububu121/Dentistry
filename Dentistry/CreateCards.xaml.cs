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
    /// Логика взаимодействия для CreateCards.xaml
    /// </summary>
    public partial class CreateCards : Window
    {
        public CreateCards()
        {
            InitializeComponent();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AllClear()
        {
            txtFName.Clear();
            txtLName.Clear();
            txtPatronymic.Clear();
            txtDoc.Clear();
            txtNumber.Clear();
            
        }
        private void Btn_Write_Click(object sender, RoutedEventArgs e)
        {
            
            Карта картс = new Карта
            {
                Фамилия = txtFName.Text,
                Имя = txtLName.Text,
                Отчество = txtPatronymic.Text,
                Телефон = Convert.ToInt64(txtNumber.Text),
                Дата_рождения = Convert.ToDateTime(dpDate.SelectedDate),
                Страховой_Полис = Convert.ToInt32(txtDoc.Text)
            };
            Instances.db.Карта.Add(картс);
            Instances.db.SaveChanges();
            AllClear();
            MessageBox.Show("Карта создана");
        }

        private void TxtFName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < 'а' || inp > 'я' )
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

        private void TxtDoc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < '0' || inp > '9')
                e.Handled = true;
        }

        private void TxtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < '0' || inp > '9')
                e.Handled = true;
        }
    }
}
