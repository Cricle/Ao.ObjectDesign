using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

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
            IEnumerable<WpfForViewBuildContext> ctxs = propertyProxies.Select(x => new WpfForViewBuildContext
            {
                BindingMode = Mode,
                Designer = Designer,
                ForViewBuilder = Builder,
                UpdateSourceTrigger = UpdateSourceTrigger,
                PropertyProxy = x
            });
            foreach (WpfForViewBuildContext item in ctxs)
            {
                FrameworkElement ui = Builder.Build(item);
                yield return new UISpirit<FrameworkElement, WpfForViewBuildContext>(ui, item);
            }
        }
    }
}
