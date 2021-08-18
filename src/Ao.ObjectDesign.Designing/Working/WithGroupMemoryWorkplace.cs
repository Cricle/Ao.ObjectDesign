using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Working
{
    public class WithGroupMemoryWorkplace<TKey, TResource> : MemoryWorkplace<TKey, TResource>, IWithGroupWorkplace<TKey, TResource>
    {
        public WithGroupMemoryWorkplace(IDictionary<TKey, TResource> origin,
            TKey groupKey,
            IWorkplaceGroup<TKey,TResource> group)
            : base(origin)
        {
            GroupKey = groupKey;
            Group = group;
        }

        public TKey GroupKey { get; }

        public IWorkplaceGroup<TKey,TResource> Group { get; }
    }
}
