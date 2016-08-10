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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace attendanceManagement.widget
{
    /// <summary>
    /// CourseSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class CourseSettingView : UserControl
    {
        public CourseSettingView()
        {
            InitializeComponent();
        }

        private void day_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            if(day_selection.SelectedIndex!=-1&&time_selection.SelectedIndex!=-1)
            {
                btn_check.Visibility = Visibility.Visible;
            }
            else
            {
                btn_check.Visibility = Visibility.Hidden;
            }
        }

        private void time_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            if (day_selection.SelectedIndex != -1 && time_selection.SelectedIndex != -1)
            {
                btn_check.Visibility = Visibility.Visible;
            }
            else
            {
                btn_check.Visibility = Visibility.Hidden;
            }
        }
    }
}
