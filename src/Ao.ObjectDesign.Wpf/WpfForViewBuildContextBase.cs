using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.ForView;
using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public abstract class WpfForViewBuildContextBase : DependencyObject, IForViewBuildContext
    {


        public bool UseNotifyVisitor
        {
            get { return (bool)GetValue(UseNotifyVisitorProperty); }
            set { SetValue(UseNotifyVisitorProperty, value); }
        }


        public IObjectDesigner Designer
        {
            get { return (IObjectDesigner)GetValue(DesignerProperty); }
            set { SetValue(DesignerProperty, value); }
        }


        public IPropertyProxy PropertyProxy
        {
            get { return (IPropertyProxy)GetValue(PropertyProxyProperty); }
            set { SetValue(PropertyProxyProperty, value); }
        }


        public BindingMode BindingMode
        {
            get { return (BindingMode)GetValue(BindingModeProperty); }
            set { SetValue(BindingModeProperty, value); }
        }


        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get { return (UpdateSourceTrigger)GetValue(UpdateSourceTriggerProperty); }
            set { SetValue(UpdateSourceTriggerProperty, value); }
        }

        public static readonly DependencyProperty UpdateSourceTriggerProperty =
            DependencyProperty.Register("UpdateSourceTrigger", typeof(UpdateSourceTrigger), typeof(WpfForViewBuildContextBase), new PropertyMetadata(UpdateSourceTrigger.Default));


        // Using a DependencyProperty as the backing store for BindingMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingModeProperty =
            DependencyProperty.Register("BindingMode", typeof(BindingMode), typeof(WpfForViewBuildContextBase), new PropertyMetadata(BindingMode.Default));


        public static readonly DependencyProperty PropertyProxyProperty =
            DependencyProperty.Register("PropertyProxy", typeof(IPropertyProxy), typeof(WpfForViewBuildContextBase), new PropertyMetadata(null));


        public static readonly DependencyProperty DesignerProperty =
            DependencyProperty.Register("Designer", typeof(IObjectDesigner), typeof(WpfForViewBuildContextBase), new PropertyMetadata(null));


        public static readonly DependencyProperty UseNotifyVisitorProperty =
            DependencyProperty.Register("UseNotifyVisitor", typeof(bool), typeof(WpfForViewBuildContextBase), new PropertyMetadata(true));


        private IPropertyVisitor propertyVisitor;

        public IPropertyVisitor PropertyVisitor
        {
            get
            {
                if (propertyVisitor is null)
                {
                    propertyVisitor = GetPropertyVisitor();
                    PropertyVisitorCreated?.Invoke(this, EventArgs.Empty);
                }
                return propertyVisitor;
            }
        }

        public bool IsPropertyVisitorCreated => propertyVisitor != null;

        public event EventHandler PropertyVisitorCreated;

        protected virtual IPropertyVisitor GetPropertyVisitor()
        {
            if (PropertyProxy.DeclaringInstance is DependencyObject @do)
            {
                return new WpfPropertyVisitor(@do, PropertyProxy.PropertyInfo);
            }
            if (UseNotifyVisitor)
            {
                return new NotifyCompiledPropertyVisitor(PropertyProxy.DeclaringInstance, PropertyProxy.PropertyInfo);
            }
            return new ExpressionPropertyVisitor(PropertyProxy.DeclaringInstance, PropertyProxy.PropertyInfo);
        }
    }
}
