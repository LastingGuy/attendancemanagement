using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using attendanceManagement.Models;
using System.IO;

namespace attendanceManagement.NET
{
    class DownLoad : NET
    {
        public string getstulist(string classid, string key)
        {
            try
            {
                WebClient client = new WebClient();
                string post = "?classid=" + classid + "&key=" + key;
                client.DownloadFile(URL.getstulist_dir + post, "list/list.xml");
                return client.DownloadString(URL.getclasslist_dir + post);
            }
            catch (WebException e)
            {
                return "";
            }
           
        }
        public bool getclasslist()
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers.Set("Cookie", Teacher.cookie);
                string result = client.DownloadString(URL_GETCOURSE);
                if (result == "\"error\"")
                    return false;
                else
                {
                    var file = File.Open(DIR.COURSES,FileMode.CreateNew);
                    file.Write(Encoding.UTF8.GetBytes(result), 0, Encoding.UTF8.GetByteCount(result));
                    file.Close();
                    return true;
                }
            }
            catch (WebException e)
            {
                return false;
            }
        }
    }
}
