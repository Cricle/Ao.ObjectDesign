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
            Visual target,
            IActionSequencer<IModifyDetail> sequencer)
            : base(provider)
        {
            Target = target;
            Sequencer = sequencer;
            Container = GetContainer(target);
            Parent = (Visual)VisualTreeHelper.GetParent(target);
        }
        public DesignContext(IServiceProvider provider,
            Visual target,
            Visual parent,
            Visual container,
            IActionSequencer<IModifyDetail> sequencer)
            : base(provider)
        {
            Target = target;
            Parent = parent;
            Container = container;
            Sequencer = sequencer;
        }

        public virtual DesignSuface DesignPanel { get; set; }

        public virtual Visual Target { get; }

        public virtual Visual Parent { get; }

        public virtual Visual Container { get; }

        public virtual IActionSequencer<IModifyDetail> Sequencer { get; }

        public virtual IEnumerable<Visual> GetAllTargets()
        {
            if (Target is Panel panel)
            {
                foreach (var item in panel.Children.OfType<Visual>())
                {
                    yield return item;
                }
            }
            else if (Target is Decorator dec)
            {
                yield return dec.Child;
            }
            else if (Target is ContentControl cc && cc is Visual ccdo)
            {
                yield return ccdo;
            }
            else
            {
                yield return Target;
            }
        }


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
                    if (type == d.GetType())
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
                    if (item.IsInstanceOfType(d)&&
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
