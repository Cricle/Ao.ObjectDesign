using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IBindingCreator<TDesignObject>
    {
        IDesignPair<UIElement, TDesignObject> DesignUnit { get; }

        IBindingCreatorState State { get; }

        IEnumerable<IWithSourceBindingScope> BindingScopes { get; }

        void Attack();

        void Detack();
    }
}
