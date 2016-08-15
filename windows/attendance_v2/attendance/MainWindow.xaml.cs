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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using attendanceManagement.XML;
using System.IO;

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
            Course course = MainwindowData.data.courselist.ElementAt(CourseSelection.SelectedIndex);
            MainwindowData.data.coursename = course.COURSENAME;
            MainwindowData.data.teachername = course.TEACHERNAME;
            MainwindowData.data.historylist = course.HISTORY;
            
        }

        private void DateSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateSelection.SelectedIndex != -1)
            {
                HistoryData history = MainwindowData.data.historylist.ElementAt(DateSelection.SelectedIndex);
                MainwindowData.data.table = history.table;
            }
        }
        void openCourses()  //打开courses的文件夹 读取文件信息
        {
            LinkedList<Course> coursesInfo = new LinkedList<Course>();
            try
            {
                DirectoryInfo courses = new DirectoryInfo(@"courses");
                FileSystemInfo[] list = courses.GetFileSystemInfos();
               
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].Extension.Equals(".xml"))
                    {
                        ZXmlDocument doc = new ZXmlDocument("courses\\" + list[i].Name);
                        Course course = doc.getCourse();
                        //course.set_filepath("courses\\" + list[i].Name);
                        coursesInfo.AddLast(course);
                    }
                }

            }
            catch (Exception e)
            {
                return;
            }
            MainwindowData.data.courselist = coursesInfo;
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = MainwindowData.data;
            MainwindowData.data.Window = this;

            //导入课程表
            MainwindowData.data.courselist = CourseInfo.getCourses();
            
        }
    }
   
}
