using Ao.ObjectDesign.Designing.Level;
using System;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IDesignSessionInfo<TScene, TSetting> : IDesignSessionWorking, IDesignSessionDesignable<TScene, TSetting>, IDesignSessionFallbackable, IDisposable
           where TScene : IDesignScene<TSetting>
    {
        Guid Id { get; }

        bool IsDisposed { get; }
    }
}
