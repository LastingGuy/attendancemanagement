using attendanceManagement.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    /// <summary>
    /// 课程类
    /// 
    /// 存储课程的基本信息
    /// 
    /// </summary>
    class Course
    {
        //课程id,课程名，教师名，人数，上课时间
        string course_id = "";
        public string COURSEID
        {
            set
            {
                course_id = value;
            }
            get
            {
                return course_id;
            }
        }

        string course_name = "";
        public string COURSENAME
        {
            set
            {
                course_name = value;
            }
            get
            {
                return course_name;
            }
        }

        string teacher_name = "";
        public string TEACHERNAME
        {
            set
            {
                teacher_name = value;
            }
            get
            {
                return teacher_name;
            }
        }

        string teacher_id = "";
        public string TEACHERID
        {
            set
            {
                teacher_id = value;
            }
            get
            {
                return teacher_id;
            }
        }

        int nrOfStu = 0;
        public int NROFSTU
        {
            set { nrOfStu = value; }
            get { return nrOfStu; }
        }

        //获得上课时间个数
        public int NROFDATES
        {
            get
            {
                return dates.Count;
            }
        }

        //当前课程所在的xml文件路径
        public string COURSEPATH
        {
            get
            {
                return DIR.HISTORY + course_id + "/";
            }
        }

        //学生名单
        List<Student> students;
        public List<Student> STUDENTS
        {
            get
            {
                if (students == null)
                    students = CourseInfo.getStudents(course_id);
                return students;
            }
        }

        //上课时间
        LinkedList<CourseDate> dates = new LinkedList<CourseDate>();
        public LinkedList<CourseDate> DATES
        {
            get
            {
                return dates;
            }
        }


        /// <summary>
        /// 获得历史纪录{get;}
        /// 利用CourseInfo.getHistory对COURSEPATH文件夹中xml文件进行解析，
        /// 获得一个LinkedList<HistoryData>类型的链表，并返回
        /// </summary>    
        public LinkedList<CheckingTable> HISTORY
        {
            get
            {
                var temp = CourseInfo.getHistory(course_id);
                return temp;
            }

        }


        /// <summary>
        /// 获得历史考勤表
        /// </summary>
        public CheckingTable table(int index)
        {
           
                var table = HISTORY.ElementAt(index);
                table = CourseInfo.getHistoryTable(table);
                int n = 0;

                while (n < table.students.Count)
                {
                    Student stu = table.students.ElementAt(n);
                    var stuinfo = getStudentInfo(stu.id);
                    if (stuinfo == null)
                    {
                        table.students.Remove(stu);
                        continue;
                    }

                    stu.name = stuinfo.name;
                    stu.major = stuinfo.major;
                    stu.sclass = stuinfo.sclass;
                    stu.college = stuinfo.college;

                    n++;
                }
                return table;
        }

        /// <summary>
        /// 获得历史考勤表上课人数
        /// </summary>
        public int nrofstu
        {
            get { return STUDENTS.Count; }
        }



        /// <summary>
        /// 保存上课时间直xml {get;}
        /// 复制为true保存
        /// </summary>
        public bool SaveTimeToXML
        {
            set
            {
                if (value)
                {
                    CourseInfo.setTimes(course_id, DATES);
                }
            }

        }


        /// <summary>
        /// 添加上课时间
        /// </summary>
        public void add_date(string start, string week)
        {
            CourseDate date = new CourseDate(start, week);
            dates.AddFirst(date);
        }//方法add_date结束


        //获得学生信息
        private Student getStudentInfo(string id)
        {
            foreach (Student stu in STUDENTS)
            {
                if (stu.id == id)
                {
                    return stu;
                }
            }

            return null;
        }//方法getStudentInfo结束
    }
}
