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
using attendanceManagement.NET;
using attendanceManagement.XML;

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

            Teacher.tid = result.Username;
            Teacher.passwd = result.Password;
            Teacher.remember = result.ShouldRemember;

            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                MainwindowData.data.Login = true;
                
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

        private void btn_absence_Click(object sender, RoutedEventArgs e)
        {
            MainwindowData.data.showAbsenceStudents = true;
        }

        private void btn_allstudent_Click(object sender, RoutedEventArgs e)
        {
            MainwindowData.data.showAllStudents = true;
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            MainwindowData.data.ChangeState = true;
        }

        private void btn_sync_Click(object sender, RoutedEventArgs e)
        {
            var download = new DownLoad();
            download.getclasslist(); 
            MainwindowData.data.courselist = CourseInfo.getCourses();
            foreach(var course in MainwindowData.data.courselist)
            {
                download.getstulist(course.COURSEID);
            }
        }
    }
}
