using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public interface IMiddlewareDesigner<TValue>
    {
        void Apply(TValue value);
        void SetDefault();
        void WriteTo(TValue value);
    }
}