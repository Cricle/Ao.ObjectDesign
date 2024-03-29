﻿using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Data
{
    public interface IBindingDrawing<out TDrawingItem>
    {
        IBindForGetter BindForGetter { get; }
        Type ClrType { get; }
        Type DependencyObjectType { get; }

        IEnumerable<TDrawingItem> Analysis();
    }
}