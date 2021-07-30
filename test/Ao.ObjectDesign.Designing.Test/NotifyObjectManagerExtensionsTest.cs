using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class NotifyObjectManagerExtensionsTest
    {
        class Student : NotifyableObject
        {
            public Student Student1 { get; set; }
            public Student Student2 { get; set; }

            public string Hello { get; set; }
        }
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var mgr = new NotifyObjectManager();
            var s = new Student();
            Assert.ThrowsException<ArgumentNullException>(() => NotifyObjectManagerExtensions.StripAll(null));
            Assert.ThrowsException<ArgumentNullException>(() => NotifyObjectManagerExtensions.AttackDeep(null, s));
            Assert.ThrowsException<ArgumentNullException>(() => NotifyObjectManagerExtensions.AttackDeep(mgr, null));
        }
        [TestMethod]
        public void StripAll_AllMustBeStriped()
        {
            var mgr = new NotifyObjectManager();
            for (int i = 0; i < 10; i++)
            {
                mgr.Attack(new Student());
            }
            NotifyObjectManagerExtensions.StripAll(mgr);
            Assert.AreEqual(0, mgr.ListeningCount);
            Assert.AreEqual(0, mgr.Listenings.Count);
        }
        [TestMethod]
        public void AttackDeep_PropertyMustBeAttacked()
        {
            var mgr = new NotifyObjectManager();
            var s = new Student
            {
                Student1 = new Student(),
                Student2 = new Student()
            };
            NotifyObjectManagerExtensions.AttackDeep(mgr, s);
            Assert.AreEqual(3, mgr.ListeningCount);
            Assert.IsTrue(mgr.Listenings.Any(x => x == s));
            Assert.IsTrue(mgr.Listenings.Any(x => x == s.Student1));
            Assert.IsTrue(mgr.Listenings.Any(x => x == s.Student2));
        }
    }
}
