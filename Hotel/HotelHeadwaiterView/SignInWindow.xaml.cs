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
using HotelBusinessLogic.BusinessLogics;
using HotelContracts.ViewModels;
using HotelContracts.BusinessLogicsContracts;
using Unity;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private readonly IHeadwaiterLogic _logic;
        public SignInWindow(IHeadwaiterLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<SignUpWindow>();
            form.ShowDialog();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var headwaiters = _logic.Read(null);
            HeadwaiterViewModel _headwaiter = null;

            foreach (var hw in headwaiters)
            {
                if (hw.Login == TextBoxLogin.Text && hw.Password == TextBoxPassword.Text)
                {
                    _headwaiter = hw;
                }
            }

            if (_headwaiter != null)
            {
                App.Headwaiter = _headwaiter;
                var form = App.Container.Resolve<MainWindow>();
                form.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
