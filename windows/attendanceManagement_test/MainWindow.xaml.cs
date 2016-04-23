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
using attendanceManagement.ATTENDANCE;


namespace attendanceManagement
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    //带有Course和CourseDate的组件
    class ZTreeViewItem : TreeViewItem
    {
        //当前的课程
        public readonly Course course = null;
        //当前的课程时间
        public readonly CourseDate date = null;
        public ZTreeViewItem(Course course)
        {
            this.course = course;
        }
        public ZTreeViewItem(Course course, CourseDate date)
        {
            this.course = course;
            this.date = date;
        }
    }

    public partial class MainWindow : Window
    {
        LinkedList<Course> coursesInfo = new LinkedList<Course>();


        public MainWindow()
        {
      
            InitializeComponent();
            openCourses();
            foreach (Course course in coursesInfo)
            {
                ZTreeViewItem item = new ZTreeViewItem(course);
                item.Header = course.get_course_name();
                LinkedList<CourseDate> dates = course.get_dates();
                foreach (CourseDate date in dates)
                {
                    ZTreeViewItem item2 = new ZTreeViewItem(course,date);
                    item2.Header = date.toString();
                    item2.MouseDoubleClick += treeViewItem_getCurrentCourse;
                    item.Items.Add(item2);

                }
                treeView.Items.Add(item);
            }
            dataGrid.MouseDoubleClick += TableRowClick;



        }

        void treeViewItem_getCurrentCourse(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            //设置label 上的数据
            ZTreeViewItem item = (ZTreeViewItem)sender;
            teacher_info.Content = item.course.get_teacher_name();
            course_info.Content = item.course.get_course_name();
            time_info.Content = item.date.get_week()+" "+item.date.get_start();
            number_info.Content = item.course.get_number();

            //生成当前考勤课程
            ZXmlDocument doc = new ZXmlDocument(item.course.get_filepath());
            doc.setCurrentCourse(item.course,item.date);
            ZXmlDocument.generateResultXml();
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
                        course.set_filepath("courses\\" + list[i].Name);
                        coursesInfo.AddFirst(course);
                    }
                }
               
            }
            catch(Exception e)
            {
                return;
            }
        }

    
        //表格查看学生详细信息
        private void TableRowClick(object sender, RoutedEventArgs e)
        {
            DataGrid row = (DataGrid)sender;
            int index = row.SelectedIndex;
            CurrentCourse course = CurrentCourse.getInstance();
            List<StudentInfo> list = course.getStudentList();
            String s = "姓名： " + list[index].name + "\n" + "学号： " + list[index].id + "\n" + "性别： " + list[index].sex + "\n" +
                "学院： " + list[index].college + "\n" + "专业： " + list[index].major + "\n" + "班级： " + list[index].sclass + "\n" +
                "蓝牙地址： " + list[index].macAdr;
            MessageBox.Show(s, "学生详细信息");
        }


        private void Btn_Begin_Click(object sender, RoutedEventArgs e)
        {
            //test 
            CurrentCourse course = CurrentCourse.getInstance();
            if (course.getCourseId() == null)
            {
                MessageBox.Show("未选择考勤课程，无法开始考勤！", "警告");
            }
            else
            {
                BTHOPERATE b = new BTHOPERATE();
                b.start(ref this.dataGrid);
                MessageBox.Show("考勤开始！", "警告");
                Button btn = (Button)sender;
                btn.Content = "考勤结束";
            }
            
        
        }


    }
}
