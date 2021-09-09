namespace Ao.ObjectDesign.Designing.Level
{
    public interface IRect
    {
        double Left { get; }

        double Top { get; }

        double Height { get; }

        double Right { get; }

        double Bottom { get; }

        double Width { get; }

        bool Contains(double x, double y);

        bool Contains(IRect other);

        bool IntersectsWith(IRect other);

    }
}
