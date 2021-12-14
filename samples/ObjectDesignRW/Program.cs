using Ao.ObjectDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Net.Http;
using System.Net;
using System.IO;
using Ao.ObjectDesign.Wpf.MessagePack;
using Ao.ObjectDesign.Wpf.Json;
using System.Diagnostics;
using System.Text;
using System.IO.Compression;

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
            Window win = new Window();
            IObjectProxy c = ObjectDesigner.Instance.CreateProxy(win, win.GetType());

            List<IPropertyProxy> props = new List<IPropertyProxy>();
            IObjectProxy[] objProxys = new[] { c };
            while (objProxys.Length != 0)
            {
                IEnumerable<IPropertyProxy> ps = objProxys.SelectMany(x => x.GetPropertyProxies());
                props.AddRange(ps);
                objProxys = ps.Where(x => !ignoreType.Contains(x.Type))
                    .Select(x => x.CreateVisitor())
                    .ToArray();
                return;
            }

        }
    }
}
