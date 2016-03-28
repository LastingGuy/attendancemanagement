using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace attendanceManagement.NET
{
    class NET
    {
        public void addPostData(Stream stream,string name,string value,string boundary)
        {
            StringBuilder header = new StringBuilder(string.Format("Content-Disposition: form-data;name=\"{0}\"\r\n\r\n", name));
            byte[] header_byte = Encoding.UTF8.GetBytes(header.ToString());

            byte[] value_byte = Encoding.UTF8.GetBytes(value);

            
           
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            stream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            stream.Write(header_byte, 0, header_byte.Length);
            stream.Write(value_byte, 0, value_byte.Length);
           // stream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            

        }
    }
}
