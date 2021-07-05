﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    public class NotifyableObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        protected void Set<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                prop = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
