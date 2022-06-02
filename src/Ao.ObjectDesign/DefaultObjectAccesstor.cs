using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign
{
    public class DefaultObjectAccesstor : ObjectAccesstor
    {
        class CacheEntity
        {
            public IReadOnlyDictionary<string, IPropertyDeclare> PropertyDeclareMap;
        }

        private static readonly Dictionary<Type, CacheEntity> CacheEntities = new Dictionary<Type, CacheEntity>();

        private static CacheEntity GetOrCreate(Type type)
        {
            if (!CacheEntities.TryGetValue(type,out var entity))
            {
                entity = new CacheEntity();
                entity.PropertyDeclareMap = new ObjectDeclaring(type)
                    .GetPropertyDeclares()
                    .ToDictionary(x => x.PropertyInfo.Name);
            }
                return entity;
        }

        public DefaultObjectAccesstor(object instance) : base(instance)
        {
            entity = GetOrCreate(instance.GetType());
        }
        
        private readonly CacheEntity entity;

        public override IEnumerable<string> Names => entity.PropertyDeclareMap.Keys;

        public override bool HasName(string name)
        {
            return entity.PropertyDeclareMap.ContainsKey(name);
        }

        public override object GetValue(string name)
        {
            var declare = entity.PropertyDeclareMap[name];
            return declare.GetValue(Instance);
        }

        public override void SetValue(string name, object value)
        {
            var declare = entity.PropertyDeclareMap[name];
            declare.SetValue(Instance,value);
        }
    }
}
