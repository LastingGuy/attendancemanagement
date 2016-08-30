using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    /// <summary>
    /// 上课时间
    /// </summary>
    public class CourseDate
    {
        public string start = "";
        public string week = "";
        public CourseDate(string start, string week)
        {
            this.start = start;
            this.week = week;
        }
        override public string ToString()
        {
            return "周" + week + "   " + start;
        }

        public string get_start()
        {
            return start;
        }

        public string get_week()
        {
            return week;
        }
    }
}
