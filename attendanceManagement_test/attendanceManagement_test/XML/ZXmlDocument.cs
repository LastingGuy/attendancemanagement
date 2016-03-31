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
            listNodes = root.SelectNodes("/lesson/lesson_id");
            course.set_lesson_id(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/lesson/lesson_name");
            course.set_lesson_name(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/lesson/teacher_name");
            course.set_teacher_name(listNodes[0].InnerText);
            listNodes = root.SelectNodes("/lesson/stu_nr");
            course.set_number(Convert.ToInt32(listNodes[0].InnerText));
            listNodes = root.SelectNodes("/lesson/date");
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

    }
}
