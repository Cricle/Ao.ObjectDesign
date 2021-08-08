using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.WpfDesign.Designers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DesignSuface : Canvas
    {
        public UIElement DesigningObject
        {
            get { return (UIElement)GetValue(DesigningObjectProperty); }
            set { SetValue(DesigningObjectProperty, value); }
        }

        public IServiceProvider ServiceProvider
        {
            get { return (IServiceProvider)GetValue(ServiceProviderProperty); }
            set { SetValue(ServiceProviderProperty, value); }
        }

        public IActionSequencer<IModifyDetail> Sequencer
        {
            get { return (IActionSequencer<IModifyDetail>)GetValue(SequencerProperty); }
            set { SetValue(SequencerProperty, value); }
        }

        public static readonly DependencyProperty SequencerProperty =
            DependencyProperty.Register("Sequencer", typeof(IActionSequencer<IModifyDetail>), typeof(DesignSuface), new PropertyMetadata(null));

        public static readonly DependencyProperty ServiceProviderProperty =
            DependencyProperty.Register("ServiceProvider", typeof(IServiceProvider), typeof(DesignSuface), new PropertyMetadata(null));

        public static readonly DependencyProperty DesigningObjectProperty =
            DependencyProperty.Register("DesigningObject", typeof(UIElement), typeof(DesignSuface), new PropertyMetadata(null, OnDesigningObjectChanged));

        private static void OnDesigningObjectChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is UIElement newEle)
            {
                DesignerProperties.SetIsInDesignMode(newEle, true);
            }
            if (e.OldValue is UIElement oldEle)
            {
                DesignerProperties.SetIsInDesignMode(oldEle, false);
            }
            if (@object is DesignSuface suface)
            {
                foreach (var item in suface.Children.OfType<IDesignHelper>())
                {
                    item.AttackObject(e.OldValue, e.NewValue);
                }
            }
        }
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded is IDesignHelper addHelper)
            {
                addHelper.Attack(this);
            }
            if (visualRemoved is IDesignHelper removeHelper)
            {
                removeHelper.Dettck();
            }
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        public void Update()
        {
            var ctx = GetContext();
            if (ctx is null)
            {
                return;
            }
            foreach (var item in Children.OfType<IDesignHelper>())
            {
                item.UpdateDesign(ctx);
            }
        }

        public DesignContext GetContext()
        {
            if (DesigningObject is null)
            {
                return null;
            }
            var ctx = new DesignContext(ServiceProvider,
                DesigningObject,
                Sequencer);
            ctx.DesignPanel = this;
            return ctx;
        }
    }
}
