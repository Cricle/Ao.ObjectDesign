namespace Ao.ObjectDesign.Wpf
{
    public interface ISequencer
    {
        CommandWays<ModifyDetail> Undos { get; }

        CommandWays<ModifyDetail> Redos { get; }
    }
}
