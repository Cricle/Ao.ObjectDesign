using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class SerializerTest
    {
        class Student
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
#if !NET452
        [TestMethod]
        public void TextJson()
        {
            var stu = new Student { Age = 123 };

            var str = System.Text.Json.JsonSerializer.Serialize(stu);

            Assert.IsFalse(str.Contains("Name"));
        }
#endif
        [TestMethod]
        public void NewtonsoftJson()
        {
            var stu = new Student { Age = 123 };

            var str = Newtonsoft.Json.JsonConvert.SerializeObject(stu);

            Assert.IsFalse(str.Contains("Name"));
        }
        [TestMethod]
        public void Xaml()
        {
            var stu = new Student { Age = 123 };

            var str = Portable.Xaml.XamlServices.Save(stu);

            Assert.IsFalse(str.Contains("Name"));
        }
    }
}
