using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public class UIDesignMapActionInstanceFactoryEventArgs : UIDesignMapActionEventArgs
    {
        public UIDesignMapActionInstanceFactoryEventArgs(Type type,IInstanceFactory oldFactory,IInstanceFactory newFactory, UIDesignMapActionTypes actionType) 
            : base(type, actionType)
        {
            OldInstanceFactory = oldFactory;
            NewInstanceFactory = newFactory;
        }

        public IInstanceFactory OldInstanceFactory { get; }
        public IInstanceFactory NewInstanceFactory { get; }
    }
}
