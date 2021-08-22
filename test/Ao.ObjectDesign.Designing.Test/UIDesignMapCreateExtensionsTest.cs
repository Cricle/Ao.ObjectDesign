using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class UIDesignMapCreateExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var map = new UIDesignMap();
            var type = typeof(object);

            Assert.ThrowsException<ArgumentNullException>(() => UIDesignMapCreateExtensions.CreateByFactoryOrEmit(null, type));
            Assert.ThrowsException<ArgumentNullException>(() => UIDesignMapCreateExtensions.CreateByFactoryOrEmit(map, null));
            
            Assert.ThrowsException<ArgumentNullException>(() => UIDesignMapCreateExtensions.CreateByFactoryOrReflection(null, type));
            Assert.ThrowsException<ArgumentNullException>(() => UIDesignMapCreateExtensions.CreateByFactoryOrReflection(map, null));
        }

        [TestMethod]
        public void CreateWithReflection()
        {
            var map = new UIDesignMap();
            map.RegistDesignType(typeof(object), typeof(object));

            var obj = UIDesignMapCreateExtensions.CreateByFactoryOrReflection(map, typeof(object));
            Assert.IsNotNull(obj);

            map.RegistInstanceFactory(new RefelectionInstanceFactory(typeof(object)));

            obj = UIDesignMapCreateExtensions.CreateByFactoryOrReflection(map, typeof(object));
            Assert.IsNotNull(obj);
        }
        [TestMethod]
        public void CreateWithEmit()
        {
            var map = new UIDesignMap();
            map.RegistDesignType(typeof(object), typeof(object));

            map.RegistInstanceFactory(new EmitInstanceFactory(typeof(object)));

            var obj = UIDesignMapCreateExtensions.CreateByFactoryOrEmit(map, typeof(object));
            Assert.IsNotNull(obj);
        }
    }
}
