using Ao.ObjectDesign.ForView;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public abstract class WpfForViewBuildContextBase : IForViewBuildContext
    {
        public bool UseCompiledVisitor { get; set; }

        public IObjectDesigner Designer { get; set; }

        public IPropertyProxy PropertyProxy { get; set; }

        public BindingMode BindingMode { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public IPropertyVisitor GetPropertyVisitor()
        {
            if (PropertyProxy.DeclaringInstance is DependencyObject @do)
            {
                return new WpfPropertyVisitor(@do, PropertyProxy.PropertyInfo);
            }
            if (UseCompiledVisitor)
            {
                return new CompiledPropertyVisitor(PropertyProxy.DeclaringInstance, PropertyProxy.PropertyInfo);
            }
            return new PropertyVisitor(PropertyProxy.DeclaringInstance, PropertyProxy.PropertyInfo);
        }
    }
}
