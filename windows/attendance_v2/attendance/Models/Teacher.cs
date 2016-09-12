using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    class Teacher
    {
        public static bool remember { get; set; } = true;
        public static bool isLogin { get; set; } = false;
        public static string LoginState
        {
            get
            {
                return isLogin ? "true" : "false";
            }
        }
        public static string cookie;
        public static string tid;
        public static string passwd;
        public static string md5;
        public static string defaultWifiName="";
        public static string defaultWifiPass="";
        

    }
}
