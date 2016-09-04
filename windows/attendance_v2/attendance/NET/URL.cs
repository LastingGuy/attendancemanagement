using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace attendanceManagement.NET
{
    class URL
    {
        private const string root = "http://xxbird.cn/attendance/index.php";
        private const string php_dir = root + "/php";
        private const string api_dir = php_dir + "/api";
        private const string download_dir = api_dir + "/download";
        private const string checkin_dir = api_dir + "/checkin";

        //下载
        protected const string getclasslist_dir = download_dir + "/getclasslist.php";
        protected const string getstulist_dir = download_dir + "/getstulist.php";


        //check in
        protected const string checkin_one_dir = checkin_dir + "/checkin_one_stu.php";
        protected const string checkin_file_dir = checkin_dir + "/checkinbyfile.php";
        protected const string checkin_string_dir = checkin_dir + "/checkinbystring.php";

        private const string background = root + "/background";
        private const string home = root + "/home";

        protected string URL_Login
        {
            get
            {
                return home + "/index/login";
            }
        }
        protected string URL_GETCOURSE
        {
            get { return background + "/Download/getCourse"; }
        }
    }
}
