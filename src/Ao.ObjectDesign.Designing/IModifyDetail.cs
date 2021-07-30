namespace Ao.ObjectDesign.Designing
{
    public interface IModifyDetail: IFallbackable
    {
        object From { get; }
        object Instance { get; }
        string PropertyName { get; }
        object To { get; }

        new IModifyDetail Reverse();
        new IModifyDetail Copy(FallbackMode? mode);
    }
}