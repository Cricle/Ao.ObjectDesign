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
            IActionSequencer<IModifyDetail> sequencer,
            DesignSuface designSuface)
            : base(provider)
        {
            DesignSuface = designSuface;
            DesignMetedatas = target.Select(x => CreateDesignMetedata(x)).ToList();
            Sequencer = sequencer;
        }
        public DesignContext(IServiceProvider provider,
            IDesignMetedata[] metedatas,
            IActionSequencer<IModifyDetail> sequencer,
            DesignSuface designSuface)
            : base(provider)
        {
            DesignMetedatas = metedatas;
            Sequencer = sequencer;
            DesignSuface = designSuface;
        }

        public virtual DesignSuface DesignSuface { get; }

        public virtual IReadOnlyList<IDesignMetedata> DesignMetedatas { get; }

        public virtual IActionSequencer<IModifyDetail> Sequencer { get; }

        protected virtual IDesignMetedata CreateDesignMetedata(UIElement element)
        {
            return new DesignMetedata(this, element);
        }

        public static readonly IReadOnlyList<Type> containerSet = new Type[]
        {
            typeof(Panel),typeof(Decorator)
        };
        public static FrameworkElement GetParent(FrameworkElement begin, Type type, bool equals)
        {
            FrameworkElement d = begin?.Parent as FrameworkElement;
            while (d != null)
            {
                if (equals)
                {
                    if (type == d.GetType())
                    {
                        return d;
                    }
                }
                else
                {
                    if (type.IsInstanceOfType(d))
                    {
                        return d;
                    }
                }
                d = d.Parent as FrameworkElement;
            }
            return null;
        }
        public static FrameworkElement GetContainer(FrameworkElement begin)
        {
            FrameworkElement d = begin?.Parent as FrameworkElement;
            while (d != null)
            {
                foreach (var item in containerSet)
                {
                    if (item.IsInstanceOfType(d))
                    {
                        return d;
                    }
                }
                d = d.Parent as FrameworkElement;
            }
            return null;
        }

    }
}
