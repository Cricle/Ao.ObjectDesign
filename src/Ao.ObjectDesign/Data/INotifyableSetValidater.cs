namespace Ao.ObjectDesign.Data
{
    public interface INotifyableSetValidater<TKey, TValue>
    {
        bool Validate(DataChangedEventArgs<TKey, TValue> e, ref NotifyableSetValidaterContext context);
    }
}
