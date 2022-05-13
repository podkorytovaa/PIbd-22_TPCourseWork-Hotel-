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
using HotelContracts.ViewModels;
using Unity;
using Microsoft.Win32;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Логика взаимодействия для GetSeminarListWindow.xaml
    /// </summary>
    public partial class GetSeminarListWindow : Window
    {

        private readonly IRoomLogic _roomLogic;
        private readonly IHeadwaiterReportLogic _reportLogic;

        public GetSeminarListWindow(IRoomLogic roomLogic, IHeadwaiterReportLogic reportLogic)
        {
            InitializeComponent();
            _roomLogic = roomLogic;
            _reportLogic = reportLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var listRooms = _roomLogic.Read(new RoomBindingModel
            {
                HeadwaiterId = (int)App.Headwaiter.Id
            });
            foreach (var room in listRooms)
            {
                ListBoxRooms.Items.Add(room);
            }
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxRooms.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var list = new List<RoomViewModel>();

                    foreach (var room in ListBoxRooms.SelectedItems)
                    {
                        list.Add((RoomViewModel)room);
                    }

                    _reportLogic.SaveRoomSeminarsToExcel(new ReportRoomBindingModel
                    {
                        FileName = dialog.FileName,
                        Rooms = list
                    });

                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxRooms.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<RoomViewModel>();

                    foreach (var room in ListBoxRooms.SelectedItems)
                    {
                        list.Add((RoomViewModel)room);
                    }

                    _reportLogic.SaveRoomSeminarsToWord(new ReportRoomBindingModel
                    {
                        FileName = dialog.FileName,
                        Rooms = list
                    });

                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
