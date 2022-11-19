using Ao.ObjectDesign.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ao.ObjectDesign.Test.Conditions
{
    [TestClass]
    public class BooleanForViewConditionTest
    {
        class ValuePropertyProxy : IPropertyProxy
        {
            public object DeclaringInstance { get; set; }

            public PropertyInfo PropertyInfo { get; set; }

            public Type Type { get; set; }

            public IEnumerable<IPropertyDeclare> GetPropertyDeclares()
            {
                yield break;
            }
        }
        [TestMethod]
        [DataRow(typeof(bool))]
        [DataRow(typeof(bool?))]
        public void GivenBoolOrNullableBool_MustCanBuild(Type type)
        {
            var proxy = new ValuePropertyProxy { Type = type };
            var condition = new BooleanForViewCondition();
            var res = condition.CanBuild(new WpfForViewBuildContext { PropertyProxy = proxy });
            Assert.IsTrue(res);
        }
    }
}
