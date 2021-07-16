using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class InnerObjectDesignExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Student inst = new Student();
            Type type = typeof(Student);
            ObjectDeclaring objDec = new ObjectDeclaring(type);
            PropertyDeclare propDec = new PropertyDeclare(type.GetProperties()[0]);
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy((IObjectDeclaring)null, inst));
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy(objDec, null));
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy((IPropertyDeclare)null, inst));
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy(propDec, null));
        }
        [TestMethod]
        public void ToProxy_MustGotProxy()
        {
            Student inst = new Student();
            Type type = typeof(Student);
            ObjectDeclaring objDec = new ObjectDeclaring(type);
            PropertyDeclare propDec = new PropertyDeclare(type.GetProperties()[0]);

            IObjectProxy objProxy = InnerObjectDesignExtensions.ToProxy(objDec, inst);
            Assert.AreEqual(inst, objProxy.Instance);
            Assert.AreEqual(type, objProxy.Type);
            IPropertyProxy propProxy = InnerObjectDesignExtensions.ToProxy(propDec, inst);
            Assert.AreEqual(inst, propProxy.DeclaringInstance);
            Assert.AreEqual(propDec.Type, propProxy.Type);

        }
    }
}
