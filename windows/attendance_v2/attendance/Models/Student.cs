using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Author:汪京陆
 * Date:2016/8/20
 * Description:
 *          将attendanceManagement.cs中Student类转移到此文件中
 *          将原有以整数类型的学生到课情况（check） 变为 attendanceManagement.Models.CheckStatus
 * 
 * */

namespace attendanceManagement.Models
{
    /// <summary>
    /// 学生类
    /// 保存学生个人信息
    /// </summary>
    class Student
    {
        public string name { get; set; }
        public string id { get; set; }
        public string college { get; set; }
        public string major { get; set; }
        public string sex { get; set; }
        public string mac { get; set; }
        public string sclass { get; set; }
        public string time { get; set; }



        //int check = 0;     

        //public int CHECK
        //{
        //    get { return check; }
        //    set
        //    {
        //        check = value;
        //    }
        //}
        //public string arrive
        //{
        //    set { }
        //    get
        //    {
        //        switch (check)
        //        {
        //            case 0:
        //                return "未到";
        //            case 1:
        //                return "到课";
        //            case 2:
        //                return "迟到";
        //            case 3:
        //                return "早退";
        //            default:
        //                return "未到";
        //        }
        //    }
        //}


        CheckStatus check = CheckStatus.ABSENCE;
        public CheckStatus CHECK
        {
            get { return check; }
            set { check = value; }
        }

        public string arrive
        {
            get { return check.ToString(); }
        }

        public string CheckCode
        {
            get { return check.code(); }
        }


        //修改状态
        public void changeState()
        {
            if (check == CheckStatus.ABSENCE)
                check = CheckStatus.ARRIVED;
            else
                check = CheckStatus.ABSENCE;
        }

    }

}
