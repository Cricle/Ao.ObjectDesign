namespace Ao.ObjectDesign
{
    public interface IPropertyVisitor
    {
        bool CanGet { get; }

        bool CanSet { get; }

        object Value { get; set; }
    }
}
