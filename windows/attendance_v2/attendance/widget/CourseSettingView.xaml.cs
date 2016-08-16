using MahApps.Metro.Controls.Dialogs;
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
        //时间列表
        LinkedList<CourseDate> _dates;
        public LinkedList<CourseDate> DATES
        {
            set
            {
                _dates = value;
                DateListBox.Items.Clear();
                foreach(var date in value)
                {
                    ListBoxItem item = new ListBoxItem { Content = date.toString() };
                    DateListBox.Items.Add(item);
                }
            }
            get
            {
                return _dates;
            }
        }

        int index;
        int defaultindex;
        string defaulttime;

        public CourseSettingView()
        {
            InitializeComponent();
        }


        private void DateListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DateListBox.SelectedIndex;
            if (index != -1 && _dates != null)
            {

                index = DateListBox.SelectedIndex;
                defaultindex = Convert.ToInt32(DATES.ElementAt(index).get_week()) - 1;
                defaulttime = DATES.ElementAt(index).get_start();

                CourseDate date = DATES.ElementAt(index);
                day_selection.SelectedIndex = Convert.ToInt32(date.get_week()) - 1;
                TimeSpan time;
                if (TimeSpan.TryParse(date.get_start(), out time))
                {
                    TimePicker.SelectedTime = time;
                }

            }
        }

        private void day_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            if (day_selection.SelectedIndex == -1)
                return;

            TimeSpan time = TimePicker.SelectedTime.Value;
            string t = time.ToString(@"hh\:mm");

            if ((day_selection.SelectedIndex!=defaultindex||t!=defaulttime))
            {
                btn_check.Visibility = Visibility.Visible;
            }
            else
            {
                btn_check.Visibility = Visibility.Hidden;
            }
        }
        private void TimePicker_SelectionChanged(object sender, MahApps.Metro.Controls.TimePickerBaseSelectionChangedEventArgs<TimeSpan?> e)
        {
            if (day_selection.SelectedIndex == -1)
                return;

            TimeSpan time = TimePicker.SelectedTime.Value;
            string t = time.ToString(@"hh\:mm");

            if ((day_selection.SelectedIndex != defaultindex || t != defaulttime))
            {
                btn_check.Visibility = Visibility.Visible;
            }
            else
            {
                btn_check.Visibility = Visibility.Hidden;
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            MainwindowData.data.showTimeSetting = false;
            
        }

       
       
    }
}
