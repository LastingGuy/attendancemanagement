using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;


namespace attendanceManagement.widget
{
    /// <summary>
    /// Dialog.xaml 的交互逻辑
    /// </summary>
    public partial class CourseSettingDialog : CustomDialog
    {

        //时间列表
        public LinkedList<CourseDate> DATES
        {
            set
            {
                coursesettingview.DATES = value;
            }
        }

        public CourseSettingDialog()
        {
            InitializeComponent();
        }

    }
}
