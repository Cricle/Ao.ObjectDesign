using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class BindVisitPathAttribute : Attribute
    {
        public BindVisitPathAttribute(string visiPath)
        {
            if (string.IsNullOrEmpty(visiPath))
            {
                throw new ArgumentException($"“{nameof(visiPath)}”不能为 null 或空。", nameof(visiPath));
            }

            VisiPath = visiPath;
        }

        public string VisiPath { get; }
    }
}
