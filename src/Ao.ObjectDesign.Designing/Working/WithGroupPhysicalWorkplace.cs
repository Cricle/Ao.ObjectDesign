using System.IO;

namespace Ao.ObjectDesign.Designing.Working
{
    public abstract class WithGroupPhysicalWorkplace<TKey, TResource> : PhysicalWorkplace<TKey, TResource>, IWithGroupWorkplace<TKey, TResource>
    {
        protected WithGroupPhysicalWorkplace(TKey groupKey, 
            IWorkplaceGroup<TKey, TResource> group,
            DirectoryInfo folder)
            :base(folder)
        {
            GroupKey = groupKey;
            Group = group;
        }

        public TKey GroupKey { get; }

        public IWorkplaceGroup<TKey, TResource> Group { get; }

    }
}
