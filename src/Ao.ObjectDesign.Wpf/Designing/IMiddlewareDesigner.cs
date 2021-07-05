using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public interface IMiddlewareDesigner<TValue>: IDefaulted
    {
        void Apply(TValue value);
        void WriteTo(TValue value);
    }
}