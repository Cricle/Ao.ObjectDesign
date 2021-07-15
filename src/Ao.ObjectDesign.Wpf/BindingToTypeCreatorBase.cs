using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Wpf
{
    public abstract class BindingToTypeCreatorBase : IBindingToTypeCreator
    {
        protected BindingToTypeCreatorBase(IEnumerable<Type> supportCreateBindTypes)
            :this(new ReadOnlyHashSet<Type>(supportCreateBindTypes))
        {
        }

        protected BindingToTypeCreatorBase(IReadOnlyHashSet<Type> supportCreateBindTypes)
        {
            SupportCreateBindTypes = supportCreateBindTypes ?? throw new ArgumentNullException(nameof(supportCreateBindTypes));
        }

        public IReadOnlyHashSet<Type> SupportCreateBindTypes { get; }

        public abstract IEnumerable<BindingUnit> CreateBindings(Type type);

        public virtual bool IsSupportCreateBind(Type type)
        {
            Debug.Assert(SupportCreateBindTypes != null);

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return SupportCreateBindTypes.Contains(type);
        }
    }

}
