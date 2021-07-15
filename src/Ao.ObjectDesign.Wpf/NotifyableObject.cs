using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    public delegate void PropertyChangeToEventHandler(object sender, PropertyChangeToEventArgs e);
    [Serializable]
    public class NotifyableObject : INotifyPropertyChanged, INotifyPropertyChanging, INotifyPropertyChangeTo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangeToEventHandler PropertyChangeTo;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChangeTo(object from, object to, [CallerMemberName] string name = null)
        {
            if (PropertyChangeTo != null)
            {
                PropertyChangeTo.Invoke(this, new PropertyChangeToEventArgs(name, from, to));
            }
        }
        protected void Set<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                var origin = prop;
                RaisePropertyChanging(name);
                prop = value;
                RaisePropertyChanged(name);
                RaisePropertyChangeTo(origin, value, name);
            }
        }
    }
}
