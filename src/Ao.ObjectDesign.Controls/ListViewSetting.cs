using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ListView))]
    public class ListViewSetting : ListBoxSetting, IMiddlewareDesigner<ListView>
    {
        public void Apply(ListView value)
        {
            Apply((ListBox)value);
        }

        public void WriteTo(ListView value)
        {
            WriteTo((ListBox)value);
        }
    }
}
