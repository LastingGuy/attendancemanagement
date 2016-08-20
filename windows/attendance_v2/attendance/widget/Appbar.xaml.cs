using MahApps.Metro.Controls.Dialogs;
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
using MahApps.Metro.Controls;
using attendanceManagement.Models;

namespace attendanceManagement.widget
{
    /// <summary>
    /// AppBar.xaml 的交互逻辑
    /// </summary>
    public partial class Appbar : UserControl
    {
        public Appbar()
        {
            InitializeComponent();
            DataContext = MainwindowData.data;


        }
  

        private async void login_dialog(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            (window.Flyouts.Items[0] as Flyout).IsOpen = false;
            // window.teacher.Content = "hahaha";
            LoginDialogData result = await window.ShowLoginAsync("Authentication", "Enter your password", new LoginDialogSettings { ColorScheme = window.MetroDialogOptions.ColorScheme, RememberCheckBoxVisibility = Visibility.Visible });
            if (result == null)
            {
                //User pressed cancel
            }
            else
            {

                MessageDialogResult messageResult = await window.ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}\nShouldRemember: {2}", result.Username, result.Password, result.ShouldRemember));
            }
        }

        private void btn_time_setting_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            (window.Flyouts.Items[0] as Flyout).IsOpen = false;
            MainwindowData.data.showTimeSetting = true;
        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            (window.Flyouts.Items[0] as Flyout).IsOpen = false;
            (window.Flyouts.Items[1] as Flyout).IsOpen = true;
        }

    }
}
