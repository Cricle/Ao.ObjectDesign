using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Test.Designing
{
    [TestClass]
    public class DefaultValue
    {
        [TestMethod]
        public void DefaultValues()
        {
            var types = typeof(DesigningHelpers).Assembly
                .GetExportedTypes()
                .Where(x => !x.IsAbstract && x.GetCustomAttribute<DesignForAttribute>() != null)
                .ToList();
            foreach (var item in types)
            {
                var designFor = item.GetCustomAttribute<DesignForAttribute>();
                Assert.IsNotNull(designFor,$"Type {item} is not tag DesignForAttribute");
                if (!designFor.Type.IsAbstract&&
                    !designFor.Type.IsGenericType&&
                    !item.IsAbstract&&
                    designFor.Type.GetConstructor(Type.EmptyTypes)!=null)
                {
                    var obj = Activator.CreateInstance(designFor.Type);
                    var objItem = Activator.CreateInstance(item);
                    var propMap = designFor.Type.GetProperties().ToDictionary(x => x.Name);
                    foreach (var prop in item.GetProperties())
                    {
                        var def = prop.GetCustomAttribute<DefaultValueAttribute>();
                        if (def!=null&& propMap.TryGetValue(prop.Name,out var d))
                        {
                            Assert.AreEqual(def.Value, d.GetValue(obj),
                                $"Type {item} map for {designFor.Type} property {prop.Name} default value is {def.Value}, actual is {d.GetValue(obj)}");
                        }
                    }
                }
            }
        }
    }
}
