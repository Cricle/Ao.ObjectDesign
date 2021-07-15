using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class NotifyObjectManagerTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrownException()
        {
            var mgr = new NotifyObjectManager();

            Assert.ThrowsException<ArgumentNullException>(() => mgr.Attack(null));
            Assert.ThrowsException<ArgumentNullException>(() => mgr.Strip(null));
            Assert.ThrowsException<ArgumentNullException>(() => mgr.IsAttacked(null));
        }
        [TestMethod]
        public void Attack_ListenMustContainsThem()
        {
            var mgr = new NotifyObjectManager();

            var obj1 = new NotifyableObject();
            var obj2 = new NotifyableObject();
            var obj3 = new NotifyableObject();

            Assert.IsTrue(mgr.Attack(obj1));
            Assert.IsTrue(mgr.Attack(obj2));
            Assert.IsTrue(mgr.Attack(obj3));

            Assert.AreEqual(3, mgr.ListeningCount);
            Assert.AreEqual(3, mgr.Listenings.Count());
            Assert.IsTrue(mgr.Listenings.Any(x => x == obj1));
            Assert.IsTrue(mgr.Listenings.Any(x => x == obj2));
            Assert.IsTrue(mgr.Listenings.Any(x => x == obj3));
        }
        [TestMethod]
        public void GivenTwiceSameAttack_SecondMustFalse()
        {
            var mgr = new NotifyObjectManager();

            var obj1 = new NotifyableObject();
            Assert.IsTrue(mgr.Attack(obj1));
            Assert.IsFalse(mgr.Attack(obj1));

            Assert.AreEqual(1, mgr.ListeningCount);
        }
        [TestMethod]
        public void Attack_Strip_MustNotListned()
        {
            var mgr = new NotifyObjectManager();

            var obj1 = new NotifyableObject();
            mgr.Attack(obj1);

            mgr.Strip(obj1);

            Assert.AreEqual(0, mgr.ListeningCount);
        }
        [TestMethod]
        public void Attack_IsAttack_MustReturnObjectIsAttacked()
        {
            var mgr = new NotifyObjectManager();

            var obj1 = new NotifyableObject();
            mgr.Attack(obj1);
            Assert.IsTrue(mgr.IsAttacked(obj1));
            mgr.Strip(obj1);
            Assert.IsFalse(mgr.IsAttacked(obj1));
        }
        [TestMethod]
        public void Attack_Clear_AllMustStrip()
        {
            var mgr = new NotifyObjectManager();

            var obj1 = new NotifyableObject();
            mgr.Attack(obj1);

            mgr.ClearNotifyer();
            Assert.AreEqual(0, mgr.ListeningCount);
        }
    }
}
