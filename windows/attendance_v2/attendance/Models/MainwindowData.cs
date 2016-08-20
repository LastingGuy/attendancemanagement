using attendanceManagement.ATTENDANCE;
using attendanceManagement.widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace attendanceManagement.Models
{
    /// <summary>
    /// 主窗口Model
    /// </summary>
    class MainwindowData : attendanceManagementModel
    {
        public static MainwindowData data = new MainwindowData();

        /*********************************************************************************************
         * 控件
         * *****************************************************************************************/

        //主窗口
        public MainWindow Window;

        /// <summary>
        /// 时间设置对话框{get;}
        /// 赋值为true是显示，false关闭
        /// </summary>
        CourseSettingDialog _timesettingdialog = new CourseSettingDialog();
        public bool showTimeSetting
        {
            set
            {
                if (value)
                {
                    _timesettingdialog.DATES = courselist.ElementAt(courseindex).DATES;
                    Window.timeSetting(_timesettingdialog);
                }
                else
                {
                    Window.timeSetting(_timesettingdialog, false);
                }
            }
        }



        /****************************************************************************************
         * 数据
         * *************************************************************************************/

        int courseindex = -1;
        public int CourseIndex
        {
            set
            {
                Course course = courselist.ElementAt(value);
                courseindex = value;
                DateIndex = -1;
                coursename = course.COURSENAME;
                teachername = course.TEACHERNAME;
                historylist = course.HISTORY;
            }
        }


        int dateindex = -1;
        public int DateIndex
        {
            set
            {
                dateindex = value;
                if (dateindex != -1)
                {
                    Course course = CurrentCourse;
                    historytable = course.table;
                }
                else
                {
                    historytable = new List<Student>();
                }
            }

            get { return dateindex; }
        }


        public int checkIndex { get; set; } = -1;

        bool _checking = false;
        WIFIOPERATE attendance = new WIFIOPERATE();
        public bool checking
        {
            get { return _checking; }
            set
            {
                setProperty(ref _checking, value);
                if (_checking)
                {
                    startlabel = "关闭热点";
                   
                    attendance.openNetwork(_coursename + "_", "123456789");
                    attendance.start(this);
                }
                else
                {
                    startlabel = "开启热点";
                    attendance.end();
                }
                   
            }
        }

        string _startlabel = "开启热点";
        public string startlabel
        {
            get
            {
                return _startlabel;
            }
            set
            {
                setProperty(ref _startlabel, value);
            }
        }

        //绑定老师名
        string _teachername = "";
        public string teachername
        {
            get
            { return _teachername; }
            set
            {
                setProperty(ref _teachername, value);
            }
        }

        //绑定课程名
        string _coursename;
        public string coursename
        {
            get { return _coursename; }
            set
            {
                setProperty(ref _coursename, value);
            }
        }

        //绑定上课时间
        string _coursetime;
        public string coursetime
        {
            set { setProperty(ref _coursetime, value); }
            get { return _coursetime; }
        }

        //绑定上课人数
        int _nrofstu;
        public int nrofstu
        {
            set { setProperty(ref _nrofstu, value); }
            get { return _nrofstu; }
        }

        //绑定已到人数
        int _arrived;
        public int arrived
        {
            set { setProperty(ref _arrived, value); }
            get { return _arrived; }
        }

        //绑定CourseSelection listbox
        LinkedList<Course> _courselist;
        public LinkedList<Course> courselist
        {
            get { return _courselist; }
            set
            {
                _courselist = value;
                if (_courselist.Count == 0)
                {
                    var item = new ListBoxItem { Content = "暂无课程" };
                    item.IsEnabled = false;
                    Window.CourseSelection.Items.Add(item);
                    Window.courseInfoView.Visibility = System.Windows.Visibility.Hidden;
                    Window.appbar.btn_timesetting.IsEnabled = false;
                }
                else
                {
                    foreach (Course course in _courselist)
                    {
                        Window.CourseSelection.Items.Add(new ListBoxItem { Content = course.COURSENAME });
                    }
                    Window.courseInfoView.Visibility = System.Windows.Visibility.Visible;

                    Window.appbar.btn_timesetting.IsEnabled = true;
                    Window.CourseSelection.SelectedIndex = 0;
                }
            }
        }

        //绑定DateSelection listbox
        LinkedList<HistoryData> _historylist;
        public LinkedList<HistoryData> historylist
        {
            get { return _historylist; }
            set
            {
                _historylist = value;
                Window.DateSelection.Items.Clear();
                //while(Window.DateSelection)

                if (_historylist.Count == 0)
                {
                    var item = new ListBoxItem { Content = "暂无记录" };
                    item.IsEnabled = false;
                    Window.DateSelection.Items.Add(item);



                }
                else
                {
                    foreach (HistoryData data in _historylist)
                    {
                        Window.DateSelection.Items.Add(new ListBoxItem { Content = data.date });
                    }



                    Window.DateSelection.SelectedIndex = 0;
                }
            }
        }

        //绑定历史表格
        public List<Student> historytable
        {
            set
            {
                Window.HistoryDATA.ItemsSource = value;
                Window.HistoryDATA.Items.Refresh();
            }
            get
            {
                return (List<Student>)Window.HistoryDATA.ItemsSource;
            }
        }

        //绑定新建表格
        public List<Student> newtable
        {
            set
            {
                Window.NewTable.ItemsSource = value;
                Window.NewTable.Items.Refresh();
            }
            get
            {
                return (List<Student>)Window.NewTable.ItemsSource;
            }
        }

        //当前Course
        public Course CurrentCourse
        {
            get
            {
                return courselist.ElementAt(courseindex);
            }
        }

    }
}
