using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using System.Windows;

namespace ObjectDesign.Wpf.Views
{
    public class PenBrushCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public int Order { get; set; }

        public bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(BrushDesigner);
        }

        public DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            return (DataTemplate)Application.Current.FindResource("ObjectDesign.Brush");
        }
    }
}
