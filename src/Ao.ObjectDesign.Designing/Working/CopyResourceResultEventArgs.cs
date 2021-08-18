namespace Ao.ObjectDesign.Designing.Working
{
    public class CopyResourceResultEventArgs<TKey> : ActionResouceResultEventArgs<TKey>
    {
        public CopyResourceResultEventArgs(TKey name, TKey resultKey)
            : base(name, ResourceActions.Copied)
        {
            ResultKey = resultKey;
        }

        public TKey ResultKey { get; }
    }
}
