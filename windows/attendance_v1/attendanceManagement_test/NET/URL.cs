using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace attendanceManagement.NET
{
    class URL
    {
        private const string root = "http://jingl.wang/attendance";
        private const string php_dir = root + "/php";
        private const string api_dir = php_dir + "/api";
        private const string download_dir = api_dir + "/download";
        private const string checkin_dir = api_dir + "/checkin";

        //下载
        public const string getclasslist_dir = download_dir + "/getclasslist.php";
        public const string getstulist_dir = download_dir + "/getstulist.php";


        //check in
        public const string checkin_one_dir = checkin_dir + "/checkin_one_stu.php";
        public const string checkin_file_dir = checkin_dir + "/checkinbyfile.php";
        public const string checkin_string_dir = checkin_dir + "/checkinbystring.php";


    }
}
