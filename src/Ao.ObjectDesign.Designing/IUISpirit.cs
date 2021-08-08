using System.Windows;

namespace Ao.ObjectDesign.Designing
{
    public interface IUISpirit<out TView, out TContext>
    {
        TContext Context { get; }
        TView View { get; }
    }
}