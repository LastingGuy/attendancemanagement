//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Diagnostics;
//using System.Threading;
//using System.Text.RegularExpressions;
//using attendanceManagement.XML;
//using attendanceManagement.NET;
//using attendanceManagement.Models;
/****************************************
 * Author:汪京陆
 * Date：2016/4/23
 * Description:
 *        利用蓝牙设备进行考勤
 *        
 * 修改：改用wifi，弃用蓝牙       
 *        
 ****************************************/





//namespace attendanceManagement.ATTENDANCE
//{
//    class BTHOPERATE
//    {
//        static string result;
//        static string[] macs;


//        private static System.Windows.Controls.DataGrid dataGird;
//        private static Thread thread=null;
//        SynchronizationContext m_SyncContext = null;

//        private DateTime dt_start;
//        private DateTime dt_end;
//        private DateTime uploadtime;

//        private CurrentCourse course;
//        private bool started;
       

//        //线程函数
//        private void threadProc()
//        {
//            while(true)
//            {
//                openBthProcess();

//                getMacs();
//                List<Student> list = checkAttendance();

//                m_SyncContext.Post(syncdatagird, list);

               
//                ZXmlDocument.generateResultXml();
//            }
//        }

//        //创建子bth进程
//        private void openBthProcess()
//        {
           
//                var bth_process = new Process();
//                bth_process.StartInfo.FileName = "bth.exe";
//                bth_process.StartInfo.UseShellExecute = false;
//                bth_process.StartInfo.CreateNoWindow = true;
//                bth_process.StartInfo.RedirectStandardError = true;
//                bth_process.StartInfo.RedirectStandardOutput = true;
//                bth_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
//                bth_process.Start();

//                result = bth_process.StandardOutput.ReadToEnd();
//                string error = bth_process.StandardError.ReadToEnd();
//                bth_process.WaitForExit();

//        }


//        private void getMacs()
//        {
//            const string pattern = "<([^>]*)>";
            
//            MatchCollection macs_match = Regex.Matches(result, pattern);

//            macs = new string[macs_match.Count];

//            for (int n = 0;n< macs_match.Count;n++)
//            {
//                macs[n] = macs_match[n].Groups[1].Value;
//            }

//        }


//        private List<Student> checkAttendance()
//        {
           
//            List<Student> list = course.getStudentList();
            

//            for (int i =0;i<list.Count;i++)
//            {
//                bool arrived = false;        
//                for(int j =0; j<macs.Length;j++)
//                {
//                    if (list[i].mac.ToUpper() == macs[j].ToUpper())
//                    {
//                        arrived = true;
//                        break;
//                    }
//                }

//                int last = list[i].CHECK;
//                list[i].CHECK = judgeState(list[i].CHECK, arrived);

                
//            }

            
//            return list;
//        }

//        private void show()
//        {
            
//            List<Student> list = course.getStudentList();
//            for (int n = 0; n < list.Count; n++)
//            {
//                list[n].CHECK = 0;

//            }
            
//            dataGird.ItemsSource = list;
            
//        }

//        private void syncdatagird(object data)
//        {
//            dataGird.ItemsSource = (List<Student>)data;
//            dataGird.Items.Refresh();
//        }

//        //判断未到0、出勤1、迟到2、早退3
//        private int judgeState(int stateNow,bool arrived)
//        {

//            DateTime presentTime = DateTime.Now;
//            int state =0;
//            if(stateNow == -1||stateNow==0)
//            {
//                if (arrived == true)
//                {
//                    state = DateTime.Compare(presentTime, dt_start) > 0 ? 2 : 1;
//                    state = DateTime.Compare(presentTime, dt_end) > 0 ? 0 : state;
//                }
//                else
//                    state = 0;
//            }
//            else
//            {
//                if (!arrived)
//                {
//                    state = DateTime.Compare(presentTime, dt_end) < 0 ? 3 : 1;
//                    state = DateTime.Compare(presentTime, dt_start) < 0 ? 1 : state;
//                }
//                else
//                    state = stateNow;
//            }
//            return state;
//        }

//        private void upload()
//        {
//            if(DateTime.Compare(DateTime.Now,uploadtime)>0)
//            {
//                new UpLoad().checkin_file(course.getCourseId(), string.Format("{0:yyyyMMdd}",DateTime.Today),"123456");
//                uploadtime.AddMinutes(50);
//            }
//        }

//        //得到UI中显示状态文字
//        private string getState(int state)
//        {
//            switch(state)
//            {
//                case 0:
//                    return "未到";
//                case 1:
//                    return "到课";
//                case 2:
//                    return "迟到";
//                case 3:
//                    return "早退";
//                default:
//                    return "";
//            }
//        }

//        public void start( ref System.Windows.Controls.DataGrid datagird)
//        {
//            dataGird = datagird;

//            course = CurrentCourse.getInstance();
//            dt_start = DateTime.Parse(course.getStartTime());
//            dt_end = DateTime.Parse(course.getEndTime());
//            started = true;

//            m_SyncContext = SynchronizationContext.Current;
//           // show();
//            uploadtime = dt_start;
//            uploadtime.AddMinutes(5);
//            if (thread == null)
//            {
//                thread = new Thread(threadProc);
//                thread.Start();
                
//            }

//        }

//        public void end()
//        {
//            if(started)
//            {
//                thread.Abort();
//            }
//            new UpLoad().checkin_file(course.getCourseId(), string.Format("{0:yyyyMMdd}", DateTime.Today), "123456");
//        }
//    }
//}
