using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using attendanceManagement.NET;
using attendanceManagement.XML;

namespace attendanceManagement
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        LinkedList<Course> coursesInfo = new LinkedList<Course>();


        public MainWindow()
        {

            InitializeComponent();
            openCourses();
            foreach (Course course in coursesInfo)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = course.get_lesson_name();
                LinkedList<CourseDate> dates = course.get_dates();
                foreach (CourseDate date in dates)
                {
                    TreeViewItem item2 = new TreeViewItem();
                    item2.Header = date.toString();
                    item2.MouseDoubleClick += test;
                    item.Items.Add(item2);

                }
                treeView.Items.Add(item);
            }
        }

        void test(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            TreeViewItem item = (TreeViewItem)sender;
            item.Header = "haha";

        }

        void openCourses()  //打开courses的文件夹 读取文件信息
        {
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
                        coursesInfo.AddFirst(course);
                    }
                }
            }
            catch(Exception e)
            {
                return;
            }
        }

    

    private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
