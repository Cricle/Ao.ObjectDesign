using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public class ForViewDataTemplateSelector : DataTemplateSelector
    {
        public ForViewDataTemplateSelector(IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> forViewBuilder,
            IObjectDesigner objectDesigner)
        {
            ForViewBuilder = forViewBuilder ?? throw new ArgumentNullException(nameof(forViewBuilder));
            ObjectDesigner = objectDesigner ?? throw new ArgumentNullException(nameof(objectDesigner));
        }

        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> ForViewBuilder { get; }

        public IObjectDesigner ObjectDesigner { get; }

        public BindingMode BindingMode { get; set; } = BindingMode.TwoWay;

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public bool UseCompiledVisitor { get; set; }

        public bool ForceSelectBuild { get; set; }

        protected virtual bool PropertyNeedBuild(IPropertyProxy proxy,DependencyObject dependency)
        {
            return true;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IPropertyProxy proxy && PropertyNeedBuild(proxy, container))
            {
                var ctx = new WpfTemplateForViewBuildContext
                {
                    Designer = ObjectDesigner,
                    ForViewBuilder = ForViewBuilder,
                    PropertyProxy = proxy,
                    BindingMode = BindingMode,
                    UpdateSourceTrigger = UpdateSourceTrigger,
                    UseCompiledVisitor = UseCompiledVisitor
                };

                var v = ForViewBuilder.Build(ctx, ForceSelectBuild);
                if (v != null)
                {
                    return v;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
