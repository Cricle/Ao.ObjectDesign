using System;

namespace Ao.ObjectDesign.Designing
{
    public abstract class UIDesignMapActionEventArgs : EventArgs
    {
        protected UIDesignMapActionEventArgs(Type uIType, UIDesignMapActionTypes actionType)
        {
            UIType = uIType;
            ActionType = actionType;
        }

        public Type UIType { get; }

        public UIDesignMapActionTypes ActionType { get; }
    }
}
