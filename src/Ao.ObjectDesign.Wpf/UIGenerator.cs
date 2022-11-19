using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign
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
            foreach (var item in propertyProxies)
            {
                var ctx = new WpfForViewBuildContext
                {
                    BindingMode = Mode,
                    Designer = Designer,
                    ForViewBuilder = Builder,
                    UpdateSourceTrigger = UpdateSourceTrigger,
                    PropertyProxy = item
                };
                yield return new WpfUISpirit(Builder.Build(ctx), ctx);
            }
        }
    }
}
