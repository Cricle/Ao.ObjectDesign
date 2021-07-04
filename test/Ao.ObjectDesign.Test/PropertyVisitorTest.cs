using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyVisitorTest
    {
        class ClassAgeTypeConvert : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(Class);
            }
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(Class);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                int.TryParse(value?.ToString(), out var res);
                return res;
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                return value?.ToString();
            }
        }
        class Class
        {
            [TypeConverter(typeof(ClassAgeTypeConvert))]
            public int Age { get; set; }

            public string Name { get; set; }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var inst = new Class();
            var ageProp = typeof(Class).GetProperty(nameof(Class.Age));
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyVisitor(null, ageProp));
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyVisitor(inst, null));
        }
        [TestMethod]
        public void GivenNoAssignableFromType_MustThrowException()
        {
            var inst = 1;
            var ageProp = typeof(Class).GetProperty(nameof(Class.Age));
            Assert.ThrowsException<ArgumentException>(() => new PropertyVisitor(inst, ageProp));
        }
        [TestMethod]
        public void GivenHasTypeConvertProperty_MustCreateConverter()
        {
            var inst = new Class { Age=11};
            var ageProp = typeof(Class).GetProperty(nameof(Class.Age));

            var visitor = new PropertyVisitor(inst, ageProp);

            Assert.AreEqual(ageProp.CanRead, visitor.CanGet);
            Assert.AreEqual(ageProp.CanWrite, visitor.CanSet);
            Assert.AreEqual(ageProp.PropertyType, visitor.Type);
            Assert.AreEqual(ageProp, visitor.PropertyInfo);
            Assert.AreEqual(inst, visitor.DeclaringInstance);
            Assert.AreEqual(inst.Age, visitor.Instance);
            Assert.AreEqual(inst.Age, visitor.Value);
            inst.Age = 22;
            Assert.AreEqual(22, visitor.Value);
            visitor.SetValue(33);
            Assert.AreEqual(33, inst.Age);

            var convert1 = visitor.TypeConverter;

            Assert.IsNotNull(convert1);
            Assert.IsTrue(convert1 is ClassAgeTypeConvert);

            visitor = new PropertyVisitor(inst, ageProp);

            var convert2 = visitor.TypeConverter;

            Assert.IsNotNull(convert2);
            Assert.IsTrue(convert2 is ClassAgeTypeConvert);
            Assert.AreEqual(convert1, convert2);

            var typeConvert= new TypeConverter();
            visitor.TypeConverter = typeConvert;
            Assert.AreEqual(typeConvert, visitor.TypeConverter);
        }

    }
}
