using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TransferOriginAttribute : Attribute
    {
        public TransferOriginAttribute(Type origin)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        }

        public Type Origin { get; set; }
    }
}
