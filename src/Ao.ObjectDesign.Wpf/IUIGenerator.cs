using System.Collections.Generic;
using System;

namespace Ao.ObjectDesign.Wpf
{
    public interface IUIGenerator<TView, TContext>
    {
        IEnumerable<UISpirit<TView, TContext>> Generate(IObjectProxy objectProxy);
    }
}
