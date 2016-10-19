using attendanceManagement.ATTENDANCE;
using attendanceManagement.NET;
using attendanceManagement.widget;
using attendanceManagement.XML;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

        public ProgressDialogController progressDialog;

        /****************************************************************************************
         * 后台数据
         * *************************************************************************************/

        /// <summary>
        /// 课程选择框中选中序号
        /// </summary>
        int courseindex = -1;
        public int CourseIndex
        {
            set
            {
                // 获得当前选中课程，并将日期选择框选中序号赋值为-1，
                // 把课程名和教师姓名显示在UI中，将历史记录绑定到日期
                // 选择框中。
                try
                {
                    Course course = courselist.ElementAt(value);
                    courseindex = value;
                    DateIndex = -1;
                    coursename = course.COURSENAME;
                    teachername = course.TEACHERNAME;
                    historylist = course.HISTORY;
                    checkingtable = new CheckingTable(CurrentCourse.COURSEID, CheckingDate, CheckingTime); //新建考勤表，并显示在UI中

                    //设置新建选项卡
                    WIFIName =Teacher.defaultWifiName!=""?Teacher.defaultWifiName: _coursename + "_" + _teachername;
                    WIFIPass =Teacher.defaultWifiPass!=""?Teacher.defaultWifiPass: "12345678";
                    Window.checkingtime.SelectedDate = DateTime.Today;
                    TimeSpan temp;
                    TimeSpan.TryParse(DateTime.Now.ToString("HH:mm"), out temp);
                    Window.checkingtime.SelectedTime = temp;


                    //如果正在考勤且当前查看课程与考勤课程不符，禁用”新建“tab项
                    if (checking)
                    {
                        if (checkIndex != courseindex)
                        {
                            currentTab = 0;
                            newTabisenable = false;
                        }
                        else
                        {
                            newTabisenable = true;
                            currentTab = 1;
                        }
                    }
                    else
                    {
                        newTabisenable = true;
                    }

                }
                catch(ArgumentOutOfRangeException e)
                {
                    System.Console.WriteLine(e.Message);
                    return;
                }
               
               
            }
        }


        /// <summary>
        /// 日期选择框中选中序号
        /// </summary>
        int dateindex = -1;
        public int DateIndex
        {
            set
            {
                //将历史考勤表显示在UI中，如果不选中任何
                //item（value==-1),清空表格

                dateindex = value;
                if (dateindex != -1)
                {
                    historycheckingtable = CurrentCourse.table(Window.DateSelection.SelectedIndex);
                   
                }
                else
                {
                    historycheckingtable = new CheckingTable();
                    nrofstu = CurrentCourse.nrofstu;
                    arrived = 0;
                    coursetime = "--:--";
                }
            }

            get { return dateindex; }
        }

        /// <summary>
        /// 正在进行考勤的课程序号
        /// </summary>
        public int checkIndex { get; set; } = -1;

        /// <summary>
        /// 控制考勤开启/关闭
        /// </summary>
        bool _checking = false;
        WIFIOPERATE attendance = new WIFIOPERATE();
        public bool checking
        {
            get { return _checking; }
            set
            {
                setProperty(ref _checking, value);
                if (_checking)                //赋值为true，考勤开始
                {
                    startlabel = "关闭热点";

                    checkIndex = courseindex;   //设置当前考勤课程序号
                    attendance.openNetwork(WIFIName, WIFIPass); //设置并打开热点
                    checkingtable = new CheckingTable(CurrentCourse.COURSEID, CheckingDate, CheckingTime); //新建考勤表，并显示在UI中
                    attendance.start(this);  //开始扫描
                }
                else                          //考勤结束
                {
                    checkIndex = -1;
                    startlabel = "开启热点";
                    attendance.end();       //关闭热点
                    CourseInfo.saveAttendancetable(checkingtable);  //保存考勤表

                    newTabisenable = true;    //当新建选项卡禁用时开启
                    historylist = CurrentCourse.HISTORY;       //刷新历史记录
                }
                   
            }
        }

        /// <summary>
        /// 当前tab
        /// </summary>
        public int currentTab
        {
            set
            {
                if (value <= -1 || value > 1)  //保证传入值在-1和1之间
                    return;
                Window.CourseInfoTabControl.SelectedIndex = value;
            }
            get { return Window.CourseInfoTabControl.SelectedIndex; }
        }

        
        public bool newTabisenable
        {
            set
            {
                if(value)
                {
                    (Window.CourseInfoTabControl.Items[1] as TabItem).IsEnabled = true;
                }
                else
                {
                    (Window.CourseInfoTabControl.Items[1] as TabItem).IsEnabled = false;
                }
            }
        }



        /// ////////////////////////////////////Label数据绑定/////////////////////
        /// <summary>
        /// 设置开启热点按钮显示字符
        /// </summary>
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
                string str = value;
                if(str.Length>7)
                {
                    str = str.Substring(0,7) + "...";
                }
                setProperty(ref _coursename, str);
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

        //正在考勤的总人数
        int _nrofstu_checking;
        public int nrofstu_checking
        {
            get { return _nrofstu_checking; }
            set { setProperty(ref _nrofstu_checking, value); }
        }


        //正在考勤的已到人数
        int _arrived_checking;
        public int arrived_checking
        {
            get { return _arrived_checking; }
            set { setProperty(ref _arrived_checking, value); }
        }


        /////////////////////////////////////textbox数据绑定////////////////////////

        public string WIFIPass
        {
            set { Window.wifi_password.Password = value; }
            get { return Window.wifi_password.Password; }          
        }

        public string WIFIName
        {
            set { Window.wifi_name.Text = value; }
            get { return Window.wifi_name.Text; }
        }

        public string CheckingDate
        {
            get
            {
                try
                {
                    var date = Window.checkingtime.SelectedDate.Value;
                    return date.ToString("yyyyMMdd");
                }
                catch(Exception e)
                {
                    return "";
                }
            }
        }
     
        public string CheckingTime
        {
            get
            {
                try
                {
                    var time = Window.checkingtime.SelectedTime.Value;
                    return time.ToString(@"hh\:mm");
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
        //////////////////////////////////////表格///////////////////////////////////

        //绑定CourseSelection listbox
        LinkedList<Course> _courselist;
        public LinkedList<Course> courselist
        {
            get { return _courselist; }
            set
            {
                //在课程选择框中添加课程，如没有课
                //程，则添加并显示"暂无课程"item。

                _courselist = value;
                Window.CourseSelection.SelectedIndex = -1;
                Window.CourseSelection.Items.Clear();
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
                    Window.CourseSelection.SelectedIndex = 0;   //选择第一个item
                }
            }
        }

        //绑定DateSelection listbox
        LinkedList<CheckingTable> _historylist;
        public LinkedList<CheckingTable> historylist
        {
            get { return _historylist; }
            set
            {
                _historylist = value;
                Window.DateSelection.Items.Clear();

                if (_historylist.Count == 0)
                {
                    var item = new ListBoxItem { Content = "暂无记录" };
                    item.IsEnabled = false;
                    Window.DateSelection.Items.Add(item);

                }
                else
                {
                    foreach (CheckingTable data in _historylist)
                    {
                        
                        Window.DateSelection.Items.Add(new ListBoxItem { Content = data.date });
                    }



                    Window.DateSelection.SelectedIndex = 0;
                }
            }
        }

        //绑定历史表格
        CheckingTable _historychcekingtable;
        public CheckingTable historycheckingtable
        {
            set
            {
                _historychcekingtable = value;
                Window.HistoryDATA.ItemsSource = _historychcekingtable.students;
                Window.HistoryDATA.Items.Refresh();

                nrofstu = _historychcekingtable.nrOfstu;
                arrived = _historychcekingtable.arrived;
                coursetime = _historychcekingtable.date + " " + _historychcekingtable.ts;
            }
            get
            {
                return _historychcekingtable;
            }
        }

        //显示未到学生
        public bool showAbsenceStudents
        {
            set
            {
                if (value)
                {
                    if (currentTab == 0)
                    {
                        Window.HistoryDATA.ItemsSource = _historychcekingtable.absenceStudents;
                        Window.HistoryDATA.Items.Refresh();
                    }
                    else
                    {
                        Window.NewTable.ItemsSource = _checkingtable.absenceStudents;
                        Window.NewTable.Items.Refresh();
                    }
                }
            }
        }

        //显示已到全部学生
        public bool showAllStudents
        {
            set
            {
                if(value)
                {
                    if (currentTab == 0)
                    {
                        Window.HistoryDATA.ItemsSource = _historychcekingtable.students;
                        Window.HistoryDATA.Items.Refresh();
                    }
                    else
                    {
                        Window.NewTable.ItemsSource = _checkingtable.students;
                        Window.NewTable.Items.Refresh();
                    }
                }
            }
        }



        //绑定新建表格
        CheckingTable _checkingtable;
        public CheckingTable checkingtable
        {
            set
            {
                _checkingtable = value;
                Window.NewTable.ItemsSource = _checkingtable.students;
                Window.NewTable.Items.Refresh();
                arrived_checking = _checkingtable.arrived;
                nrofstu_checking = _checkingtable.nrOfstu;
            }
            get
            {
                return _checkingtable;
            }
        }

        //修改学生出勤状态
        public bool ChangeState
        {
            set
            {
                if(value)
                {
                    if(currentTab==0)
                    {
                        var list = Window.HistoryDATA.SelectedItems;
                        foreach(Student temp in list)
                        {
                            foreach(Student stu in _historychcekingtable.students)
                            {
                                if(stu.id == temp.id)
                                {
                                    stu.changeState();
                                }
                            }
                        }
                        Window.HistoryDATA.Items.Refresh();
                        CourseInfo.saveAttendancetable(historycheckingtable);
                    }
                    else
                    {
                        var list = Window.NewTable.SelectedItems;
                        
                        foreach (Student temp in list)
                        {
                            foreach (Student stu in _checkingtable.students)
                            {
                                if (stu.id == temp.id)
                                {
                                    stu.changeState();
                                }
                            }
                        }
                        Window.NewTable.Items.Refresh();
                    }
                }
            }
        }

        //////////////////////////////功能///////////////////////////////

        /// <summary>
        /// 登陆
        /// </summary>
        public bool Login
        {
            set
            {
                if(value)
                {
                    LOGIN();
                }
            }
        }
        
        /// <summary>
        /// 同步
        /// </summary>
        public bool ASYNC_FILES
        {
            set
            {
                if(value)
                {
                    ////Window.showProgressDialog(true,null ,"正在同步", "请稍等！");
                    //if(!Teacher.isLogin)
                    //{
                    //    //Window.showProgressDialog(false,null);
                    //    Window.errorBoard("Please Log In First~");
                    //    return;
                    //}

                    //var download = new DownLoad();
                    //download.getclasslist();
                    //var courses = CourseInfo.getCourses();
                    //foreach (var course in courses)
                    //{
                    //    download.getstulist(course.COURSEID);
                    //}

                    //new UpLoad().uploadTable();
                    //MainwindowData.data.courselist = courses;
                    ////Window.showProgressDialog(false,null);
                    SYNC();
                }
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




        ///////////////////////////私有函数////////////////////////////

        private async void LOGIN()
        {
            await Window.showProgressDialog(true, async delegate ()
             {
                 int i = 0;
                 if (new UpLoad().login(Teacher.tid, Teacher.passwd))
                 {
                     await Window.showProgressDialog(false);
                     Teacher.isLogin = true;
                     Window.Dispatcher.Invoke(
                         delegate ()
                         {
                             ASYNC_FILES = true;
                             if (!Teacher.remember)
                             {
                                 Teacher.tid = "";
                                 Teacher.passwd = "";
                             }
                             CourseInfo.saveConfig();
                         });
        //Window.loginInfo("Authentication Information", "登陆成功");                 
                }
                 else
                 {
                     await Window.showProgressDialog(false);
                     Window.Dispatcher.Invoke(
                        delegate ()
                        {
                            Teacher.isLogin = false;
                            Teacher.tid = "";
                            Teacher.passwd = "";
                            Window.loginInfo("Authentication Information", "登陆失败");
                        });
                 }
             },
            "正在登陆","请稍后！");
            

        }

        private async void SYNC()
        {
            await Window.showProgressDialog(true, async delegate ()
            {
                if (!Teacher.isLogin)
                {
                    await Window.showProgressDialog(false);
                    Window.Dispatcher.Invoke(
                        delegate ()
                        {
                            Window.errorBoard("Please Log In First~");
                        }
                        );
                    return;
                }

                var download = new DownLoad();
                download.getclasslist();
                var courses = CourseInfo.getCourses();
                foreach (var course in courses)
                {
                    download.getstulist(course.COURSEID);
                }

                new UpLoad().uploadTable();
                Window.Dispatcher.Invoke(
                        delegate ()
                        {
                            data.courselist = courses;
                        }
                        );
               
                await Window.showProgressDialog(false);
            },
            "正在同步", "请稍后！");

        }
    }
}
