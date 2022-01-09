using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class EmitInstanceFactoryTest
    {

        class Student { }

        [TestMethod]
        public void Create()
        {
            var type = typeof(Student);
            var factory = new EmitInstanceFactory(type);
            var instance = factory.Create();
            Assert.IsInstanceOfType(instance, type);
        }
    }
}
