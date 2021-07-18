using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindingDrawing
    {
        IBindForGetter BindForGetter { get; }
        Type ClrType { get; }
        Type DependencyObjectType { get; }

        IEnumerable<IBindingDrawingItem> Analysis();
    }
}