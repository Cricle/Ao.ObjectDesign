using System.Windows;

namespace Ao.ObjectDesign.Designing
{
    public interface IUISpirit<out TView, TContext>
    {
        TContext Context { get; }
        TView View { get; }
    }
}