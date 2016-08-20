using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    struct DIR
    {
        //根目录
        public const string ROOT = "courses/";
        //名单目录
        public const string STULIST = ROOT + "stulist/";
        //历史目录
        public const string HISTORY = ROOT + "results/history/";
        //上传目录
        public const string UPLOAD = ROOT + "results/upload/";
        //课表文件
        public const string COURSES = ROOT + "courses.xml";
    };
}
