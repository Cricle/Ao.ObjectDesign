﻿using System.ComponentModel;

namespace Ao.ObjectDesign.Designing
{
    public class PropertyChangeToEventArgs : PropertyChangedEventArgs
    {
        public PropertyChangeToEventArgs(string propertyName, object from, object to)
            : base(propertyName)
        {
            From = from;
            To = to;
        }

        public object From { get; }

        public object To { get; }
    }
}
