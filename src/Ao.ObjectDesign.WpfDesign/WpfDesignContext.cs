using Ao.ObjectDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public partial class WpfDesignContext : DesignContext<UIElement,IWpfDesignContext>, IWpfDesignContext
    {
        public WpfDesignContext(IServiceProvider provider, IEnumerable<UIElement> target, IActionSequencer<IModifyDetail> sequencer, IDesignSuface<UIElement, IWpfDesignContext> designSuface) : base(provider, target, sequencer, designSuface)
        {
        }

        public WpfDesignContext(IServiceProvider provider, IDesignMetedata<UIElement, IWpfDesignContext>[] metedatas, IActionSequencer<IModifyDetail> sequencer, IDesignSuface<UIElement, IWpfDesignContext> designSuface) : base(provider, metedatas, sequencer, designSuface)
        {
        }
        protected override IDesignMetedata<UIElement, IWpfDesignContext> CreateDesignMetedata(UIElement element)
        {
            return new WpfDesignMetedata(this, element);
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
