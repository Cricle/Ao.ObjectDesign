namespace Ao.ObjectDesign.Sources
{
    public interface IProviderFactoryCondition
    {
        bool Condition(ProviderFactorySelectContext context);
    }
}
