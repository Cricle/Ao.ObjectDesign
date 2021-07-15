using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Data;
using Ao.ObjectDesign.ForView;

namespace Ao.ObjectDesign.Wpf
{
    public class UIGenerator : IUIGenerator<FrameworkElement, WpfForViewBuildContext>
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
            Designer = designer;
            Builder = builder;

            Mode = BindingMode.TwoWay;
            UpdateSourceTrigger = UpdateSourceTrigger.Default;
        }

        public IObjectDesigner Designer { get; }

        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> Builder { get; }

        public BindingMode Mode { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public IEnumerable<IUISpirit<FrameworkElement, WpfForViewBuildContext>> Generate(IEnumerable<IPropertyProxy> propertyProxies)
        {
            var ctxs = propertyProxies.Select(x => new WpfForViewBuildContext
            {
                BindingMode = Mode,
                Designer = Designer,
                ForViewBuilder = Builder,
                UpdateSourceTrigger = UpdateSourceTrigger,
                PropertyProxy = x
            });
            foreach (var item in ctxs)
            {
                var ui = Builder.Build(item);
                yield return new UISpirit<FrameworkElement, WpfForViewBuildContext>(ui, item);
            }
        }
    }
}
