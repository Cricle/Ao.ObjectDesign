using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IDesignUnit<TDesignObject>
    {
        UIElement UI { get; }

        TDesignObject DesigningObject { get; }

        IReadOnlyList<IWithSourceBindingScope> BindingScopes { get; }

        void Build();

        IReadOnlyList<BindingExpressionBase> Bind();
    }
}
