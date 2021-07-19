namespace Ao.ObjectDesign.Wpf.Designing
{
    public interface IMiddlewareDesigner<TValue> : IDefaulted
    {
        void Apply(TValue value);

        void WriteTo(TValue value);
    }
    public interface IMiddlewareDesigner : IDefaulted
    {
        void Apply(object value);

        void WriteTo(object value);
    }
}