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
using Unity;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemLanches_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<LunchesWindow>();
            form.ShowDialog();
        }

        private void MenuItemLunchSeminar_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<LinkLunchSeminarWindow>();
            form.ShowDialog();
        }

        private void MenuItemRooms_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<RoomsWindow>();
            form.ShowDialog();
        }

        private void MenuItemRoomers_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<RoomersWindow>();
            form.ShowDialog();
        }

        private void MenuItemSeminarsList_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<GetSeminarListWindow>();
            form.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<ReportLunchWindow>();
            form.ShowDialog();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            App.Headwaiter = null;
            var windowSignIn = App.Container.Resolve<SignInWindow>();
            Close();
            windowSignIn.ShowDialog();
        }
    }
}
