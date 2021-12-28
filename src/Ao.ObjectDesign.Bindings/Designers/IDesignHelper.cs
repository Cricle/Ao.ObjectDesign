using Ao.ObjectDesign.Designing;
using System.Windows;

namespace Ao.ObjectDesign.Bindings.Designers
{
    public interface IDesignHelper<TUI,TContext>
    {
        void Attack(IDesignSuface<TUI,TContext> panel);

        void Dettck();

        void UpdateDesign(TContext context);

        void AttackObject(TUI[] old, TUI[] @new);
    }
}
