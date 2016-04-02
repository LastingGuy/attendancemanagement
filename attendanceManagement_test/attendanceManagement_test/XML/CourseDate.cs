using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.XML
{
    class CourseDate
    {
        string start = "";
        string end = "";
        string week = "";
        public CourseDate(string start, string end, string week)
        {
            this.start = start;
            this.end = end;
            this.week = week;
        }
        public String toString()
        {
            return week + " " + start;
        }

        public String get_start()
        {
            return start;
        } 
        
        public String get_end()
        {
            return end;
        }

        public String get_week()
        {
            return week;
        }
    }
}
