using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace attendanceManagement.NET
{
    class UpLoad:NET
    {
        public bool checkin_file(string classid,string date,string key)
        {
           
            string path = "db\\"+date+".xml";
            // 设置参数
            HttpWebRequest request = WebRequest.Create(URL.checkin_file_dir) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";

            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
           
            Stream postStream = request.GetRequestStream();
        
            postBegin(postStream,boundary);
            addPostData("cid", classid);
            addPostData("key", key);
            addPostData("date", date);
            addPostFile("file", path);
            postEnd();

            try
            {
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
               
            }
            catch(Exception e)
            { return false; }

            return true;
        }


        public bool checkin_one_stu(string classid, string date, string sid, string ck, string ts, string te, string key)
        {

            HttpWebRequest request = WebRequest.Create(URL.checkin_one_dir) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";

            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

            Stream postStream = request.GetRequestStream();

            postBegin(postStream, boundary);
            addPostData("cid", classid);
            addPostData("date", date);
            addPostData("sid", sid);
            addPostData("ck", ck);
            addPostData("ts", ts);
            addPostData("te", te);
            addPostData("key", key);
            postEnd();

            try
            {
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();

            }
            catch (Exception e)
            { return false; }

            return true;
        }
    }
}
