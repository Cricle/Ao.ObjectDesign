namespace Ao.ObjectDesign.Wpf
{
    public interface ISequencer<TFallback>
        where TFallback:IFallbackable
    {
        ICommandWays<TFallback> Undos { get; }

        ICommandWays<TFallback> Redos { get; }
    }
}
