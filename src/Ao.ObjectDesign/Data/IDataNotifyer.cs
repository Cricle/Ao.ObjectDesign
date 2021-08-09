namespace Ao.ObjectDesign.Data
{
    public interface IDataNotifyer<TKey>
    {
        void OnDataChanged(object sender,DataChangedEventArgs<TKey, VarValue> e);
    }
}
