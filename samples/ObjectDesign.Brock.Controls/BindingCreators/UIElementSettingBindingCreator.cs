using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Annotations;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public partial class UIElementSettingBindingCreator : WpfBindingCreator<UIElementSetting>
    {
        public UIElementSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }

        protected override IEnumerable<IWithSourceBindingScope> CreateBindingScopes()
        {
            var setting = DesignUnit.DesigningObject;
            if (CanSetToUI())
            {
                SetUIElementSettingToUI(setting, DesignUnit.UI);
                SetToUI();
            }
            if (CanGenerateBindings())
            {
                foreach (var item in UIElementSettingTwoWayScopes.Where(BindingCondition))
                {
                    yield return item.ToWithSource(DesignUnit.DesigningObject);
                }
                foreach (var item in GenerateBindings())
                {
                    yield return item;
                }
            }
        }

        protected virtual void SetToUI()
        {

        }
        protected virtual IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            yield break;
        }

        protected virtual bool CanSetToUI()
        {
            return true;
        }
        protected virtual bool CanGenerateBindings()
        {
            return State.RuntimeType != DesignRuntimeTypes.Running;
        }
        protected virtual bool BindingCondition(IBindingScope scope)
        {
            return true;
        }
    }
}
