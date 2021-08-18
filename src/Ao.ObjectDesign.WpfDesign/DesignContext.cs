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
            DesignMetedatas = target.Select(x => new DesignMetedata(this, x)).ToList();
            Sequencer = sequencer;
        }
        public DesignContext(IServiceProvider provider,
            DesignMetedata[] metedatas,
            IActionSequencer<IModifyDetail> sequencer,
            DesignSuface designSuface)
            : base(provider)
        {
            DesignMetedatas = metedatas;
            Sequencer = sequencer;
            DesignSuface = designSuface;
        }

        public virtual DesignSuface DesignSuface { get; }

        public virtual IReadOnlyList<DesignMetedata> DesignMetedatas { get; }

        public virtual IActionSequencer<IModifyDetail> Sequencer { get; }

        public static readonly IReadOnlyHashSet<Type> containerSet = new ReadOnlyHashSet<Type>(new Type[]
        {
            typeof(Panel),typeof(Decorator)
        });
        public static FrameworkElement GetParent(FrameworkElement begin, Type type, bool equals)
        {
            FrameworkElement d = begin?.Parent as FrameworkElement;
            while (d != null)
            {
                if (equals)
                {
                    if (type==d.GetType())
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
            //新增直接取
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
