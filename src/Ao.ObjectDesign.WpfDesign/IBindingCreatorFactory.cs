using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IBindingCreatorFactory<TDesignObject>
    {
        int Order { get; }

        bool IsAccept(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state);

        IEnumerable<IBindingCreator<TDesignObject>> Create(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state);
    }
}
