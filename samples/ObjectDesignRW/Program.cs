using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using Ao.ObjectDesign;

namespace ObjectDesignRW
{
    class Program
    {
        private static readonly HashSet<Type> ignoreType = new HashSet<Type>
        {
            typeof(string),
            typeof(Type),
            typeof(decimal),
            typeof(ICollection<>),
            typeof(MethodBase),

        };
        [STAThread]
        static void Main(string[] args)
        {
            var win = new Window();
            var c = ObjectDesigner.Instance.CreateProxy(win, win.GetType());

            var props = new List<IPropertyProxy>();
            var objProxys = new[] { c };
            while (objProxys.Length != 0)
            {
                var ps = objProxys.SelectMany(x => x.GetPropertyProxies());
                props.AddRange(ps);
                objProxys = ps.Where(x=> !ignoreType.Contains(x.Type))
                    .Select(x => x.CreateVisitor())
                    .ToArray();
                break;
            }
        }
    }
}
