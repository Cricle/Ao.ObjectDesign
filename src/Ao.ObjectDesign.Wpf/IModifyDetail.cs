namespace Ao.ObjectDesign.Wpf
{
    public interface IModifyDetail
    {
        object From { get; }
        object Instance { get; }
        string PropertyName { get; }
        object To { get; }

        ModifyDetail Reverse();
    }
}