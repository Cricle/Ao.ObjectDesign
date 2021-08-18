using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IDesignUnit<TDesignObject> : IDesignPair<UIElement, TDesignObject>
    {
        IReadOnlyList<IWithSourceBindingScope> BindingScopes { get; }

        void Build();

        IReadOnlyList<BindingExpressionBase> Bind();
    }
}
