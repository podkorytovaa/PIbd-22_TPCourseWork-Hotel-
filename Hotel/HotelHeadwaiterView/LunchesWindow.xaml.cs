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
    /// Логика взаимодействия для LunchesWindow.xaml
    /// </summary>
    public partial class LunchesWindow : Window
    {
        private readonly ILunchLogic _logic;
        public LunchesWindow(ILunchLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = App.Container.Resolve<LunchWindow>();
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LunchesWindow_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _logic.Read(new LunchBindingModel
                {
                    HeadwaiterId = (int)App.Headwaiter.Id
                });
                if (list != null)
                {
                    DataGridLunches.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridLunches.SelectedItems.Count == 1)
                {
                    var form = App.Container.Resolve<LunchWindow>();
                    form.Id = ((LunchViewModel)DataGridLunches.SelectedItems[0]).Id;

                    if (form.ShowDialog() == true)
                    {
                        LoadData();
                    }
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridLunches.SelectedItems.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int id = ((LunchViewModel)DataGridLunches.SelectedItems[0]).Id;

                    try
                    {
                        _logic.Delete(new LunchBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
