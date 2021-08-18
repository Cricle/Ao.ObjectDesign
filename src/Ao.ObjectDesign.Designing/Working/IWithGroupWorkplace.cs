using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Working
{
    public interface IWithGroupWorkplace<TKey,TResource> : IWorkplace<TKey,TResource>
    {
        TKey GroupKey { get; }

        IWorkplaceGroup<TKey,TResource> Group { get; }
    }
}
