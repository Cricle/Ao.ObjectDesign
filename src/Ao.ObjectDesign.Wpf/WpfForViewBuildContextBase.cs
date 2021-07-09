using Ao.ObjectDesign.ForView;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public abstract class WpfForViewBuildContextBase : IForViewBuildContext
    {
        public bool UseNotifyVisitor { get; set; } = true;

        public IObjectDesigner Designer { get; set; }

        public IPropertyProxy PropertyProxy { get; set; }

        public BindingMode BindingMode { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

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
            return new CompiledPropertyVisitor(PropertyProxy.DeclaringInstance, PropertyProxy.PropertyInfo);
        }
    }
}
