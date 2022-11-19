using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf.BindingCreators
{
    public abstract class WpfBindingCreator<TSetting> : BindingCreator<UIElement, TSetting, IWithSourceBindingScope>
    {
        public WpfBindingCreator(IDesignPair<UIElement, TSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }

        protected override IEnumerable<IWithSourceBindingScope> CreateBindingScopes()
        {
            if (CanSetToUI())
            {
                SetToUI();
            }
            if (CanGenerateBindings())
            {
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
