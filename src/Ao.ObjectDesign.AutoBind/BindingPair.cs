using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Data;
using System.Reflection;

namespace Ao.ObjectDesign.AutoBind
{
    public class BindingPair
    {
        internal BindingPair(IBindingMaker maker, IPropertyBox box)
        {
            Maker = maker;
            Box = box;
            if (box.Property.PropertyType.GetCustomAttribute<DesignForAttribute>() != null)
            {
                DesignForBox = DynamicTypePropertyHelper.FindFirstVirtualProperty(box.Property.PropertyType);
            }
        }

        public IBindingMaker Maker { get; }

        public IPropertyBox Box { get; }

        public IPropertyBox DesignForBox { get; }
    }
}
