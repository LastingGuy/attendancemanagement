using System.Collections.Generic;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;
using System;
using attendanceManagement.Models;

namespace attendanceManagement.XML
{

    /**************************************************************
     * Author:汪京陆
     * Date:2016/8/10
     * Description:对xml文件的解析和保存
     **************************************************************/
    class CourseInfo
    {

        /// <summary>
        ///获取教师课程表
        ///解析 DIR.coursesdir 路径中内容 
        /// </summary>
        /// <returns>课程集合</returns>
        public static LinkedList<Course> getCourses()
        {
            var list = new LinkedList<Course>();
            if (!File.Exists(DIR.COURSES))
                return list;
            
            var dom = XDocument.Load(DIR.COURSES);
            var root = dom.Root;
            

            foreach(var courseElement in root.Elements())
            {
                Course course = new Course();
                course.COURSENAME = courseElement.Element("name").Value;
                course.COURSEID = courseElement.Element("id").Value;
                course.TEACHERNAME = courseElement.Element("teacher").Value;
                course.NROFSTU = Convert.ToInt32(courseElement.Element("nrofstu").Value);
                foreach(var time in courseElement.Element("date").Elements())
                {
                    course.add_date(time.Element("t").Value, time.Element("w").Value);
                }
                list.AddLast(course);
            }

            return list;
        }


       /// <summary>
       /// 获取历史纪录
       /// 根据历史文件夹路径，遍历 history/<courseid>/ 文件夹中所有文件， 只返回文件名链表，不解析xml文件
       /// </summary>
       /// <param name="path">文件路径</param>
       /// <returns>历史记录集合</returns>
        public static LinkedList<CheckingTable> getHistory(string courseid)
        {
            LinkedList<CheckingTable> history = new LinkedList<CheckingTable>();

            try
            {
                string path = DIR.HISTORY + courseid + "/";
                //获得文件夹信息
                DirectoryInfo courses = new DirectoryInfo(path);
                FileSystemInfo[] list = courses.GetFileSystemInfos();

                //遍历文件
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].Extension.Equals(".xml"))
                    {
                        
                        string filename = list[i].Name.Replace(".xml", "");
                        CheckingTable data = CheckingTable.getFromFile(filename,courseid);
                        history.AddFirst(data);
                    }
                }

            }
            catch (Exception e)
            {
                return history;
            }

            return history;
        }


        /// <summary>
        /// 获取历史考勤表
        /// </summary>
        /// <param name="path">考勤表路径</param>
        /// <returns></returns>
        public static CheckingTable getHistoryTable(CheckingTable table)
        {
            //List<Student> table = new List<Student>();
            string path = DIR.HISTORY + table.courseID + "/" + table.filename + ".xml";
            if(File.Exists(path))
            {
                var dom = XDocument.Load(path);
                var root = dom.Root;

                table.ts = root.Element("ts").Value;
                table.te = root.Element("te").Value;

                foreach(var item in root.Element("students").Elements())
                {
                    Student stu = new Student();
                    stu.id = item.Element("id").Value;

                    //stu.CHECK = Convert.ToInt32(item.Element("check").Value);
                    stu.CHECK = CheckStatus.parseCode(item.Element("check").Value);

                    stu.time = item.Element("t").Value;

                    table.students.Add(stu);
                }

            }
            return table;
        }


        /// <summary>
        /// 获取学生名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Student> getStudents(string id)
        {
            List<Student> students = new List<Student>();
            string path = DIR.STULIST + id + ".xml";

            if(File.Exists(path))
            {
                var dom = XDocument.Load(path);
                var root = dom.Root;
                foreach(var item in root.Elements())
                {
                    Student stu = new Student();
                    stu.name = item.Element("name").Value;
                    stu.id = item.Element("id").Value;
                    stu.mac = item.Element("mac").Value;
                    stu.major = item.Element("major").Value;
                    stu.sex = item.Element("sex").Value;
                    stu.sclass = item.Element("sclass").Value;
                    stu.college = item.Element("college").Value;

                    students.Add(stu);
                }
            }
            return students;
        }


        /// <summary>
        /// 设置上课时间
        /// </summary>
        /// <param name="cid">课程id</param>
        /// <param name="dates">添加的日期</param>
        /// <returns></returns>
        public static bool setTimes(string cid,LinkedList<CourseDate> dates)
        {

            var dom = XDocument.Load(DIR.COURSES);
            var root = dom.Root;

            foreach(var course in root.Elements())
            {
                if(cid == course.Element("id").Value)
                {
                    XElement e = new XElement("date");
                    foreach(var date in dates)
                    {
                        e.Add(new XElement("time",
                            new XElement("w", date.get_week()),
                            new XElement("t", date.get_start())
                            ));
                    }

                    course.Element("date").ReplaceWith(e);
                    dom.Save(DIR.COURSES);
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 保存考勤表
        /// </summary>
        /// <param name="table">考勤表</param>
        /// <returns></returns>
        public static bool saveAttendancetable(CheckingTable table)
        {
            bool result = false;

            XDocument dom = new XDocument();
            XElement root = new XElement("course");

            XElement courseid = new XElement("courseid", table.courseID);
            XElement date = new XElement("date", table.date);
            XElement ts = new XElement("ts",table.ts);
            XElement te = new XElement("te",table.te);

            XElement students = new XElement("students");
            foreach(var student in table.students)
            {
                XElement stu = new XElement("stu",
                    new XElement("id", student.id),
                    new XElement("name",student.name),
                    new XElement("mac",student.mac),
                    new XElement("college",student.college),
                    new XElement("major",student.major),
                    new XElement("sclass",student.sclass),
                    new XElement("sex",student.sex),
                    new XElement("check",student.CheckCode),
                    new XElement("t",student.time)
                    );

                students.Add(stu);
            }

            root.Add(courseid, date, ts, te, students);
            dom.Add(root);

            //保存至history文件夹
            dom.Save(DIR.safedir(DIR.HISTORY + table.courseID, table.date+ table.ts.Replace(":","") + ".xml"));
            //保存至upload文件夹
            dom.Save(DIR.safedir(DIR.UPLOAD , table.courseID + table.date + table.ts.Replace(":", "") + ".xml"));

            return result;
        }
    }
}
