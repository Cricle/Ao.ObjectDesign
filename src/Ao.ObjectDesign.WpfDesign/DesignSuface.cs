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
        public static readonly UIElement[] EmptyElements = new UIElement[0];

        public UIElement[] DesigningObjects
        {
            get { return (UIElement[])GetValue(DesigningObjectProperty); }
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
            DependencyProperty.Register("DesigningObjects", typeof(UIElement[]), typeof(DesignSuface), new PropertyMetadata(null, OnDesigningObjectChanged));

        private static void OnDesigningObjectChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
        {
            var newEle = e.NewValue as UIElement[];
            var oldEle = e.OldValue as UIElement[];

            if (oldEle != null)
            {
                foreach (var item in oldEle)
                {
                    DesignerProperties.SetIsInDesignMode(item, false);
                }
            }
            if (newEle != null)
            {
                foreach (var item in newEle)
                {
                    DesignerProperties.SetIsInDesignMode(item, true);
                }
            }
            if (@object is DesignSuface suface)
            {
                foreach (var item in suface.Children.OfType<IDesignHelper>())
                {
                    item.AttackObject(newEle, newEle);
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
            BeginUpdate(ctx);
            foreach (var item in Children.OfType<IDesignHelper>())
            {
                item.UpdateDesign(ctx);
            }
            EndUpdate(ctx);
        }

        protected virtual void BeginUpdate(DesignContext context)
        {

        }
        protected virtual void EndUpdate(DesignContext context)
        {

        }

        public virtual void ClearDesignObjects()
        {
            DesigningObjects = EmptyElements;
        }
        public virtual void SetDesigningObjects(params UIElement[] elements)
        {
            DesigningObjects = elements;
        }
        public virtual DesignContext GetContext()
        {
            if (DesigningObjects is null)
            {
                return null;
            }
            var ctx = new DesignContext(ServiceProvider,
                DesigningObjects,
                Sequencer,
                this);
            return ctx;
        }
    }
}
