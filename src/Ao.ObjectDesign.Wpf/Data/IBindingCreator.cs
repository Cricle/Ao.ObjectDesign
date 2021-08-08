using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindingCreator
    {
        Action<Binding> Actions { get; }
        DependencyProperty DependencyProperty { get; }

        IBindingCreator Add(Action<Binding> doAction);
        IBindingScope Build();
        IBindingCreator Clone();
    }
}