namespace Ao.ObjectDesign.Wpf
{
    public interface IModifyDetail: IFallbackable
    {
        object From { get; }
        object Instance { get; }
        string PropertyName { get; }
        object To { get; }

        new ModifyDetail Reverse();
    }
}