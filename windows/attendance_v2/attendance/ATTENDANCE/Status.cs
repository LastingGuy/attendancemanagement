using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.ATTENDANCE
{
    /*****************************************************************
     * Author:汪京陆
     * Date:2016/8/19
     * Description:
     *       表示WiFIOPERATE中操作结果
     * 
     * ***************************************************************/
    class Status
    {
        enum errors
        {
            FAIL = 0,                               //操作失败
            SUCCESS = 1,                            //成功
            FAIL_OPEN_HANDLE = 2,                   //打开句柄失败
            VERSION_IS_LOW = 3,                     //版本过低
            FAIL_TO_SET_ALLOW = 4,                  //热点设置失败
            FAIL_TO_SET_SSID = 5,                   //设置ssid失败
            FAIL_TO_SET_PASS = 6,                   //设置密码失败
            FAIL_TO_START_USING = 7,                //无法启用设置
            FAIL_TO_START_NETWORK = 8               //无法开启负载网络
        }

        private errors state = errors.FAIL;

        Status(errors code)
        {
            state = code;
        }



        public bool isOK()
        {
            return state == errors.SUCCESS;
        }





        public static Status OK { get { return new Status(errors.SUCCESS); } }

        public static Status FAIL { get { return new Status(errors.FAIL); } }

        public static Status FAIL_TO_OPEN_HANDLE { get { return new Status(errors.FAIL_OPEN_HANDLE); } }

        public static Status VERSION_LOW { get { return new Status(errors.VERSION_IS_LOW); } }

        public static Status FAIL_TO_SET_ALLOW { get { return new Status(errors.FAIL_TO_SET_ALLOW); } }



        //get the code of state
        public int code() { return Convert.ToInt32(state); }
        public string ToString() { return code().ToString(); }

    }
}
