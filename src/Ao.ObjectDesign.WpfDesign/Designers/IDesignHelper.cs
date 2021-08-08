namespace Ao.ObjectDesign.WpfDesign.Designers
{
    public interface IDesignHelper
    {
        void Attack(DesignSuface panel);

        void Dettck();

        void UpdateDesign(DesignContext context);

        void AttackObject(object old, object @new);
    }
}
