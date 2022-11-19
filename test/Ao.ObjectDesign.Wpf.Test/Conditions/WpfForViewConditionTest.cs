using Ao.ObjectDesign.Conditions;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Test.Conditions
{
    [TestClass]
    public class WpfForViewConditionTest
    {
        public class ValueWpfForViewCondition : WpfForViewCondition
        {
            public override bool CanBuild(WpfForViewBuildContext context)
            {
                return true;
            }

            protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
            {
                e.SetBinding(Button.ContentProperty, binding);
            }

            protected override FrameworkElement CreateView(WpfForViewBuildContext context)
            {
                return new Button();
            }


        }
        class Student
        {
            public string Name { get; set; }
        }
        [TestMethod]
        public void CreateUIWithBinding()
        {
            var th = new Thread(() =>
              {

                  var condition = new ValueWpfForViewCondition();
                  var proxy = ObjectDesigner.CreateDefaultProxy(new Student());
                  var builder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();
                  var ctx = new WpfForViewBuildContext
                  {
                      PropertyProxy = proxy.GetPropertyProxies().First(),
                      BindingMode = BindingMode.TwoWay,
                      Designer = ObjectDesigner.Instance,
                      UseNotifyVisitor = true,
                      UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                      ForViewBuilder = builder
                  };
                  var fe = condition.Create(ctx);
                  Assert.IsInstanceOfType(fe, typeof(Button));
                  var bd = BindingOperations.GetBinding(fe, Button.ContentProperty);
                  Assert.IsNotNull(bd);
                  Assert.AreEqual(ctx.BindingMode, bd.Mode);
                  Assert.AreEqual(ctx.UpdateSourceTrigger, bd.UpdateSourceTrigger);
                  Assert.AreEqual(ctx.PropertyProxy.PropertyInfo.Name, bd.Path.Path);
                  Assert.AreEqual(ctx.PropertyProxy.DeclaringInstance, bd.Source);
              });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();
        }
    }
}
