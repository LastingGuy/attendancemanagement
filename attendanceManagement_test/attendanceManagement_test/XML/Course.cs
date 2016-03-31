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
        string lesson_id = "";
        string lesson_name = "";
        string teacher_name = "";
        int number = 0;
        int nrOfdates = 0;
        LinkedList<CourseDate> dates = new LinkedList<CourseDate>();

        public Course()
        {
        }

        public void set_lesson_id(string lesson_id)
        {
            this.lesson_id = lesson_id;
        }
        public void set_lesson_name(string lesson_name)
        {
            this.lesson_name = lesson_name;
        }
        public void set_teacher_name(string teacher_name)
        {
            this.teacher_name = teacher_name;
        }
        public void set_number(int number)
        {
            this.number = number;
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
        public string get_lession_id()
        {
            return lesson_id;
        }
        public string get_lesson_name()
        {
            return lesson_name;
        }
        public string get_teacher_name()
        {
            return teacher_name;
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
