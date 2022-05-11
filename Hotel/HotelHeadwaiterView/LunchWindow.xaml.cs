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
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using Unity;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Логика взаимодействия для LunchWindow.xaml
    /// </summary>
    public partial class LunchWindow : Window
    {
        private readonly ILunchLogic _logic;
        public int Id { set { id = value; } }
        private int? id;

        public LunchWindow(ILunchLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название обеда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxDish.Text))
            {
                MessageBox.Show("Введите название блюда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxDrink.Text))
            {
                MessageBox.Show("Введите название напитка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new LunchBindingModel
                {
                    Id = id,
                    Name = TextBoxName.Text,
                    Dish = TextBoxDish.Text,
                    Drink = TextBoxDrink.Text,
                    HeadwaiterId = (int)App.Headwaiter.Id,
                    LunchSeminars = new Dictionary<int, string>()
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id != null)
            {
                var lunch = _logic.Read(new LunchBindingModel
                {
                    Id = id
                })[0];

                TextBoxName.Text = lunch.Name;
                TextBoxDish.Text = lunch.Dish;
                TextBoxDrink.Text = lunch.Drink;
            }
        }
    }
}
