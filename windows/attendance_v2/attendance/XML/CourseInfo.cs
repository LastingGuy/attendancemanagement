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
        public static LinkedList<HistoryData> getHistory(string path)
        {
            LinkedList<HistoryData> history = new LinkedList<HistoryData>();

            try
            {
                //获得文件夹信息
                DirectoryInfo courses = new DirectoryInfo(path);
                FileSystemInfo[] list = courses.GetFileSystemInfos();

                //遍历文件
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].Extension.Equals(".xml"))
                    {
                        HistoryData data = new HistoryData();
                        string str = list[i].Name.Replace(".xml", "");
                        data.date = str;
                        data.path = path;
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
        public static List<Student> getHistoryTable(string path)
        {
            List<Student> table = new List<Student>();
            if(File.Exists(path))
            {
                var dom = XDocument.Load(path);
                var root = dom.Root;

                foreach(var item in root.Element("students").Elements())
                {
                    Student stu = new Student();
                    stu.id = item.Element("id").Value;

                    //stu.CHECK = Convert.ToInt32(item.Element("check").Value);
                    stu.CHECK = CheckStatus.parseCode(item.Element("check").Value);

                    stu.time = item.Element("t").Value;

                    table.Add(stu);
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
    }
}
