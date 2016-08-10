using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement
{
    class attendanceManagementModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void  setProperty<T>(ref T item,T value,[CallerMemberName] string  itemname = null)
        {
            if(!EqualityComparer<T>.Default.Equals(item,value))
            {
                item = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(itemname));
            }

        }
            
    }
    class StudentInfo
    {
        public string name { get; set; }
        public String id { get; set; }
        public string college { get; set; }
        public string major { get; set; }
        public String sex { get; set; }
        public String macAdr { get; set; }
        public String ts { get; set; }
        public String te { get; set; }
        public String sclass { get; set; }
        public string arrive { get; set; } = "未到";

        // 0未到 1-3早退 4-6迟到 7到课 8请假
        //第一次考勤到课+1 第二次考勤到课+2 第三次考勤到课+4
        public int check { get; set; } = -1;
        public StudentInfo(String name, String id, String college, String major, String sex, String macAdr, String sclass)
        {
            this.name = name;
            this.id = id;
            this.college = college;
            this.major = major;
            this.sex = sex;
            this.macAdr = macAdr;
            this.sclass = sclass;
        }       
    }

    class MainwindowData: attendanceManagementModel
    {
        public static MainwindowData data = new MainwindowData();

        
        public int CourseIndex { get; set; } = -1;
        public int DateIndex { get; set; } = -1;
        public int checkIndex { get; set; } = -1;

        bool _checking = false;

        public bool checking
        {
            get { return _checking; }
            set
            {
                setProperty(ref _checking, value);
                if (_checking)
                    startlabel = "关闭热点";
                else
                    startlabel = "开启热点";
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

        string _coursename;
        public string coursename
        {
            get { return _coursename; }
            set
            {
                setProperty(ref _coursename, value);
            }
        }
    }
}
