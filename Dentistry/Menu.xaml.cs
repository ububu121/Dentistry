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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        int Role;
        private Пользователи user = null;
        public Menu(Пользователи user)
        {
            InitializeComponent();

            if (user != null)
            {
                this.user = user;
                this.lblRole.Content = user.Роли.Роль + " " + user.Фамилия_Пользователя + " " + user.Имя_Пользователя + " " + user.Отчество_Пользователя;
                Role = user.Роли.Код_Роли;
            }
        }

        private void Btn_list_services_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Services().ShowDialog();
            Show();


        }

        private void Btn_table_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Table().ShowDialog();
            Show();

        }

        private void Btn_make_appointment_Click(object sender, RoutedEventArgs e)
        {
            if (Role == 4 || Role == 3) 
            {
                Hide();
                new NewMenu().ShowDialog();
                Show();
            }
            else MessageBox.Show("Доступ ограничен!");
        }

        private void Btn_view_recordings_Click(object sender, RoutedEventArgs e)
        {
            if (Role == 1)
            {
                Hide();
                new Recordings(user).ShowDialog();
                Show();
            }
            else MessageBox.Show("Доступ ограничен!");
        }

        private void Btn_users_Click(object sender, RoutedEventArgs e)
        {
            if (Role == 3)
            {
                Hide();
                new Users().ShowDialog();
                Show();
            }
            else MessageBox.Show("Доступ ограничен!");
        }

        private void Btn_generate_reports_Click(object sender, RoutedEventArgs e)
        {
            if (Role == 2 || Role == 3)
            {
                Hide();
                new Reports().ShowDialog();
                Show();
            }
            else MessageBox.Show("Доступ ограничен!");
        }

        private void Btn_personal_cards_Click(object sender, RoutedEventArgs e)
        {
            if (Role == 1 || Role == 3 || Role == 4)
            {
                Hide();
                new Cards().ShowDialog();
                Show();
            }
            else MessageBox.Show("Доступ ограничен!");
        }

        private void Btn_create_card_Click(object sender, RoutedEventArgs e)
        {
            if (Role == 4 || Role == 3)
            {
                Hide();
                new CreateCards().ShowDialog();
                Show();
            }
            else MessageBox.Show("Доступ ограничен!");
        }

        private void Btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
