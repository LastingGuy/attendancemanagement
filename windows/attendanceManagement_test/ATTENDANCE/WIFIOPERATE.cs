using attendanceManagement.NET;
using attendanceManagement.XML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace attendanceManagement_test.ATTENDANCE
{
    class WIFIOPERATE
    {

        const int FAIL = 0;
        const int SUCCESS = 1;
        const int FAIL_OPEN_HANDLE = 2;
        const int VERSION_IS_LOW = 3;
        const int FAIL_TO_SET_ALLWO = 4;
        const int FAIL_TO_SET_SSID = 5;
        const int FAIL_TO_SET_PASS = 6;
        const int FAIL_TO_START_USING = 7;
        const int FAIL_TO_START_NETWORK = 8;

        private string list;
        private string[] macs;

        private bool isWifiRun = false;

        private static System.Windows.Controls.DataGrid dataGird;
        private static Thread thread = null;
        SynchronizationContext m_SyncContext = null;

        private DateTime dt_start;
        private DateTime dt_end;
        private DateTime uploadtime;

        private CurrentCourse course;
        private bool started;

        public bool openNetwork(string ssid,string passwd,int maxpeer)
        {
            var wifi_process = new Process();
            wifi_process.StartInfo.FileName = "wifi.exe";
            wifi_process.StartInfo.Arguments = "-s " + ssid + " " + passwd + " " + maxpeer;
            wifi_process.StartInfo.UseShellExecute = false;
            wifi_process.StartInfo.CreateNoWindow = true;
            wifi_process.StartInfo.RedirectStandardError = true;
            wifi_process.StartInfo.RedirectStandardOutput = true;
            wifi_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            wifi_process.Start();

            string result = wifi_process.StandardOutput.ReadToEnd();
            string error = wifi_process.StandardError.ReadToEnd();

            wifi_process.WaitForExit();
            if(result==SUCCESS.ToString())
            {
                isWifiRun = true;
                return true;
            }
            return false;
        }
        public void start(ref System.Windows.Controls.DataGrid datagird)
        {
            dataGird = datagird;

            course = CurrentCourse.getInstance();
            dt_start = DateTime.Parse(course.getStartTime());
            dt_end = DateTime.Parse(course.getEndTime());
            started = true;

            m_SyncContext = SynchronizationContext.Current;

            uploadtime = dt_start;
            uploadtime.AddMinutes(5);
            if(!isWifiRun)
            {
                openNetwork("attendance", "123123123", 100);
            }

            if (thread == null&&isWifiRun)
            {
                thread = new Thread(listMacThread);
                thread.Start();

            }
        }

        private void listMacThread()
        {
            while(isWifiRun)
            {
                getList();
                getMacs();
                List<StudentInfo> studentList = checkAttendance();
                m_SyncContext.Post(syncdatagird, studentList);

                ZXmlDocument.generateResultXml();
            }
        }

        private void getList()
        {
            var wifi_process = new Process();
            wifi_process.StartInfo.FileName = "wifi.exe";
            wifi_process.StartInfo.Arguments = "-ls";
            wifi_process.StartInfo.UseShellExecute = false;
            wifi_process.StartInfo.CreateNoWindow = true;
            wifi_process.StartInfo.RedirectStandardError = true;
            wifi_process.StartInfo.RedirectStandardOutput = true;
            wifi_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            wifi_process.Start();

            list = wifi_process.StandardOutput.ReadToEnd();
            string error = wifi_process.StandardError.ReadToEnd();
            wifi_process.WaitForExit();
        }

        private void getMacs()
        {
            const string pattern = "<([^>]*)>";

            MatchCollection macs_match = Regex.Matches(list, pattern);

            macs = new string[macs_match.Count];

            for (int n = 0; n < macs_match.Count; n++)
            {
                macs[n] = macs_match[n].Groups[1].Value;
            }

        }


        private List<StudentInfo> checkAttendance()
        {

            List<StudentInfo> list = course.getStudentList();


            for (int i = 0; i < list.Count; i++)
            {
                bool arrived = false;
                for (int j = 0; j < macs.Length; j++)
                {
                    if (list[i].macAdr.ToUpper() == macs[j].ToUpper())
                    {
                        arrived = true;
                        break;
                    }
                }

                int last = list[i].check;
                list[i].check = judgeState(list[i].check, arrived);
                list[i].arrive = getState(list[i].check);

                if (last != list[i].check)
                {
                    if (list[i].check == 1 || list[i].check == 2)
                        list[i].ts = DateTime.Now.ToLongTimeString().ToString();
                    else if (list[i].check == 3)
                        list[i].te = DateTime.Now.ToLongTimeString().ToString();
                }

            }


            return list;
        }

        private void show()
        {

            List<StudentInfo> list = course.getStudentList();
            for (int n = 0; n < list.Count; n++)
            {
                list[n].check = 0;
                list[n].arrive = "未到";
            }

            dataGird.ItemsSource = list;

        }

        private void syncdatagird(object data)
        {
            dataGird.ItemsSource = (List<StudentInfo>)data;
            dataGird.Items.Refresh();
        }

        //判断未到0、出勤1、迟到2、早退3
        private int judgeState(int stateNow, bool arrived)
        {

            DateTime presentTime = DateTime.Now;
            int state = 0;
            if (stateNow == -1 || stateNow == 0)
            {
                if (arrived == true)
                {
                    state = DateTime.Compare(presentTime, dt_start) > 0 ? 2 : 1;
                    state = DateTime.Compare(presentTime, dt_end) > 0 ? 0 : state;
                }
                else
                    state = 0;
            }
            else
            {
                if (!arrived)
                {
                    state = DateTime.Compare(presentTime, dt_end) < 0 ? 3 : 1;
                    state = DateTime.Compare(presentTime, dt_start) < 0 ? 1 : state;
                }
                else
                    state = stateNow;
            }
            return state;
        }

        private void upload()
        {
            if (DateTime.Compare(DateTime.Now, uploadtime) < 0)
            {
                new UpLoad().checkin_file(course.getCourseId(), string.Format("{0:yyyyMMdd}", DateTime.Today), "123456");
                uploadtime.AddMinutes(50);
            }
        }

        //得到UI中显示状态文字
        private string getState(int state)
        {
            switch (state)
            {
                case 0:
                    return "未到";
                case 1:
                    return "到课";
                case 2:
                    return "迟到";
                case 3:
                    return "早退";
                default:
                    return "";
            }
        }
    }
}
