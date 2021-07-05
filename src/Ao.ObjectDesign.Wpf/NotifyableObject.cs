using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    [Serializable]
    public class NotifyableObject : INotifyPropertyChanged,INotifyPropertyChanging
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        [field: NonSerialized]
        public event PropertyChangingEventHandler PropertyChanging;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanging([CallerMemberName] string name = null)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(name));
            }
        }
        protected void Set<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                RaisePropertyChanging(name);
                prop = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
