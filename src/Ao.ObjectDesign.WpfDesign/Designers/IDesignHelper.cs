using System.Windows;

namespace Ao.ObjectDesign.WpfDesign.Designers
{
    public interface IDesignHelper
    {
        void Attack(DesignSuface panel);

        void Dettck();

        void UpdateDesign(DesignContext context);

        void AttackObject(UIElement[] old, UIElement[] @new);
    }
}
