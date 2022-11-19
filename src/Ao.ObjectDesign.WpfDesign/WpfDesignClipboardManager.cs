using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfDesignClipboardManager<TDesignObject> : DesignClipboardManager<TDesignObject>
    {
        public override TDesignObject Clone(TDesignObject @object)
        {
            if (@object == null)
            {
                return default;
            }
            return (TDesignObject)ReflectionHelper.Clone(@object.GetType(), @object, DesigningHelpers.KnowDesigningTypes);
        }
    }
}
