namespace Ao.ObjectDesign.Sources
{
    public interface IProviderFactoryCondition<TContext>
    {
        bool Condition(TContext context);
    }
}
