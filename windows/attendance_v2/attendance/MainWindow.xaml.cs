using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using attendanceManagement.XML;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using attendanceManagement.widget;

namespace attendanceManagement
{
    /// <summary>
    /// MainWindow_v2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();         
        }
        
        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
          
            var item = Flyouts.Items[0] as Flyout;
            item.IsOpen = true;
        }

        private void CourseSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainwindowData.data.CourseIndex = CourseSelection.SelectedIndex;          
        }

        private void DateSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainwindowData.data.DateIndex = DateSelection.SelectedIndex;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = MainwindowData.data;
            MainwindowData.data.Window = this;

            //导入课程表
            MainwindowData.data.courselist = CourseInfo.getCourses();
            
        }

        public async void timeSetting(MetroWindow window,CourseSettingDialog dialog,bool flag = true)
        {
            if(flag)
            {
                await DialogManager.ShowMetroDialogAsync(window, dialog);
            }
            else
            {
                await DialogManager.HideMetroDialogAsync(window, dialog);
            }
        }
    }
   
}
