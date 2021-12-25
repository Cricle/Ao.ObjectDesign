using System;

namespace Ao.ObjectDesign.WpfDesign.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class BindingCreatorFactoryAttribute : Attribute
    {
        public BindingCreatorFactoryAttribute(Type creatorType)
        {
            CreatorType = creatorType ?? throw new ArgumentNullException(nameof(creatorType));
        }

        public Type CreatorType { get; }
    }
}
