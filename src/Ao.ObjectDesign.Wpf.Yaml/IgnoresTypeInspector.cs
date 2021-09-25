using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace Ao.ObjectDesign.Wpf.Yaml
{
    public class IgnoresTypeInspector : TypeInspectorSkeleton
    {
        private readonly ITypeInspector _innerTypeDescriptor;
        private readonly IReadOnlyHashSet<Type> ignoreTypes;

        public IgnoresTypeInspector(ITypeInspector innerTypeDescriptor,
            IReadOnlyHashSet<Type> ignoreTypes)
        {
            this.ignoreTypes = ignoreTypes ?? throw new ArgumentNullException(nameof(ignoreTypes));
            _innerTypeDescriptor = innerTypeDescriptor ?? throw new ArgumentNullException(nameof(innerTypeDescriptor));
        }

        public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
        {
            return _innerTypeDescriptor.GetProperties(type, container).Where(x => !ignoreTypes.Contains(x.Type));
        }

    }
}
