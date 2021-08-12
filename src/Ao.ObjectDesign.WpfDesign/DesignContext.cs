using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public partial class DesignContext : DesignInitContext
    {
        public DesignContext(IServiceProvider provider,
            IEnumerable<UIElement> target,
            IActionSequencer<IModifyDetail> sequencer)
            : base(provider)
        {
            DesignMetedatas = target.Select(x => new DesignMetedata(this, x)).ToList();
            Sequencer = sequencer;
        }
        public DesignContext(IServiceProvider provider,
            DesignMetedata[] metedatas,
            IActionSequencer<IModifyDetail> sequencer)
            : base(provider)
        {
            DesignMetedatas = metedatas;
            Sequencer = sequencer;
        }

        public virtual DesignSuface DesignPanel { get; set; }

        public virtual IReadOnlyList<DesignMetedata> DesignMetedatas { get; }

        public virtual IActionSequencer<IModifyDetail> Sequencer { get; }

        public static readonly IReadOnlyHashSet<Type> containerSet = new ReadOnlyHashSet<Type>(new Type[]
        {
            typeof(Panel),typeof(Decorator)
        });
        public static Visual GetParent(DependencyObject begin, Type type, bool equals)
        {
            var d = VisualTreeHelper.GetParent(begin);
            while (d != null)
            {
                if (equals)
                {
                    if (type.IsEquivalentTo(d.GetType()))
                    {
                        return d as Visual;
                    }
                }
                else
                {
                    if (type.IsInstanceOfType(d))
                    {
                        return d as Visual;
                    }
                }
                d = VisualTreeHelper.GetParent(d);
            }
            return null;
        }
        public static Visual GetContainer(DependencyObject begin)
        {
            var d = VisualTreeHelper.GetParent(begin);
            while (d != null)
            {
                foreach (var item in containerSet)
                {
                    if (item.IsInstanceOfType(d) &&
                        d is Visual v)
                    {
                        return v;
                    }
                }
                d = VisualTreeHelper.GetParent(d);
            }
            return null;
        }

    }
}
