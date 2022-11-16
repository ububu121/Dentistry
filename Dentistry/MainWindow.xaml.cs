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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dentistry
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_entr_Click(object sender, RoutedEventArgs e)
        {
            var login = txtlogin.Text;
            var pass = txtpass.Password;

            Пользователи user;

            if ((user = Instances.db.Пользователи.FirstOrDefault(q => q.Логин_Пользователя == login && q.Пароль_Пользователя == pass)) != null)
            {
                Hide();
                new Menu(user).ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("Неправильно введен логин или пароль, попробуйте еще раз!");
            }

        }
    }
    
}
