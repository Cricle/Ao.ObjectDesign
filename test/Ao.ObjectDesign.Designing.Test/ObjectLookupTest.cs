using Ao.ObjectDesign.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class ObjectLookupTest
    {
        class Class:NotifyableObject
        {
            public string Group { get; set; }


            public Student Student1 { get; set; }

            public Student Student2 { get; set; }
        }
        class Student : NotifyableObject
        {
            public string Name { get; set; }
        }
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var inst = new Class();
            var proxy = ObjectDesigner.CreateDefaultProxy(inst);
            Assert.ThrowsException<ArgumentNullException>(() => ObjectLookup.LookupNotifyableChangeTo(null, false).ToArray());
            Assert.ThrowsException<ArgumentNullException>(() => ObjectLookup.Lookup<object>(null, _ => true, (x, y) => y, false).ToArray());
            Assert.ThrowsException<ArgumentNullException>(() => ObjectLookup.Lookup<object>(proxy, null, (x, y) => y, false).ToArray());
            Assert.ThrowsException<ArgumentNullException>(() => ObjectLookup.Lookup<object>(proxy, _ => true, null, false).ToArray());
        }
        [TestMethod]
        public void Lookup()
        {
            var inst = new Class
            {
                Student1 = new Student(),
                Student2 = new Student()
            };
            var proxy = ObjectDesigner.CreateDefaultProxy(inst);
            var vals = ObjectLookup.LookupNotifyableChangeTo(proxy, false).ToArray();
            Assert.IsTrue(vals.Any(x => x == inst.Student1));
            Assert.IsTrue(vals.Any(x => x == inst.Student2));
        }
        [TestMethod]
        public void LookupWidthDeep_MustGotThem()
        {
            var c = new Class
            {
                Student1 = new Student(),
                Student2 = new Student()
            };
            var proxy = ObjectDesigner.CreateDefaultProxy(c);
            var ds = ObjectLookup.Lookup(proxy,
                x => true,
                (x, y) => y.Value as Student,
                false).ToArray();

            Assert.IsTrue(ds.Any(x => x == c.Student1));
            Assert.IsTrue(ds.Any(x => x == c.Student2));
        }
    }
}
