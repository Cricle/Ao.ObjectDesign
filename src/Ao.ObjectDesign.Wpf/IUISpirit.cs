namespace Ao.ObjectDesign.Wpf
{
    public interface IUISpirit<TView, TContext>
    {
        TContext Context { get; }
        TView View { get; }
    }
}