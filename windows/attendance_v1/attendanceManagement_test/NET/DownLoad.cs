using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
        public string getclasslist(string teacherid)
        {
            try
            {
                WebClient client = new WebClient();
                string post = "?teacherid=" + teacherid;
                client.DownloadFile(URL.getclasslist_dir + post, "list/classes.xml");
                return client.DownloadString(URL.getclasslist_dir + post);
            }
            catch (WebException e)
            {
                return "";
            }
        }
    }
}
