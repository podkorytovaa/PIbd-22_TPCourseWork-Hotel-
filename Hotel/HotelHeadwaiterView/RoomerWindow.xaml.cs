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
using HotelContracts.ViewModels;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using Unity;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Логика взаимодействия для RoomerWindow.xaml
    /// </summary>
    public partial class RoomerWindow : Window
    {
        private readonly IRoomerLogic _logic;
        private readonly IRoomLogic _roomLogic;
        public int Id { set { id = value; } }
        private int? id;

        public RoomerWindow(IRoomerLogic logic, IRoomLogic roomLogic)
        {
            InitializeComponent();
            _logic = logic;
            _roomLogic = roomLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxRoom.ItemsSource = _roomLogic.Read(new RoomBindingModel
            {
                HeadwaiterId = (int)App.Headwaiter.Id
            });
            if (id != null)
            {
                var roomer = _logic.Read(new RoomerBindingModel
                {
                    Id = id
                })[0];

                TextBoxFullName.Text = roomer.FullName;
                TextBoxPhoneNumber.Text = roomer.PhoneNumber;
                DateBookingPicker.SelectedDate = roomer.DateBooking;
                ComboBoxRoom.SelectedValue = roomer.RoomId;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFullName.Text))
            {
                MessageBox.Show("Введите полное имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPhoneNumber.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DateBookingPicker.SelectedDate == null) 
            {
                MessageBox.Show("Выберите время бронирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RoomViewModel room = (RoomViewModel)ComboBoxRoom.SelectedItem;

            try
            {
                _logic.CreateOrUpdate(new RoomerBindingModel
                {
                    Id = id,
                    FullName = TextBoxFullName.Text,
                    PhoneNumber = TextBoxPhoneNumber.Text,
                    DateBooking = (DateTime)DateBookingPicker.SelectedDate,
                    RoomId = room.Id,
                    HeadwaiterId = (int)App.Headwaiter.Id
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
    }
}
