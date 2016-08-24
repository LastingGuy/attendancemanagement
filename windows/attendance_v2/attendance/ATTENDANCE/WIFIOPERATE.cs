using attendanceManagement.Models;
using attendanceManagement.NET;
using attendanceManagement.XML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace attendanceManagement.ATTENDANCE
{
    /****************************************************************
     * Author:汪京陆
     * Date:2016/8/4
     * Description:创建wifi.exe子进程，扫描连接的手机mac地址，检查学生到
     *             课情况并更新UI
     * 
     *             
     ****************************************************************/
    class WIFIOPERATE
    {

        private string list;
        private string[] macs;

        private bool isWifiRun = false;

        private static MainwindowData windowdata;
        private static Thread thread = null;
        SynchronizationContext m_SyncContext = null;

        private DateTime dt_start;
        private DateTime dt_end;
        private DateTime uploadtime;

        private Course course;
        private CheckingTable table;
        private bool started;


        /// <summary>
        /// 打开负载网络
        /// 
        /// 创建子进程，调用wifi.exe -s <ssid> <passwd> <maxpeer>
        /// </summary>
        /// <param name="ssid">热点名称</param>
        /// <param name="passwd">热点密码</param>
        /// <param name="maxpeer">最大连接数</param>
        /// <returns></returns>
        public bool openNetwork(string ssid, string passwd, int maxpeer = 100)
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
            if (result == Status.OK.ToString())
            {
                isWifiRun = true;
                return true;
            }
            return false;
        }//openNetwork结束


        /// <summary>
        /// 关闭负载网络
        /// </summary>
        /// <returns></returns>
        void shutdown()
        {
            var wifi_process = new Process();
            wifi_process.StartInfo.FileName = "wifi.exe";
            wifi_process.StartInfo.Arguments = "-q";
            wifi_process.StartInfo.UseShellExecute = false;
            wifi_process.StartInfo.CreateNoWindow = true;
            wifi_process.StartInfo.RedirectStandardError = true;
            wifi_process.StartInfo.RedirectStandardOutput = true;
            wifi_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            wifi_process.Start();
            wifi_process.WaitForExit();
        }

        /// <summary>
        /// 开始扫描
        /// </summary>
        /// <param name="data"></param>
        public void start(MainwindowData data)
        {
            
            windowdata = data;

            course = windowdata.CurrentCourse;
            started = true;

            m_SyncContext = SynchronizationContext.Current;

            table = windowdata.checkingtable;

            uploadtime = dt_start;
            uploadtime.AddMinutes(5);
            if (!isWifiRun)
            {
                openNetwork("attendance", "123123123", 100);
            }

            thread = new Thread(listMacThread);
            if(isWifiRun)
            thread.Start();
        }

        /// <summary>
        /// 结束扫描，关闭负载网络
        /// </summary>
        public void end()
        {
            if (started)
            {
                thread.Abort();
                new Thread(shutdown).Start();        //关闭负载网络
            }
        }


        private void listMacThread()
        {
            while (isWifiRun)
            {
                Thread.Sleep(5000);
                getList();
                getMacs();
                List<Student> studentList = checkAttendance();
                m_SyncContext.Post(syncdatagird, studentList);

                //ZXmlDocument.generateResultXml();
            }
        }

        /// <summary>
        /// 获得扫描结果
        /// </summary>
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

        /// <summary>
        /// 解析获得的已连接设备mac地址列表
        /// </summary>
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


        private List<Student> checkAttendance()
        {

            for (int i = 0; i < table.students.Count; i++)
            {
                bool arrived = false;
                for (int j = 0; j < macs.Length; j++)
                {
                    if (table.students[i].mac.ToUpper() == macs[j].ToUpper())
                    {
                        arrived = true;
                        break;
                    }

                }

                if (arrived)
                    table.students[i].CHECK = CheckStatus.ARRIVED;
                //else               
                //    students[i].CHECK = CheckStatus.ABSENCE;

            }


            return table.students;
        }

       

        /// <summary>
        /// 同步ui
        /// </summary>
        /// <param name="data"></param>
        private void syncdatagird(object data)
        {
            windowdata.checkingtable = (CheckingTable)table;

        }

        /// <summary>
        /// 判断未到0、出勤1、迟到2、早退3，以弃用
        /// </summary>
        //private int judgeState(int stateNow, bool arrived)
        //{

        //    DateTime presentTime = DateTime.Now;
        //    int state = 0;
        //    if (stateNow == -1 || stateNow == 0)
        //    {
        //        if (arrived == true)
        //        {
        //            state = DateTime.Compare(presentTime, dt_start) > 0 ? 2 : 1;
        //            state = DateTime.Compare(presentTime, dt_end) > 0 ? 0 : state;
        //        }
        //        else
        //            state = 0;
        //    }
        //    else
        //    {
        //        if (!arrived)
        //        {
        //            state = DateTime.Compare(presentTime, dt_end) < 0 ? 3 : 1;
        //            state = DateTime.Compare(presentTime, dt_start) < 0 ? 1 : state;
        //        }
        //        else
        //            state = stateNow;
        //    }
        //    return state;
        //}


        /// <summary>
        /// 上传至服务器，以弃用
        /// </summary>
        //private void upload()
        //{
        //    if (DateTime.Compare(DateTime.Now, uploadtime) < 0)
        //    {
        //        new UpLoad().checkin_file(course.COURSEID, string.Format("{0:yyyyMMdd}", DateTime.Today), "123456");
        //        uploadtime.AddMinutes(50);
        //    }
        //}
    

        //得到UI中显示状态文字，以弃用
        //private string getState(int state)
        //{
        //    switch (state)
        //    {
        //        case 0:
        //            return "未到";
        //        case 1:
        //            return "到课";
        //        case 2:
        //            return "迟到";
        //        case 3:
        //            return "早退";
        //        default:
        //            return "";
        //    }
        //}
    }
}
