using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IBindingCreator<TDesignObject>
    {
        int Order { get; }

        bool IsAccept(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state);

        IEnumerable<IBindingScope> Create(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state);
    }
}
