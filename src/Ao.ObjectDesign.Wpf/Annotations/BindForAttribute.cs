using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BindForAttribute : Attribute
    {
        public BindForAttribute(string forName)
        {
            if (string.IsNullOrEmpty(forName))
            {
                throw new ArgumentException($"“{nameof(forName)}”不能为 null 或空。", nameof(forName));
            }

            ForName = forName;
        }

        public BindForAttribute(Type dependencyObjectType, string forName)
        {
            if (string.IsNullOrEmpty(forName))
            {
                throw new ArgumentException($"“{nameof(forName)}”不能为 null 或空。", nameof(forName));
            }

            DependencyObjectType = dependencyObjectType ?? throw new ArgumentNullException(nameof(dependencyObjectType));
            ForName = forName;
        }

        public Type DependencyObjectType { get; }

        public string ForName { get; }
    }
}
