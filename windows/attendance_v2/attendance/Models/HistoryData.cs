using attendanceManagement.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    /// <summary>
    /// 历史记录
    /// </summary>
    class HistoryData
    {
        public string path  ="";
        public string date = "";
        public string time = "";

        public List<Student> table
        {
            get
            {
                return new List<Student>();
            }
        }
    }
}
