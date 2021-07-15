namespace Ao.ObjectDesign.Wpf
{
    public interface ISequencer
    {
        ICommandWays<IModifyDetail> Undos { get; }

        ICommandWays<IModifyDetail> Redos { get; }
    }
}
