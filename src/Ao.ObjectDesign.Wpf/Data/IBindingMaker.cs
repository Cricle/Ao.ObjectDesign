using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    public interface IBindingMaker<T>
    {
        Action<T> Actions { get; }
        DependencyProperty DependencyProperty { get; }

        IBindingMaker<T> Add(Action<T> doAction);
        IBindingScope Build();
        IBindingMaker<T> Clone();
    }
    public interface IBindingMaker : IBindingMaker<Binding>
    {

        new IBindingMaker Add(Action<Binding> doAction);
    }
}