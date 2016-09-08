using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Exceptions
{
    class FileDamagedException:ApplicationException
    {
        //文件损坏异常
        public void error()
        {
            ErrorBroadcast.error("文件损坏,请重新同步！");
        }
    }
}
