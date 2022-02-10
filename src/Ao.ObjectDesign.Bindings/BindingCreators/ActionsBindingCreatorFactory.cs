using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings.BindingCreators
{
    public delegate IBindingCreator<TUI, TDesignObject, TBindingScope> CreateBindingCreator<TUI, TDesignObject, TBindingScope>(IDesignPair<TUI, TDesignObject> designUnit, IBindingCreatorState state);

    public class ActionsBindingCreatorFactory<TUI, TDesignObject, TBindingScope> : IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>
    {
        public ActionsBindingCreatorFactory(Type settingType, IEnumerable<CreateBindingCreator<TUI, TDesignObject, TBindingScope>> spiritTypes)
        {
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
            SpiritTypes = spiritTypes ?? throw new ArgumentNullException(nameof(spiritTypes));
        }

        public Type SettingType { get; }

        public IEnumerable<CreateBindingCreator<TUI, TDesignObject, TBindingScope>> SpiritTypes { get; }

        public int Order { get; set; }

        public IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> Create(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            foreach (var item in SpiritTypes)
            {
                yield return item(unit, state);
            }
        }

        public bool IsAccept(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == SettingType;
        }
    }
}
