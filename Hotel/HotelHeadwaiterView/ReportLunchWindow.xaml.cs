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
    /// Логика взаимодействия для ReportLunchWindow.xaml
    /// </summary>
    public partial class ReportLunchWindow : Window
    {
        private readonly IHeadwaiterReportLogic _reportLogic;

        public ReportLunchWindow(IHeadwaiterReportLogic reportLogic)
        {
            InitializeComponent();
            _reportLogic = reportLogic;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты начала и окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var dataSource = _reportLogic.GetLunches(new ReportBindingModel
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                LunchesGrid.ItemsSource = dataSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonPdf_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты начала и окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _reportLogic.SaveLunchesToPdf(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = DatePickerFrom.SelectedDate,
                            DateTo = DatePickerTo.SelectedDate
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
