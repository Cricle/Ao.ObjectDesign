using Ao.ObjectDesign.ForView;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign
{
    public class ForViewDataTemplateSelector : DataTemplateSelector
    {
        public ForViewDataTemplateSelector(IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> forViewBuilder,
            IObjectDesigner objectDesigner)
        {
            ForViewBuilder = forViewBuilder ?? throw new ArgumentNullException(nameof(forViewBuilder));
            ObjectDesigner = objectDesigner ?? throw new ArgumentNullException(nameof(objectDesigner));

            BindingMode = BindingMode.TwoWay;
            UseNotifyVisitor = true;
        }

        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> ForViewBuilder { get; }

        public IObjectDesigner ObjectDesigner { get; }

        public BindingMode BindingMode { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public bool UseNotifyVisitor { get; set; }

        public bool ForceSelectBuild { get; set; }

        protected virtual bool PropertyNeedBuild(WpfTemplateForViewBuildContext context)
        {
            return true;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is WpfTemplateForViewBuildContext ctx &&
                PropertyNeedBuild(ctx))
            {
                DataTemplate v = ForViewBuilder.Build(ctx, ForceSelectBuild);
                if (v != null)
                {
                    return v;
                }
            }
            return null;
        }
    }
}
