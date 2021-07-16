using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyDeclareTest
    {
        [TestMethod]
        public void GivenPropertyInfo_PropertyMustEqualInput()
        {
            System.Reflection.PropertyInfo propinfo = typeof(Student).GetProperties()[0];
            PropertyDeclare dec = new PropertyDeclare(propinfo);
            Assert.AreEqual(propinfo, dec.PropertyInfo);
        }
    }
}
