using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
   public class InnerObjectDesignExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var inst = new Student();
            var type = typeof(Student);
            var objDec = new ObjectDeclaring(type);
            var propDec = new PropertyDeclare(type.GetProperties()[0]);
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy((IObjectDeclaring)null, inst));
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy(objDec, null));
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy((IPropertyDeclare)null, inst));
            Assert.ThrowsException<ArgumentNullException>(() => InnerObjectDesignExtensions.ToProxy(propDec, null));
        }
        [TestMethod]
        public void ToProxy_MustGotProxy()
        {
            var inst = new Student();
            var type = typeof(Student);
            var objDec = new ObjectDeclaring(type);
            var propDec = new PropertyDeclare(type.GetProperties()[0]);

            var objProxy = InnerObjectDesignExtensions.ToProxy(objDec, inst);
            Assert.AreEqual(inst, objProxy.Instance);
            Assert.AreEqual(type, objProxy.Type);
            var propProxy = InnerObjectDesignExtensions.ToProxy(propDec, inst);
            Assert.AreEqual(inst, propProxy.DeclaringInstance);
            Assert.AreEqual(propDec.Type, propProxy.Type);

        }
    }
}
