using Ao.ObjectDesign.AutoBind;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public class BrockAutoBindingCreatorFactory<TUI, TDesignObject> : AutoBindingCreatorFactory<TUI, UIElementSetting, TDesignObject>
        where TUI : DependencyObject
    {
        public BrockAutoBindingCreatorFactory()
        {
        }
        public BrockAutoBindingCreatorFactory(Func<IDesignPair<UIElement, UIElementSetting>, IBindingCreatorState, IBindingCreator<UIElement, UIElementSetting, IWithSourceBindingScope>> parentCreator)
        {
            ParentCreator = parentCreator;
        }
    }
}
