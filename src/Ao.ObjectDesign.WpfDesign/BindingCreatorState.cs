using System;
using System.Collections;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public class BindingCreatorState : DependencyObject,IBindingCreatorState
    {
        public IServiceProvider Provider { get; }

        public IDictionary Features { get; }

        public DesignRuntimeTypes RuntimeType { get; }

        public BindingCreatorState(IServiceProvider serviceProvider,
            IDictionary features,
            DesignRuntimeTypes runtimeType)
        {
            Provider = serviceProvider;
            Features = features;
            RuntimeType = runtimeType;
        }

        public object GetService(Type serviceType)
        {
            return Provider?.GetService(serviceType);
        }
    }
}
