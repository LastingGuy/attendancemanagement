using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.XML
{
    class Course
    {
        //课程id,课程名，教师名，人数，上课时间
        string course_id = "";
        string course_name = "";
        string teacher_name = "";
        string teacher_id = "";
        
        int number = 0;
        int nrOfdates = 0;
        //当前课程所在的xml文件路径
        string filepath = "";

        LinkedList<CourseDate> dates = new LinkedList<CourseDate>();

        public Course()
        {
        }

        public void set_course_id(string course_id)
        {
            this.course_id = course_id;
        }
        public void set_course_name(string course_name)
        {
            this.course_name = course_name;
        }
        public void set_teacher_name(string teacher_name)
        {
            this.teacher_name = teacher_name;
        }
        public void set_teacher_id(string teacher_id)
        {
            this.teacher_id = teacher_id;
        }
        public void set_number(int number)
        {
            this.number = number;
        }
        public void set_filepath(string filepath)
        {
            this.filepath = filepath;
        }
        public void nrOfDatesAdd()
        {
            nrOfdates++;
        }
        public void add_date(string start, string end, string week)
        {
            CourseDate date = new CourseDate(start, end, week);
            dates.AddFirst(date);
        }
        public string get_course_id()
        {
            return course_id;
        }
        public string get_course_name()
        {
            return course_name;
        }      
        public string get_teacher_name()
        {
            return teacher_name;
        }
        public string get_teacher_id()
        {
            return teacher_id;
        }
        public string get_filepath()
        {
            return this.filepath;
        }
        public LinkedList<CourseDate> get_dates()
        {
            return dates;
        }
        public int get_number()
        {
            return number;
        }
    }
}
