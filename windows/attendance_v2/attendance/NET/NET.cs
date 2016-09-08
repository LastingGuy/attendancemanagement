using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace attendanceManagement.NET
{
    class NET:URL
    {


/// <summary>
///     写入post数据
/// 
///     以postBegin（）  开始   参数为Stream类型
///     以postEnd()  结束
/// ///////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        private string boundary;
        private Stream stream;

        //初始化post写入
        protected void postBegin(Stream stream,string boundary)
        {
            
            this.stream = stream;
            this.boundary = boundary; 
        }

        //向流中写入post数据
        protected void addPostData(string name,string value)
        {
            StringBuilder header = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"{0}\"\r\n\r\n", name));
            byte[] header_byte = Encoding.UTF8.GetBytes(header.ToString());
            byte[] value_byte = Encoding.UTF8.GetBytes(value);

            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");

            stream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            stream.Write(header_byte, 0, header_byte.Length);
            stream.Write(value_byte, 0, value_byte.Length);          
        }

        //post  提交文件
        protected void addPostFile(string key,string path,string filename="")
        {
            if (filename == "")
                filename = path;
            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] contents = new byte[fs.Length];

            fs.Read(contents, 0, contents.Length);
            fs.Close();

            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:text/xml\r\n\r\n", key, fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            stream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            stream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            stream.Write(contents, 0, contents.Length);
        }

        //结束post数据写入
        protected void postEnd()
        {
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            stream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            stream.Close();
        }


/////////////////////////////////////////////////////////////////////////////////////



    }
}
