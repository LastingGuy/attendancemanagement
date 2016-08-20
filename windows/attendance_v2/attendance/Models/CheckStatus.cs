using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************
 * Author:汪京陆
 * Date：2016/8/20
 * Description:
 *      表示学生到课情况
 *********************************************/
namespace attendanceManagement.Models
{

    class CheckStatus
    {
        enum status
        {
            absence = 0,    //未到
            arrived = 1     //已到
        }

        
        status state = status.absence;
        string msg = "";

        CheckStatus(status state,string msg) { this.state = state; this.msg = msg; }
       
      
        //缺席
        public static CheckStatus ABSENCE { get { return new CheckStatus(status.absence,"未到"); } }

        //到课
        public static CheckStatus ARRIVED { get { return new CheckStatus(status.arrived,"已到"); } }

        //判断是否到课
        public bool isArrived()
        {
            return state == status.arrived;
        }

        //状态
        override public string ToString()
        {
            return msg;
        }

        /// <summary>
        ///状态代码
        /// 1 为已到   0 为未到
        /// </summary>
        public string code()
        {
            switch(state)
            {
                case status.absence:
                    return "0";
                case status.arrived:
                    return "1";
                default:
                    return "0";
            }
        }

        //转换状态代码
        public static CheckStatus parseCode(string code)
        {
            if (code == "1")
                return ABSENCE;
            else
                return ARRIVED;
        }

        /**************************
         * 比较操作符
         * ************************/
        public static bool operator==(CheckStatus _this,CheckStatus another)
        {
            return _this.state == another.state;
        }

        public static bool operator!=(CheckStatus _this,CheckStatus another)
        {
            return !(_this == another);
        }

        public override bool Equals(object obj)
        {
            return this == (CheckStatus)obj;
        }
    }
}
