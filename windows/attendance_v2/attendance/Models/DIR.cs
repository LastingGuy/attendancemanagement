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
        public const string ROOT = "courses/";
        //名单目录
        public const string STULIST = ROOT + "stulist/";
        //历史目录
        public const string HISTORY = ROOT + "results/history/";
        //上传目录
        public const string UPLOAD = ROOT + "results/upload/";
        //课表文件
        public const string COURSES = ROOT + "courses.xml";

        /// <summary>
        /// 检测文件夹是否存在，如果不存在，则新建文件夹，防止文件夹不存在而报错
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="filename">文件名</param>
        /// <returns>完整路径，即path+filename</returns>
        public static string safedir(string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();
            }
            return path + "/" + filename;
        }
    };
}
