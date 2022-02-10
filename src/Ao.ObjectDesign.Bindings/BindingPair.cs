using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Reflection;

namespace Ao.ObjectDesign.Bindings
{
    public class BindingPair<TBindingMaker>
    {
        public BindingPair(TBindingMaker maker, IPropertyBox box)
        {
            Maker = maker;
            Box = box;
            if (box.Property.PropertyType.GetCustomAttribute<DesignForAttribute>() != null)
            {
                DesignForBox = DynamicTypePropertyHelper.FindFirstVirtualProperty(box.Property.PropertyType);
            }
        }

        public TBindingMaker Maker { get; }

        public IPropertyBox Box { get; }

        public IPropertyBox DesignForBox { get; }
    }
}
