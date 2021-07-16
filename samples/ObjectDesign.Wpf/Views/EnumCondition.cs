using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using System.Windows;

namespace ObjectDesign.Wpf.Views
{
    public class EnumCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public int Order { get; set; }

        public bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type.IsEnum;
        }

        public DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            return (DataTemplate)Application.Current.FindResource("ObjectDesign.Enum");
        }
    }
}
