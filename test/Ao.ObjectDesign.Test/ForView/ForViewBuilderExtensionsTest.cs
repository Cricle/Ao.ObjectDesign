using Ao.ObjectDesign.Abstract.Annotations;
using Ao.ObjectDesign.ForView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Test.ForView
{
    [TestClass]
    public class ForViewBuilderExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ForViewBuilderExtensions.Build<object, NullForViewBuildContext>(null, new NullForViewBuildContext()));
        }
        class NullForViewBuildContext : IForViewBuildContext
        {
            public IPropertyProxy PropertyProxy { get; set; }
        }
        class ValueCondition : IForViewCondition<int?, ForViewBuildContext>
        {
            public int Order { get; set; }

            public Func<ForViewBuildContext, bool> CanBuildFun { get; set; }
            public Func<ForViewBuildContext, int?> CreateFunc { get; set; }

            public virtual bool CanBuild(ForViewBuildContext context)
            {
                return CanBuildFun?.Invoke(context) ?? false;
            }

            public virtual int? Create(ForViewBuildContext context)
            {
                return CreateFunc?.Invoke(context);
            }
        }
        class TrueCondition : ValueCondition
        {
            public override bool CanBuild(ForViewBuildContext context)
            {
                return false;
            }
            public override int? Create(ForViewBuildContext context)
            {
                return 123;
            }
        }
        class SelectFromAttrObj
        {
            [ForViewCondition(typeof(TrueCondition))]
            public int Name { get; set; }
        }
        [TestMethod]
        public void SelectFromAttribute()
        {
            var builder = new ForViewBuilder<int?, ForViewBuildContext>();
            var inst = new SelectFromAttrObj();
            var prop = inst.GetType().GetProperty(nameof(SelectFromAttrObj.Name));
            var propProxy = new PropertyProxy(inst, prop);
            var i = builder.Build(new ForViewBuildContext(propProxy),false);
            Assert.IsNull(i);
            i = builder.Build(new ForViewBuildContext(propProxy), true);
            Assert.AreEqual(123, i.Value);
            i = builder.Build(new ForViewBuildContext(propProxy));
            Assert.AreEqual(123, i.Value);
        }
        [TestMethod]
        public void Build()
        {
            var builder = new ForViewBuilder<int?, ForViewBuildContext>();
            builder.Add(new ValueCondition
            {
                CanBuildFun = a => a.PropertyProxy.PropertyInfo.Name == nameof(Student.Name),
                CreateFunc = a => 1
            });
            builder.Add(new ValueCondition
            {
                CanBuildFun = a => a.PropertyProxy.PropertyInfo.Name == nameof(Student.Age),
                CreateFunc = a => 2
            });
            var inst = new Student();

            var prop = inst.GetType().GetProperty(nameof(Student.Name));
            var propProxy = new PropertyProxy(inst, prop);
            var ctx = new ForViewBuildContext(propProxy);
            var val = ForViewBuilderExtensions.Build(builder, ctx);
            Assert.AreEqual(1, val.Value);

            prop = inst.GetType().GetProperty(nameof(Student.Age));
            propProxy = new PropertyProxy(inst, prop);
            ctx = new ForViewBuildContext(propProxy);
            val = ForViewBuilderExtensions.Build(builder, ctx);
            Assert.AreEqual(2, val.Value);

            prop = inst.GetType().GetProperty(nameof(Student.Address));
            propProxy = new PropertyProxy(inst, prop);
            ctx = new ForViewBuildContext(propProxy);
            val = ForViewBuilderExtensions.Build(builder, ctx);
            Assert.IsNull(val);
        }
    }
}
