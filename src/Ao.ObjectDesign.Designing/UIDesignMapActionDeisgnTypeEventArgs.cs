using System;

namespace Ao.ObjectDesign.Designing
{
    public class UIDesignMapActionDeisgnTypeEventArgs : UIDesignMapActionEventArgs
    {
        public UIDesignMapActionDeisgnTypeEventArgs(Type uIType,Type designType, UIDesignMapActionTypes actionType) 
            : base(uIType, actionType)
        {
            DesignType = designType;
        }

        public Type DesignType { get; }
    }
}
