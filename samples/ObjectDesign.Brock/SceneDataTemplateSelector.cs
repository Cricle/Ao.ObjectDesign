using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock
{
    public class SceneDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ItemTemplate { get; set; }

        public DataTemplate SceneTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IDesignScene<UIElementSetting>)
            {
                return SceneTemplate;
            }
            return ItemTemplate;
        }
    }
}
