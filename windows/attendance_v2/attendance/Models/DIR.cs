using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    class DIR
    {

        //根目录
        static string _ROOT = "courses/";
        public static string ROOT
        {
            get { return safedir(_ROOT); }
        }
        //名单目录
        static string _STULIST = _ROOT + "stulist/";
        public static string STULIST
        {
            get { return safedir(_STULIST); }
        }
        //历史目录
        static string _HISTORY = _ROOT + "results/history/";
        public static string HISTORY
        {
            get { return safedir(_HISTORY); }
        }
        //上传目录
        static string _UPLOAD = _ROOT + "results/upload/";
        public static string UPLOAD
        {
            get { return safedir(_UPLOAD); }
        }
        //课表文件
        //static string _COURSES = ROOT + "courses.xml";
        public static string COURSES
        {
            get { return ROOT + "courses.xml"; }
        }
        //登陆信息
        public static string CONFIG
        {
            get { return "config.xml"; }
        }


        /// <summary>
        /// 检测文件夹是否存在，如果不存在，则新建文件夹，防止文件夹不存在而报错
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="filename">文件名</param>
        /// <returns>完整路径，即path+filename</returns>
        public static string safedir(string path, string filename)
        {
            //if (!Directory.Exists(path))
            //{
            //    DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //    directoryInfo.Create();
            //}
            path = safedir(path);
            return path + "/" + filename;
        }

        private static string safedir(string path)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();
            }
            return path;
        }
    };
}
