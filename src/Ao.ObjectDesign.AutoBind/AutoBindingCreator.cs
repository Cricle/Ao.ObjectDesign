using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.AutoBind
{
    public partial class AutoBindingCreator<TUI,TSetting,TDesignObject> : WpfBindingCreator<TSetting>
        where TUI:DependencyObject
    {
        public AutoBindingCreator(IDesignPair<UIElement, TSetting> designUnit, IBindingCreatorState state) : base(designUnit, state)
        {
        }

        public IBindingCreator<UIElement, TSetting, IWithSourceBindingScope> Parent { get; set; }

        protected override IEnumerable<IWithSourceBindingScope> CreateBindingScopes()
        {
            if (CanSetToUI())
            {
                SetToUI();
            }
            if (Parent!=null)
            {
                foreach (var item in Parent.BindingScopes)
                {
                    yield return item.ToWithSource(DesignUnit.DesigningObject);
                }
            }
            if (CanGenerateBindings())
            {
                foreach (var item in GenerateBindings())
                {
                    yield return item.ToWithSource(DesignUnit.DesigningObject);
                }
            }
        }
        public override void Attack()
        {
            base.Attack();
            Parent?.Attack();
        }
        public override void Detack()
        {
            base.Detack();
            Parent?.Detack();
        }
        protected virtual void SetToUI()
        {
            SetSettingToUI(DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected virtual IEnumerable<IBindingScope> GenerateBindings()
        {
            return TwoWayScopes;
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
