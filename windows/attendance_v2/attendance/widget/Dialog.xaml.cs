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
    public partial class Dialog : CustomDialog
    {
        public Dialog()
        {
            InitializeComponent();
           // Content = new CourseSettingView();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            var dia = window.Resources["dia"] as BaseMetroDialog;
            await DialogManager.HideMetroDialogAsync(window,this);
        }
    }
}
