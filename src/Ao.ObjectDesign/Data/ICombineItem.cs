namespace Ao.ObjectDesign.Data
{
    public interface ICombineItem
    {
        string Name { get; }

        object GetValue();

        void SetValue(object value);
    }

}
