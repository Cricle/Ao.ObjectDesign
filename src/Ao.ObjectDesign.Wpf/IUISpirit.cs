namespace Ao.ObjectDesign.Wpf
{
    public interface IUISpirit<out TView, TContext>
    {
        TContext Context { get; }
        TView View { get; }
    }
}