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
        public bool getstulist(string courseid)
        {
            try
            {
                WebClient client = new WebClient();
                string post = "?course_id=" + courseid;
                client.Headers.Set("Cookie", Teacher.cookie);
                string result = client.DownloadString(URL_GETSTULIST + post);

                if (result != "\"error\"" || result != "")
                {
                    var file = File.Create(DIR.STULIST + "/" + courseid + ".xml");
                    file.Write(Encoding.UTF8.GetBytes(result), 0, Encoding.UTF8.GetByteCount(result));
                    file.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (WebException e)
            {
                return false;
            }
           
        }

        public bool getclasslist()
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers.Set("Cookie", Teacher.cookie);
                string result = client.DownloadString(URL_GETCOURSE);
                if (result == "\"error\"" || result == "")
                    return false;
                else
                {
                    var file = File.Create(DIR.COURSES);
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
