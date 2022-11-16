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
    /// Логика взаимодействия для NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();

            
            List<string> lstRole = new List<string>();
            var role = Instances.db.Роли.ToList();
            foreach (var r in role)
            {
                lstRole.Add(r.Роль);
            }
            cmbRole.ItemsSource = lstRole;


            }
        //private void CmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //if (cmbRole.SelectedIndex == 0)
        //    //{
        //    //    txtSpec.IsEnabled = true;
        //    //    txtTime.IsEnabled = true;
        //    //    txtkab.IsEnabled = true;
        //    //}
        //    //else
        //    //{
        //    //    txtSpec.IsEnabled = false;
        //    //    txtTime.IsEnabled = false;
        //    //    txtkab.IsEnabled = false;
        //    //}

        //}
        private void TxtPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void TxtFName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < 'а' || inp > 'я')
                if (inp < 'А' || inp > 'Я')
                    e.Handled = true;
        }
        //private void FillRasp()
        //{
        //    Расписание расп = new Расписание();
        //    расп.Пн = txtTime.Text;
        //    расп.Вт = txtTime.Text;
        //    расп.Ср = txtTime.Text;
        //    расп.Чт = txtTime.Text;
        //    расп.Пт = txtTime.Text;
        //    расп.Сб = txtTime.Text;
        //    расп.Вс = txtTime.Text;
        //    расп.Кабинет = txtkab.Text;
        //    var aa = Instances.db.Врачи;
        //    foreach (var a in aa)
        //    {
        //        расп.Код_Врача = a.Код_врача;
        //    }
           
        //}

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "" && txtLogin.Text != "" && txtPass.Text != "")
            {
                Пользователи пользователь = new Пользователи();
                пользователь.Фамилия_Пользователя = txtFName.Text;
                пользователь.Имя_Пользователя = txtLName.Text;
                пользователь.Отчество_Пользователя = txtPatronymic.Text;
                пользователь.Логин_Пользователя = txtLogin.Text;
                пользователь.Пароль_Пользователя = txtPass.Text;
                пользователь.Роль_Пользователя = cmbRole.SelectedIndex +1;
                //if (cmbRole.SelectedIndex == 0)
                //{
                //    txtSpec.IsEnabled = true;
                //    Врачи врач = new Врачи();
                //    врач.Фамилия = txtFName.Text;
                //    врач.Имя = txtLName.Text;
                //    врач.Отчество = txtPatronymic.Text;
                //    врач.Специальность = txtSpec.Text;
                //}
                Instances.db.Пользователи.Add(пользователь);
                Instances.db.SaveChanges();
                //FillRasp();
                MessageBox.Show("Новый пользователь создан!");
            }

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
