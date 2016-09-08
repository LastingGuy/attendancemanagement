using attendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Exceptions
{
    class ErrorBroadcast
    {
        public static void error(string message)
        {
            MainwindowData.data.Window.errorBoard(message);
        }
    }
}
