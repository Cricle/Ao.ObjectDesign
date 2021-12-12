using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test
{
    public class MyStudent
    {
        public int Age { get; set; }

        private string name;
        [PlatformTargetGetMethod]
        public string GetName()
        {
            return name;
        }
        [PlatformTargetSetMethod]
        public void SetName(string name)
        {
            this.name = name;
        }
    }
    [TestClass]
    public class SerializerTest
    {

#if !NET452
        [TestMethod]
        public void TextJson()
        {
            var stu = new MyStudent { Age = 123 };

            var str = System.Text.Json.JsonSerializer.Serialize(stu);

            Assert.IsFalse(str.Contains("Name"));
        }
#endif
        [TestMethod]
        public void NewtonsoftJson()
        {
            var stu = new MyStudent { Age = 123 };

            var str = Newtonsoft.Json.JsonConvert.SerializeObject(stu);

            Assert.IsFalse(str.Contains("Name"));
        }
        [TestMethod]
        public void Xaml()
        {
            var stu = new MyStudent { Age = 123 };

            var str = Portable.Xaml.XamlServices.Save(stu);

            Assert.IsFalse(str.Contains("Name"));
        }
    }
}
