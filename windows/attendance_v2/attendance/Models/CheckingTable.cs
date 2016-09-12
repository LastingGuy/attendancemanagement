using attendanceManagement.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    struct  CheckingTable
    {
        public string filename;
        public string courseID;
        public string date;
        public string ts;
        public string te;

        public List<Student> students;

        public List<Student> absenceStudents
        {
            get
            {
                List<Student> list = new List<Student>();
                try
                {
                    
                    foreach (Student stu in students)
                    {
                        if (!stu.CHECK.isArrived())
                        {
                            list.Add(stu);
                        }
                    }
                }
                catch(Exception e)
                {
                    System.Console.Write(e.Message);
                }
                return list;
            }
        }

        /// <summary>
        /// 构造考勤表不导入学生名单
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="date"></param>
        /// <param name="ts"></param>
        /// <param name="te"></param>
        public CheckingTable(string courseid,string date,string ts,string te="")
        {
            filename = date + ts.Replace(":", "");
            courseID = courseid;
            this.date = date;
            this.ts = ts;
            this.te = te;

            students = CourseInfo.getStudents(courseid);
        }


        public CheckingTable(string courseid, string date)
        {
            filename = "";
            courseID = courseid;
            this.date = date;
            ts = "";
            te = "";

            students = new List<Student>();
        }

        public static CheckingTable getFromFile(string filename,string courseid)
        {
            CheckingTable temp = new CheckingTable();
            temp.filename = filename;
            temp.courseID = courseid;
            temp.date = filename.Substring(0, 8);
            temp.ts = "";
            temp.te = "";
            temp.students = new List<Student>();

            return temp;
        }

        public int arrived
        {
            get
            {
                if (students == null)
                    return 0;
                else
                {
                    int count = 0;
                    foreach (var stu in students)
                    {
                        if (stu.CHECK.isArrived())
                            count++;
                    }
                    return count;
                }
            }
        }

        public int nrOfstu
        {
            get
            {
                if (students == null)
                    return 0;
                else
                    return students.Count;
            }
        }
    }
}
