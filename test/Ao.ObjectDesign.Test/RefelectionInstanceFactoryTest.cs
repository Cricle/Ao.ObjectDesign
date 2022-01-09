using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class RefelectionInstanceFactoryTest
    {
        class Student { }

        [TestMethod]
        public void Create()
        {
            var type = typeof(Student);
            var factory = new RefelectionInstanceFactory(type);
            var instance = factory.Create();
            Assert.IsInstanceOfType(instance, type);
        }
    }
}
