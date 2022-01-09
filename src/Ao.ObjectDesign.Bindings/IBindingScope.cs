namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingScope<TBinding, TExpression, TObject>
    {
        TBinding CreateBinding(object source);

        TExpression Bind(TObject @object, object source);
    }
}
