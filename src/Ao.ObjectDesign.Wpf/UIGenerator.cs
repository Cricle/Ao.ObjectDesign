using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Ao.ObjectDesign.Designing;

namespace Ao.ObjectDesign.Wpf
{
    public class UIGenerator : IWpfUIGenerator, IUIGenerator<FrameworkElement, WpfForViewBuildContext>
    {
        public UIGenerator()
            : this(new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>())
        {

        }
        public UIGenerator(IForViewBuilder<FrameworkElement, WpfForViewBuildContext> builder)
            : this(ObjectDesigner.Instance, builder)
        {

        }

        public UIGenerator(IObjectDesigner designer,
            IForViewBuilder<FrameworkElement, WpfForViewBuildContext> builder)
        {
            Designer = designer ?? throw new System.ArgumentNullException(nameof(designer));
            Builder = builder ?? throw new System.ArgumentNullException(nameof(builder));

            Mode = BindingMode.TwoWay;
            UpdateSourceTrigger = UpdateSourceTrigger.Default;
        }

        public IObjectDesigner Designer { get; }

        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> Builder { get; }

        public BindingMode Mode { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        IEnumerable<IUISpirit<FrameworkElement, WpfForViewBuildContext>> IUIGenerator<FrameworkElement, WpfForViewBuildContext>.Generate(IEnumerable<IPropertyProxy> propertyProxies)
        {
            return Generate(propertyProxies);
        }

        public IEnumerable<IWpfUISpirit> Generate(IEnumerable<IPropertyProxy> propertyProxies)
        {
            return propertyProxies.Select(x => new WpfForViewBuildContext
            {
                BindingMode = Mode,
                Designer = Designer,
                ForViewBuilder = Builder,
                UpdateSourceTrigger = UpdateSourceTrigger,
                PropertyProxy = x
            }).Select(x => new WpfUISpirit(Builder.Build(x), x));
        }
    }
}
