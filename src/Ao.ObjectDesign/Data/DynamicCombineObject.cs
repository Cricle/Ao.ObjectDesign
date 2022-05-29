using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Ao.ObjectDesign.Data
{
    public class DynamicCombineObject : DynamicObject
    {
        public DynamicCombineObject()
            :this(new ObjectCombiner())
        {
        }

        public DynamicCombineObject(ObjectCombiner combiner)
        {
            Combiner = combiner ?? throw new ArgumentNullException(nameof(combiner));
        }

        public ObjectCombiner Combiner { get; } 

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Combiner.PropertyProxyMap.Keys;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Combiner.TryGetProxy(binder.Name, out var val))
            {
                val.SetValue(value);
                return true;
            }
            return base.TrySetMember(binder, value);
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (Combiner.TryGet(binder.Name, out var val))
            {
                result = val;
                return true;
            }
            return base.TryGetMember(binder, out result);
        }
    }

}
