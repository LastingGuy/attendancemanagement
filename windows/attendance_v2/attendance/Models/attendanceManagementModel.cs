using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace attendanceManagement.Models
{
    class attendanceManagementModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void setProperty<T>(ref T item, T value, [CallerMemberName] string itemname = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(itemname));
            }

        }

    }
}
