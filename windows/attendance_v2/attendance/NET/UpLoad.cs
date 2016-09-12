using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using attendanceManagement.Models;

namespace attendanceManagement.NET
{
    class UpLoad:NET
    {
        public bool login(string tid,string passwd)
        {
            string _result;
            string _cookie;
            try
            {
                HttpWebRequest request = WebRequest.Create(URL_Login) as HttpWebRequest;
                CookieContainer cookie = new CookieContainer();


                request.CookieContainer = cookie;
                request.AllowAutoRedirect = true;
                request.Method = "POST";

                string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

                Stream postStream = request.GetRequestStream();

                //构建post数据
                postBegin(postStream, boundary);
                addPostData("username", tid);
                addPostData("password", passwd);
                addPostData("type", "1");
                postEnd();


                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                stream.ReadTimeout = 15 * 1000;
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                _result = reader.ReadToEnd();
                _cookie = cookie.GetCookieHeader(request.RequestUri);
                if(_result.Trim()=="\"2\"")
                {
                    Teacher.cookie = _cookie;
                    Teacher.tid = tid;
                    Teacher.passwd = passwd;
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public bool uploadTable()
        {
            try
            {
                foreach (var file in DIR.getFiles(DIR.UPLOAD))
                {
                    string path = file.FullName;
                    // 设置参数
                    HttpWebRequest request = WebRequest.Create(URL_UPLOAD) as HttpWebRequest;
                    CookieContainer cookieContainer = new CookieContainer();

                    request.Headers.Set("Cookie", Teacher.cookie);
                    request.AllowAutoRedirect = true;
                    request.Method = "POST";

                    string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                    request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

                    Stream postStream = request.GetRequestStream();

                    string filename = file.Name.Replace(".xml", "").Substring(0, 8);
                    postBegin(postStream, boundary);
                    addPostFile("file", path,filename);
                    postEnd();


                    //发送请求并获取相应回应数据
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //直到request.GetResponse()程序才开始向目标网页发送Post请求
                    Stream instream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                    //返回结果网页（html）代码
                    string content = sr.ReadToEnd();
                    if(content.Trim()=="success")
                    {
                        DIR.delete(path);
                    }
                }
               
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
