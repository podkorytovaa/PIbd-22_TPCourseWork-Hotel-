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
using HotelContracts.StoragesContracts;
using Unity;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Логика взаимодействия для LinkLunchSeminarWindow.xaml
    /// </summary>
    public partial class LinkLunchSeminarWindow : Window
    {
        private readonly ILunchLogic _lunchLogic;
        private readonly ISeminarLogic _seminarLogic;

        public LinkLunchSeminarWindow(ILunchLogic lunchLogic, ISeminarLogic seminarLogic)
        {
            InitializeComponent();
            _lunchLogic = lunchLogic;
            _seminarLogic = seminarLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxLunch.ItemsSource = _lunchLogic.Read(new LunchBindingModel
            {
                HeadwaiterId = (int)App.Headwaiter.Id
            });
            ComboBoxLunch.SelectedItem = null;

            var listSeminars = _seminarLogic.Read(null);
            foreach (var seminar in listSeminars)
            {
                ListBoxSeminars.Items.Add(seminar);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<int, string> lunchSeminars = new Dictionary<int, string>();
                foreach (var sem in ListBoxSeminars.SelectedItems)
                {
                    var seminar = (SeminarViewModel)sem;
                    lunchSeminars.Add(seminar.Id, seminar.Name);
                }

                LunchViewModel lunch = (LunchViewModel)ComboBoxLunch.SelectedItem;
                _lunchLogic.CreateOrUpdate(new LunchBindingModel
                {
                    Id = lunch.Id,
                    Name = lunch.Name,
                    Dish = lunch.Dish,
                    Drink = lunch.Drink,
                    HeadwaiterId = (int)App.Headwaiter.Id,
                    LunchSeminars = lunchSeminars
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
