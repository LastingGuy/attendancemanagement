using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;



namespace attendanceManagement.XML
{
    class ZXmlDocument
    {
        XmlDocument doc;
        XmlElement root;
        Course course = null;
        public ZXmlDocument(string filename)
        {
            doc = new XmlDocument();
            doc.Load(filename);
            root = doc.DocumentElement;
        }

        //得到课程
        public Course getCourse()
        {
            if (course != null)
                return course;

            course = new Course();
            XmlNodeList listNodes = null;
            listNodes = root.SelectNodes("/course/course_id");
            course.set_course_id(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/course/course_name");
            course.set_course_name(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/course/teacher_name");
            course.set_teacher_name(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/course/teacher_id");
            course.set_teacher_id(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/course/stu_nr");
            course.set_number(Convert.ToInt32(listNodes[0].InnerText));
            listNodes = root.SelectNodes("/course/date");
            foreach (XmlNode node in listNodes)
            {
                string start = ((XmlElement)(((XmlElement)node).GetElementsByTagName("start"))[0]).InnerText;
                string end = ((XmlElement)(((XmlElement)node).GetElementsByTagName("end"))[0]).InnerText;
                string week = ((XmlElement)(((XmlElement)node).GetElementsByTagName("week"))[0]).InnerText;
                course.add_date(start, end, week);
                course.nrOfDatesAdd();
            }
            return course;

        }

        //得到某教师所有的课程号  用于下载课程表
        public String[] getClassIds()
        {
            String[] classids = null;
            XmlNodeList listNodes = null;
            listNodes = root.SelectNodes("/classes/classid");
            int number = 0;
            foreach (XmlNode node in listNodes)
            {
                number++;
            }
            classids = new String[number];
            int i = 0;
            foreach (XmlNode node in listNodes)
            {

                classids[i] = node.InnerText;
                i++;
            }
            return classids;
        }

        //设置当前的考勤课程，需要生成ZXmlDocumnt的实例，然后调用。
        public void setCurrentCourse(Course course, CourseDate date)
        {
            //设置课程有关信息
            String courseId;
            String courseName;
            String teacherId;
            String teacherName;
            int studentNr;
            CurrentCourse currentCourse = CurrentCourse.getInstance();
            courseId = course.get_course_id();
            courseName = course.get_course_name();
            teacherId = course.get_teacher_id();
            teacherName = course.get_teacher_name();
            studentNr = course.get_number();
            currentCourse.setCourse(courseId, courseName, teacherId, teacherName, studentNr);

            //设置时间信息
            String start = "";
            String end = "";
            String week = "";
            start = date.get_start();
            end = date.get_end();
            week = date.get_week();
            currentCourse.setTime(week,start,end);

            //从xml文件中读取学生的信息
            XmlNodeList listNodes = root.SelectNodes("/course/students/stu");
            int i = 0;
            foreach(XmlNode node in listNodes)
            {
                XmlNodeList stuNodes = node.SelectNodes("stu_name");
                String name = stuNodes[0].InnerText;
                stuNodes = node.SelectNodes("stu_id");
                String id = stuNodes[0].InnerText;
               
                String college = null, major = null, sex = null;
                //xml中还未设置这些属性
                //stuNodes = node.SelectNodes("stu_college");
                //college = stuNodes[0].InnerText;

                //stuNodes = node.SelectNodes("stu_major");
                //major = stuNodes[0].InnerText;

                //stuNodes = node.SelectNodes("stu_sex");
                //sex = stuNodes[0].InnerText;

                stuNodes = node.SelectNodes("mac_adr");
                String macAdr = stuNodes[0].InnerText;

                currentCourse.students[i] = new StudentInfo(name,id,college,major,sex,macAdr);
                i++;
            }         
        }

        public static void generateResultXml()
        {
            //建立考勤结果XML        
            XmlDocument newDoc = new XmlDocument();
            XmlNode node = newDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            newDoc.AppendChild(node);

            XmlNode root = newDoc.CreateElement("students");
            newDoc.AppendChild(root);

            foreach (XmlNode anode in listNodes)
            {
                XmlElement stu = newDoc.CreateElement("stu");
                XmlElement stu_id = newDoc.CreateElement("stuid");
                XmlElement stu_ck = newDoc.CreateElement("ck");
                XmlElement stu_ts = newDoc.CreateElement("ts");
                XmlElement stu_te = newDoc.CreateElement("te");

                stu_id.InnerText = anode.InnerText;

                stu.AppendChild(stu_id);
                stu.AppendChild(stu_ck);
                stu.AppendChild(stu_ts);
                stu.AppendChild(stu_te);

                root.AppendChild(stu);
            }
            newDoc.Save("result.xml");
        }

    }
            
}
