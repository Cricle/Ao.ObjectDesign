namespace Ao.ObjectDesign.Designing.Level
{
    public interface IPositionBounded
    {
        IVector GetPosition();

        IRect GetBounds();
    }
}
