﻿using System;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace attendance
{
    /// <summary>
    /// MainWindow_v2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow_v2 : MetroWindow
    {
        public MainWindow_v2()
        {
            InitializeComponent();
            
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            var item = Flyouts.Items[0] as Flyout;
            item.IsOpen = true;
        }
    }
}
