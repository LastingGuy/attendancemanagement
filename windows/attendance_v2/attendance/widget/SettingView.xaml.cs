using attendanceManagement.Models;
using attendanceManagement.XML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace attendanceManagement.widget
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : UserControl
    {
        private SynchronizationContext m_SyncContext = null;

        public SettingView()
        {
            InitializeComponent();
           
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void btn_clean_Click(object sender, RoutedEventArgs e)
        {
            clean_process.Visibility = Visibility.Visible;
            Thread t = new Thread(clean_thread);
            m_SyncContext = SynchronizationContext.Current;
            t.Start();
        }

        private void clean_thread()
        {
            m_SyncContext.Post(clean_process_operate, true);
            DIR.delete(DIR.ROOT);
            m_SyncContext.Post(reload, null);
            m_SyncContext.Post(clean_process_operate, false);
        }

        private void clean_process_operate(object flag)
        {
            if((bool)flag)
                clean_process.Visibility = Visibility.Visible;
            else
                clean_process.Visibility = Visibility.Hidden;
        }
        private void reload(object flag)
        {
            MainwindowData.data.courselist = CourseInfo.getCourses();
        }

        private void wifiname_LostFocus(object sender, RoutedEventArgs e)
        {
            Teacher.defaultWifiName = wifiname.Text;
            MainwindowData.data.WIFIName = Teacher.defaultWifiName;
            CourseInfo.saveConfig();
        }

        private void wifipassword_LostFocus(object sender, RoutedEventArgs e)
        {
            Teacher.defaultWifiPass = wifipassword.Password;
            MainwindowData.data.WIFIPass = Teacher.defaultWifiPass;
            CourseInfo.saveConfig();
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            wifiname.Text = Teacher.defaultWifiName != "" ? Teacher.defaultWifiName : "";
            wifipassword.Password = Teacher.defaultWifiPass != "" ? Teacher.defaultWifiPass : "12345678";
        }
    }
}
