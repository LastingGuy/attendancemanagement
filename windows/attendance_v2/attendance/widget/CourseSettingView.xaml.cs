using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                foreach (var date in value)
                {
                    ListBoxItem item = new ListBoxItem { Content = date.ToString() };
                    DateListBox.Items.Add(item);
                }

                DateListBox.Items.Add(new ListBoxItem { Content = "新建" });
                DateListBox.Items.Refresh();
                DateListBox.SelectedIndex = 0;

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


        /// <summary>
        /// 日期选择框响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            /*
             * 响应ListBoxItem点击操作，
             * 如果DateListBox中存在Item，将相应item的星期、时间等信息填入day_selection和TimePicker中，
             * 显示删除按钮；
             * 如果不存在Item，则在day_selection中不填入任何信息，在TimePicker中填入“00：00”，隐藏删除按钮
             * 
             */
            index = DateListBox.SelectedIndex;
            if (index != -1 &&  _dates != null)
            {
                if (index < _dates.Count)
                {
                    defaultindex = Convert.ToInt32(DATES.ElementAt(index).get_week()) - 1;
                    defaulttime = DATES.ElementAt(index).get_start();

                    CourseDate date = DATES.ElementAt(index);
                    day_selection.SelectedIndex = Convert.ToInt32(date.get_week()) - 1;
                    TimeSpan time;
                    if (TimeSpan.TryParse(date.get_start(), out time))
                    {
                        TimePicker.SelectedTime = time;
                    }
                    btn_delete.Visibility = Visibility.Visible;
                }
                else
                {
                    defaultindex = -1;
                    defaulttime = "00:00";
                    TimeSpan time;
                    TimeSpan.TryParse("00:00", out time);
                    day_selection.SelectedIndex = -1;
                    TimePicker.SelectedTime = time;
                    btn_delete.Visibility = Visibility.Hidden;
                
                }
            }
            
        }

        private void day_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            if (day_selection.SelectedIndex == -1|| DateListBox.SelectedIndex==-1)
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


        private void TimePicker_SelectionChanged(object sender, 
            MahApps.Metro.Controls.TimePickerBaseSelectionChangedEventArgs<TimeSpan?> e)
        {
            if (day_selection.SelectedIndex == -1 || DateListBox.SelectedIndex == -1)
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

        private void btn_check_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan time = TimePicker.SelectedTime.Value;
            string t = time.ToString(@"hh\:mm");
            int w = day_selection.SelectedIndex + 1;

            //增加新建item
            if (index < _dates.Count)
            {
                _dates.ElementAt(index).start = t;
                _dates.ElementAt(index).week = w.ToString();
                DateListBox.Items.Add(new ListBoxItem { Content = _dates.ElementAt(index).ToString() });
            }
            else
            {
                CourseDate date = new CourseDate(t, w.ToString());
                _dates.AddLast(date);
                (DateListBox.SelectedItem as ListBoxItem).Content = date.ToString();
                DateListBox.Items.Add(new ListBoxItem { Content = "新建" });
            }

            btn_check.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Visible;

            //保存至xml文件
            MainwindowData.data.CurrentCourse.SaveTimeToXML = true;
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            int temp = index;
            var rmitem = _dates.ElementAt(index);
            _dates.Remove(rmitem);
            DateListBox.Items.RemoveAt(index);
            DateListBox.SelectedIndex = temp;


            //保存至xml文件
            MainwindowData.data.CurrentCourse.SaveTimeToXML = true;
        }
    }
}
