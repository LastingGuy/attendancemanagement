﻿//using attendanceManagement.widget;
//using attendanceManagement.XML;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Windows.Controls;

///*******************************************************************************
// * author:汪京陆
// * Date:2016/8/10
// * Description:此文件中提供了整个工程所需的所有数据结构
// * 
// * attendanceManatementModel:对有需要数据绑定的类，需要集成此类，提供setProperty方法，更新UI
// * MainwindowData:提供对窗口的数据绑定
// * Student:存储学生个人信息
// * Course:课程类
// * CourseDate:保存课程上课时间，在Course类中使用
// * HistoryData：保存历史考勤表
// * DIR：保存
// * 
// * 修改：
// *      已分别保存至attendanceManagement.Models空间中
// * **************************************************************************

//namespace attendanceManagement
//{
//    class attendanceManagementModel : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void setProperty<T>(ref T item, T value, [CallerMemberName] string itemname = null)
//        {
//            if (!EqualityComparer<T>.Default.Equals(item, value))
//            {
//                item = value;
//                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(itemname));
//            }

//        }

//    }

//    /// <summary>
//    /// 主窗口Model
//    /// </summary>
//    class MainwindowData : attendanceManagementModel
//    {
//        public static MainwindowData data = new MainwindowData();

//        /*********************************************************************************************
//         * 控件
//         * *****************************************************************************************
               
//        //主窗口
//        public MainWindow Window;

//        /// <summary>
//        /// 时间设置对话框{get;}
//        /// 赋值为true是显示，false关闭
//        /// </summary>
//        CourseSettingDialog _timesettingdialog = new CourseSettingDialog();
//        public bool showTimeSetting
//        {
//            set
//            {
//                if(value)
//                {
//                    _timesettingdialog.DATES = courselist.ElementAt(courseindex).DATES;
//                    Window.timeSetting( _timesettingdialog);             
//                }
//                else
//                {
//                    Window.timeSetting( _timesettingdialog,false);
//                }
//            }
//        }



//        /****************************************************************************************
//         * 数据
//         * *************************************************************************************/

//        int courseindex = -1;
//        public int CourseIndex
//        {
//            set
//            {
//                Course course = courselist.ElementAt(value);
//                courseindex = value;
//                DateIndex = -1;
//                coursename = course.COURSENAME;
//                teachername = course.TEACHERNAME;
//                historylist = course.HISTORY;
//            }
//        }


//        int dateindex = -1;
//        public int DateIndex
//        {
//            set
//            {
//                dateindex = value;
//                if (dateindex != -1)
//                {
//                    Course course = CurrentCourse;
//                    historytable = course.table;
//                }
//                else
//                {
//                    historytable = new List<Student>();
//                }
//            }

//            get { return dateindex; }
//        }


//        public int checkIndex { get; set; } = -1;

//        bool _checking = false;

//        public bool checking
//        {
//            get { return _checking; }
//            set
//            {
//                setProperty(ref _checking, value);
//                if (_checking)
//                    startlabel = "关闭热点";
//                else
//                    startlabel = "开启热点";
//            }
//        }

//        string _startlabel = "开启热点";
//        public string startlabel
//        {
//            get
//            {
//                return _startlabel;
//            }
//            set
//            {
//                setProperty(ref _startlabel, value);
//            }
//        }

//        //绑定老师名
//        string _teachername = "";
//        public string teachername
//        {
//            get
//            { return _teachername; }
//            set
//            {
//                setProperty(ref _teachername, value);
//            }
//        }

//        //绑定课程名
//        string _coursename;
//        public string coursename
//        {
//            get { return _coursename; }
//            set
//            {
//                setProperty(ref _coursename, value);
//            }
//        }

//        //绑定上课时间
//        string _coursetime;
//        public string coursetime
//        {
//            set { setProperty(ref _coursetime, value); }
//            get { return _coursetime; }
//        }

//        //绑定上课人数
//        int _nrofstu;
//        public int nrofstu
//        {
//            set { setProperty(ref _nrofstu, value); }
//            get { return _nrofstu; }
//        }

//        //绑定已到人数
//        int _arrived;
//        public int arrived
//        {
//            set { setProperty(ref _arrived, value); }
//            get { return _arrived; }
//        }

//        //绑定CourseSelection listbox
//        LinkedList<Course> _courselist;
//        public LinkedList<Course> courselist
//        {
//            get { return _courselist; }
//            set
//            {
//                _courselist = value;
//                if (_courselist.Count == 0)
//                {
//                    var item = new ListBoxItem { Content = "暂无课程" };
//                    item.IsEnabled = false;
//                    Window.CourseSelection.Items.Add(item);
//                    Window.courseInfoView.Visibility = System.Windows.Visibility.Hidden;
//                    Window.appbar.btn_timesetting.IsEnabled = false;
//                }
//                else
//                {
//                    foreach (Course course in _courselist)
//                    {
//                        Window.CourseSelection.Items.Add(new ListBoxItem { Content = course.COURSENAME });
//                    }
//                    Window.courseInfoView.Visibility = System.Windows.Visibility.Visible;

//                    Window.appbar.btn_timesetting.IsEnabled = true;
//                    Window.CourseSelection.SelectedIndex = 0;
//                }
//            }
//        }

//        //绑定DateSelection listbox
//        LinkedList<HistoryData> _historylist;
//        public LinkedList<HistoryData> historylist
//        {
//            get { return _historylist; }
//            set
//            {
//                _historylist = value;
//                Window.DateSelection.Items.Clear();
//                //while(Window.DateSelection)

//                if (_historylist.Count == 0)
//                {
//                    var item = new ListBoxItem { Content = "暂无记录" };
//                    item.IsEnabled = false;
//                    Window.DateSelection.Items.Add(item);


                   
//                }
//                else
//                {
//                    foreach (HistoryData data in _historylist)
//                    {
//                        Window.DateSelection.Items.Add(new ListBoxItem { Content = data.date });
//                    }


                   
//                    Window.DateSelection.SelectedIndex = 0;
//                }
//            }
//        }

//        //绑定历史表格
//        public List<Student> historytable
//        {
//            set
//            {
//                Window.HistoryDATA.ItemsSource = value;
//                Window.HistoryDATA.Items.Refresh();
//            }
//            get
//            {
//                return (List<Student>)Window.HistoryDATA.ItemsSource;
//            }
//        } 

//        //绑定新建表格
//        public List<Student> newtable
//        {
//            set
//            {
//                Window.NewTable.ItemsSource = value;
//                Window.NewTable.Items.Refresh();
//            }
//            get
//            {
//                return (List<Student>)Window.NewTable.ItemsSource;
//            }
//        }

//        //当前Course
//        public Course CurrentCourse
//        {
//            get
//            {
//                return courselist.ElementAt(courseindex);
//            }
//        }

//    }

//    /// <summary>
//    /// 学生类
//    /// 保存学生个人信息
//    /// </summary>
//    class Student
//    {
//        public string name { get; set; }
//        public string id { get; set; }
//        public string college { get; set; }
//        public string major { get; set; }
//        public string sex { get; set; }
//        public string mac { get; set; }
//        public string sclass { get; set; }
//        public string time { get; set; }


//        //判断未到0、出勤1、迟到2、早退3
//        int check  = 0;
//        public int CHECK
//        {
//            get { return check; }
//            set
//            {
//                check = value;
//            }
//        } 
//        public string arrive
//        {
//            set { }
//            get
//            {
//                switch(check)
//                {
//                    case 0:
//                        return "未到";
//                    case 1:
//                        return "到课";
//                    case 2:
//                        return "迟到";
//                    case 3:
//                        return "早退";
//                    default:
//                        return "未到";
//                }
//            }
//        }    
//    }

//    /// <summary>
//    /// 课程类
//    /// 
//    /// 存储课程的基本信息
//    /// 
//    /// </summary>
//    class Course
//    {
//        //课程id,课程名，教师名，人数，上课时间
//        string course_id  = "";
//        public string COURSEID
//        {
//            set
//            {
//                course_id = value;
//            }
//            get
//            {
//                return course_id;
//            }
//        }

//        string course_name  = "";
//        public string COURSENAME
//        {
//            set
//            {
//                course_name = value;
//            }
//            get
//            {
//                return course_name;
//            }
//        }

//        string teacher_name  = "";
//        public string TEACHERNAME
//        {
//            set
//            {
//                teacher_name = value;
//            }
//            get
//            {
//                return teacher_name;
//            }
//        }

//        string teacher_id  = "";
//        public string TEACHERID
//        {
//            set
//            {
//                teacher_id = value;
//            }
//            get
//            {
//                return teacher_id;
//            }
//        }

//        int nrOfStu  = 0;
//        public int NROFSTU
//        {
//            set { nrOfStu = value; }
//            get { return nrOfStu; }
//        }
        
//        //获得上课时间个数
//        public int NROFDATES
//        {
//            get
//            {
//                return dates.Count;
//            }
//        }

//        //当前课程所在的xml文件路径
//        public string COURSEPATH
//        {
//            get
//            {
//                return DIR.HISTORY + course_id + "/";
//            }
//        }

//        //学生名单
//        List<Student> students;
//        public List<Student> STUDENTS
//        {
//            get
//            {
//                if(students==null)
//                    students = CourseInfo.getStudents(course_id);
//                return students;
//            }
//        }

//        //上课时间
//        LinkedList<CourseDate> dates  = new LinkedList<CourseDate>();
//        public LinkedList<CourseDate> DATES
//        {
//            get
//            {
//                return dates;
//            }
//        }

       
//        /// <summary>
//        /// 获得历史纪录{get;}
//        /// 利用CourseInfo.getHistory对COURSEPATH文件夹中xml文件进行解析，
//        /// 获得一个LinkedList<HistoryData>类型的链表，并返回
//        /// </summary>    
//        public LinkedList<HistoryData> HISTORY
//        {
//            get
//            {
//                var temp =  CourseInfo.getHistory(COURSEPATH);
//                return temp;
//            }

//        }


//        /// <summary>
//        /// 获得历史考勤表
//        /// </summary>
//        public List<Student> table
//        {
//            get
//            {
//                var table = HISTORY.ElementAt(MainwindowData.data.Window.DateSelection.SelectedIndex).table;

//                int n = 0;

//                while(n<table.Count)
//                {
//                    Student stu = table.ElementAt(n);
//                    var stuinfo = getStudentInfo(stu.id);
//                    if(stuinfo==null)
//                    {
//                        table.Remove(stu);
//                        continue;
//                    }

//                    stu.name = stuinfo.name;
//                    stu.major = stuinfo.major;
//                    stu.sclass = stuinfo.sclass;
//                    stu.college = stuinfo.college;

//                    n++;
//                }
//                return table;
//            }
//        }

//        /// <summary>
//        /// 获得历史考勤表上课人数
//        /// </summary>
//        public int nrofstu
//        {
//            get { return students.Count; }
//        }


//        /// <summary>
//        /// 获得历史考勤表已到人数 {get;}
//        /// </summary>
//        public int nrofarrived
//        {
//            get
//            {
//                int selectdateindex = MainwindowData.data.DateIndex;
//                var temp = HISTORY.ElementAt(selectdateindex);
//                int count = 0;
//                foreach(var stu in temp.table)
//                {
//                    if (stu.CHECK == 1)
//                        count++;
//                }
//                return count;
//            }
//        }


//        /// <summary>
//        /// 保存上课时间直xml {get;}
//        /// 复制为true保存
//        /// </summary>
//        public bool SaveTimeToXML
//        {
//            set
//            {
//                if(value)
//                {
//                    CourseInfo.setTimes(course_id, DATES);
//                }
//            }
            
//        }


//        /// <summary>
//        /// 添加上课时间
//        /// </summary>
//        public void add_date(string start, string week)
//        {
//            CourseDate date = new CourseDate(start, week);
//            dates.AddFirst(date);
//        }//方法add_date结束


//        //获得学生信息
//        private Student getStudentInfo(string id)
//        {
//            foreach(Student stu in STUDENTS)
//            {
//                if(stu.id == id)
//                {
//                    return stu;
//                }
//            }

//            return null;
//        }//方法getStudentInfo结束
//    }

//    /// <summary>
//    /// 上课时间
//    /// </summary>
//    public class CourseDate
//    {
//        public string start = "";
//        public string week = "";
//        public CourseDate(string start, string week)
//        {
//            this.start = start;
//            this.week = week;
//        }
//        override public string ToString()
//        {
//            return "周"+week + "   " + start;
//        }

//        public string get_start()
//        {
//            return start;
//        }

//        public string get_week()
//        {
//            return week;
//        }
//    }


//    /// <summary>
//    /// 历史记录
//    /// </summary>
//    class HistoryData
//    {
//        public string path;
//        public string date = "";
//        public string time = "";
        
//        public List<Student> table
//        {
//            get
//            {
//                return CourseInfo.getHistoryTable(path + date + ".xml");
//            }
//        }
//    }


//    struct  DIR
//    {
//        //根目录
//        public const string ROOT = "courses/";
//        //名单目录
//        public const string STULIST = ROOT + "stulist/";
//        //历史目录
//        public const string HISTORY = ROOT + "results/history/";
//        //上传目录
//        public const string UPLOAD = ROOT + "results/upload/";
//        //课表文件
//        public const string COURSES = ROOT + "courses.xml";
//    };
//}
