using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using attendanceManagement.XML;


namespace attendanceManagement.ATTENDANCE
{
    class BTHOPERATE
    {
        static string result;
        static string[] macs;


        private static System.Windows.Controls.DataGrid dataGird;
        private static Thread thread=null;
        SynchronizationContext m_SyncContext = null;


        //线程函数
        private void threadProc()
        {
            while(true)
            {
                openBthProcess();

                getMacs();
                List<StudentInfo> list = checkAttendance();

                m_SyncContext.Post(syncdatagird, list);
                
                /* 

                此处写将考勤信息写入文件代码
                */
            }
        }

        //创建子bth进程
        private void openBthProcess()
        {
           
                var bth_process = new Process();
                bth_process.StartInfo.FileName = "bth.exe";
                bth_process.StartInfo.UseShellExecute = false;
                bth_process.StartInfo.CreateNoWindow = true;
                bth_process.StartInfo.RedirectStandardError = true;
                bth_process.StartInfo.RedirectStandardOutput = true;
                bth_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                bth_process.Start();

                result = bth_process.StandardOutput.ReadToEnd();
                string error = bth_process.StandardError.ReadToEnd();
                bth_process.WaitForExit();

        }


        private void getMacs()
        {
            const string pattern = "<([^>]*)>";
            
            MatchCollection macs_match = Regex.Matches(result, pattern);

            macs = new string[macs_match.Count];

            for (int n = 0;n< macs_match.Count;n++)
            {
                macs[n] = macs_match[n].Groups[1].Value;
            }

        }


        private List<StudentInfo> checkAttendance()
        {
            CurrentCourse course = CurrentCourse.getInstance();
            List<StudentInfo> list = course.getStudentList();
            for(int i =0;i<list.Count;i++)
            {
                

                for(int j =0; j<macs.Length;j++)
                {
                    if(list[i].macAdr.ToUpper()==macs[j].ToUpper())
                    {
                        if(list[i].check==0)
                        {
                            list[i].check = 1;
                            //list[i].checkattendance = "到课";
                            list[i].ts = DateTime.Now.ToLongTimeString().ToString();
                        }
                        else
                        {
                            list[i].te = DateTime.Now.ToLongTimeString().ToString();
                        }
                        break;
                    }
                    
                    if(j==macs.Length-1)
                    {
                        list[i].check = 0;
                        //list[i].checkattendance = "未到";
                        list[i].te = DateTime.Now.ToLongTimeString().ToString();
                    }
                }
                
            }


            //List<ATTENDANCEINFO> list = new List<ATTENDANCEINFO>();
            //for(int i =0;i<course.students.Length;i++)
            //{
            //    bool check = false;
              
                    
            //        for (int j = 0;j<macs.Length;j++)
            //        {
                       
            //            if(course.students[i].macAdr.ToUpper() == macs[j].ToUpper())
            //            {
            //                check = true;
            //                break;
            //            }
                      
            //        }
            //        if (check)
            //        {
            //        list.Add(new ATTENDANCEINFO
            //        {
            //            name = course.students[i].name,
            //            major = course.students[i].major,
            //            college = course.students[i].college,
            //            sclass = "",
            //            stuid = course.students[i].id,
            //            checkattendace = "到课"
            //            });
            //        }
            //        else
            //        {
            //            list.Add(new ATTENDANCEINFO
            //            {
            //                name = course.students[i].name,
            //                major = course.students[i].major,
            //                college = course.students[i].college,
            //                sclass = "",
            //                stuid = course.students[i].id,
            //                checkattendace = "未到"
            //            });
            //        }
               
            //}
           
    
            
            return list;
        }

        private void show()
        {
            CurrentCourse course = CurrentCourse.getInstance();
            List<StudentInfo> list = course.getStudentList();
            for (int n = 0; n < list.Count; n++)
            {
                list[n].check = 0;
                // list[n].checkattendance = "未到";
            }
            
            dataGird.ItemsSource = list;
            
        }

        private void syncdatagird(object data)
        {
            dataGird.ItemsSource = (List<StudentInfo>)data;
        }
     

        public void start( ref System.Windows.Controls.DataGrid datagird)
        {
            dataGird = datagird;

            m_SyncContext = SynchronizationContext.Current;
            show();
            if (thread == null)
            {
                thread = new Thread(threadProc);
                thread.Start();
            }

        }
    }
}
