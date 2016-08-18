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


/***********************************************************************
 * Author：汪京陆
 * Date：2016/8/10
 * Name:MainWindow.xaml.cs
 * *********************************************************************/

namespace attendanceManagement
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();         
        }
        

        /// <summary>
        /// 对右键点击事件的响应函数
        /// 弹出底部appbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
          
            var item = Flyouts.Items[0] as Flyout;
            item.IsOpen = true;
        }//方法Grid_MouseRightBUttonDown结束



        /// <summary>
        /// 对课程选择框选择事件的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CourseSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /**
             * 对主窗口Model  MainwindowData中CourseIndex进行赋值
             * 后续操作在CourseIndex{set;}中完成
             */
            MainwindowData.data.CourseIndex = CourseSelection.SelectedIndex;          
        }//方法CourseSelection_selectionchanged结束




        /// <summary>
        /// 日期选择框响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainwindowData.data.DateIndex = DateSelection.SelectedIndex;
        }




        /// <summary>
        /// 窗口加载完成相应函数、程序入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //把主窗口上下文赋值Mainwindowdata
            DataContext = MainwindowData.data;
            MainwindowData.data.Window = this;

            //导入课程表
            MainwindowData.data.courselist = CourseInfo.getCourses();
            
        }





        /// <summary>
        /// 控制上课时间对话框的显示与隐藏
        /// </summary>
        /// <param name="dialog">操作的对话框句柄</param>
        /// <param name="flag">默认为true，显示， false 关闭</param>
        public async void timeSetting(CourseSettingDialog dialog,bool flag = true)
        {
            if(flag)
            {
                await DialogManager.ShowMetroDialogAsync(this, dialog);
            }
            else
            {
                await DialogManager.HideMetroDialogAsync(this, dialog);
            }
        }




    }//MainWindow结束
   
}
