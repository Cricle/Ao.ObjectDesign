﻿using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class BindingCreatorStateDecoraterCollection<TSetting> : List<IBindingCreatorStateDecorater<TSetting>>, IBindingCreatorStateDecorater<TSetting>
    {
        public BindingCreatorStateDecoraterCollection()
        {
        }

        public BindingCreatorStateDecoraterCollection(int capacity) : base(capacity)
        {
        }

        public BindingCreatorStateDecoraterCollection(IEnumerable<IBindingCreatorStateDecorater<TSetting>> collection) : base(collection)
        {
        }

        public void Decorate(IBindingCreatorState state, IDesignPair<UIElement, TSetting> unit)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].Decorate(state, unit);
            }
        }
    }
}