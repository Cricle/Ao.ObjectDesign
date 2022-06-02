using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Ao.ObjectDesign
{
    public class MapObjectAccesstor : ObjectAccesstor
    {
        public MapObjectAccesstor(IDictionary<string,object> instance) : base(instance)
        {
            Map = instance;
            Names = instance.Keys;
        }

        public IDictionary<string, object> Map { get; }

        public override IEnumerable<string> Names { get; }

        public override bool HasName(string name)
        {
            return Map.ContainsKey(name);
        }

        public override object GetValue(string name)
        {
            return Map[name];
        }

        public override void SetValue(string name, object value)
        {
            Map[name] = value;
        }
    }
}
