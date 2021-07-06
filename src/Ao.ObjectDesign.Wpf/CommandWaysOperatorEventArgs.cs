using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public class CommandWaysOperatorEventArgs<T> : EventArgs
    {
        public CommandWaysOperatorEventArgs(IEnumerable<T> items, CommandWaysOperatorTypes type)
        {
            Items = items;
            Type = type;
        }

        public IEnumerable<T> Items { get; }

        public CommandWaysOperatorTypes Type { get; }
    }
}
