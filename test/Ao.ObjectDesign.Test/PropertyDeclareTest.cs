using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyDeclareTest
    {
        [TestMethod]
        public void GivenPropertyInfo_PropertyMustEqualInput()
        {
            var propinfo = typeof(Student).GetProperties()[0];
            var dec = new PropertyDeclare(propinfo);
            Assert.AreEqual(propinfo, dec.PropertyInfo);
        }
    }
}
