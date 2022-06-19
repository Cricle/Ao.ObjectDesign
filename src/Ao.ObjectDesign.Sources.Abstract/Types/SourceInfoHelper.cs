using Ao.ObjectDesign.Sources.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ao.ObjectDesign.Sources.Types
{
    public class SourceInfoHelper
    {
        public static SourceInfo FromType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.GetTypeInfo().IsClass)
            {
                throw new ArgumentException($"{type} is not class");
            }

            var prs = new List<SourceColumn>();
            var columns = new List<SourceColumn>();

            foreach (var item in type.GetTypeInfo().DeclaredProperties)
            {
                if (item.GetCustomAttribute<SourceIgnoreAttribute>() == null)
                {
                    var nameAttr = item.GetCustomAttribute<SourceNameAttribute>();
                    var primaryAttr = item.GetCustomAttribute<SourcePrimaryAttribute>();
                    
                    var code = Type.GetTypeCode(item.PropertyType);

                    var column = new SourceColumn(nameAttr?.Name ?? item.Name, item.Name, code);
                    columns.Add(column);

                    if (primaryAttr != null)
                    {
                        prs.Add(column);
                    }
                }
            }
            return new SourceInfo(prs, columns);
        }
    }
}
