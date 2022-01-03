using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Bindings.Designers;
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
    public class DesignSuface : Canvas, IWpfDesignSuface
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
                var len = oldEle.Length;
                for (int i = 0; i < len; i++)
                {
                    DesignerProperties.SetIsInDesignMode(oldEle[i], false);
                }
            }
            if (newEle != null)
            {
                var len = newEle.Length;
                for (int i = 0; i < len; i++)
                {
                    DesignerProperties.SetIsInDesignMode(newEle[i], true);
                }
            }
            if (@object is DesignSuface suface)
            {
                var children = suface.Children;
                var len = children.Count;
                for (int i = 0; i < len; i++)
                {
                    var child = children[i];
                    if (child is IDesignHelper<UIElement,IWpfDesignContext> helper)
                    {
                        helper.AttackObject(oldEle, newEle);
                    }
                }
            }

        }
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded is IDesignHelper<UIElement, IWpfDesignContext> addHelper)
            {
                addHelper.Attack(this);
            }
            if (visualRemoved is IDesignHelper<UIElement, IWpfDesignContext> removeHelper)
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
            var len = Children.Count;
            for (int i = 0; i < len; i++)
            {
                if (Children[i] is IDesignHelper<UIElement, IWpfDesignContext> helper)
                {
                    helper.UpdateDesign(ctx);
                }
            }
            EndUpdate(ctx);
        }

        protected virtual void BeginUpdate(IDesignContext<UIElement, IWpfDesignContext> context)
        {

        }
        protected virtual void EndUpdate(IDesignContext<UIElement, IWpfDesignContext> context)
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
        public virtual IWpfDesignContext GetContext()
        {
            if (DesigningObjects is null)
            {
                return null;
            }
            var ctx = new WpfDesignContext(ServiceProvider,
                DesigningObjects,
                Sequencer,
                this);
            return ctx;
        }
    }
}
