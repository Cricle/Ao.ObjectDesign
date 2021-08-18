namespace Ao.ObjectDesign.Designing.Working
{
    public static class WorkplaceActionExtensions
    {
        public static IWithGroupWorkplace<TKey,TResource> GetOrCreate<TKey,TResource>(this IWorkplaceGroup<TKey,TResource> workplace,
            TKey key)
        {
            if (workplace.Has(key))
            {
                return workplace.Get(key);
            }
            return workplace.Create(key);
        }
    }
}
