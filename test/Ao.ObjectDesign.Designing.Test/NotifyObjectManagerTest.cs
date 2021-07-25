using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class NotifyObjectManagerTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrownException()
        {
            NotifyObjectManager mgr = new NotifyObjectManager();

            Assert.ThrowsException<ArgumentNullException>(() => mgr.Attack(null));
            Assert.ThrowsException<ArgumentNullException>(() => mgr.Strip(null));
            Assert.ThrowsException<ArgumentNullException>(() => mgr.IsAttacked(null));
        }
        [TestMethod]
        public void Attack_ListenMustContainsThem()
        {
            NotifyObjectManager mgr = new NotifyObjectManager();

            NotifyableObject obj1 = new NotifyableObject();
            NotifyableObject obj2 = new NotifyableObject();
            NotifyableObject obj3 = new NotifyableObject();

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
            NotifyObjectManager mgr = new NotifyObjectManager();

            NotifyableObject obj1 = new NotifyableObject();
            Assert.IsTrue(mgr.Attack(obj1));
            Assert.IsFalse(mgr.Attack(obj1));

            Assert.AreEqual(1, mgr.ListeningCount);
        }
        [TestMethod]
        public void Attack_Strip_MustNotListned()
        {
            NotifyObjectManager mgr = new NotifyObjectManager();

            NotifyableObject obj1 = new NotifyableObject();
            mgr.Attack(obj1);

            mgr.Strip(obj1);

            Assert.AreEqual(0, mgr.ListeningCount);
        }
        [TestMethod]
        public void Attack_IsAttack_MustReturnObjectIsAttacked()
        {
            NotifyObjectManager mgr = new NotifyObjectManager();

            NotifyableObject obj1 = new NotifyableObject();
            mgr.Attack(obj1);
            Assert.IsTrue(mgr.IsAttacked(obj1));
            mgr.Strip(obj1);
            Assert.IsFalse(mgr.IsAttacked(obj1));
        }
        [TestMethod]
        public void Attack_Clear_AllMustStrip()
        {
            NotifyObjectManager mgr = new NotifyObjectManager();

            NotifyableObject obj1 = new NotifyableObject();
            mgr.Attack(obj1);

            mgr.ClearNotifyer();
            Assert.AreEqual(0, mgr.ListeningCount);
        }
    }
}
