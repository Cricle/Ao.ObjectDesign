using Ao.ObjectDesign.Designing.Level;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test.Level
{
    [TestClass]
    public class DesignSceneControllerTest
    {
        [TestMethod]
        public void Initialize_WasListenScene()
        {
            var controller = new ValueDesignSceneController();
            controller.Initialize();

            Assert.IsTrue(controller.IsInitialized);

            controller.Scene.DesigningObjects.Add(1);

            Assert.AreEqual(1, controller.DesignUnits.Count);
            Assert.AreEqual(1, controller.DesignObjectUnitMap.Count);
            Assert.AreEqual(1, controller.DesignUnitMap.Count);

            var a = controller.DesignObjectUnitMap[1];
            var b = controller.DesignUnitMap["1"];
            var c = controller.DesignUnits[0];

            Assert.AreEqual(a, b);
            Assert.AreEqual(a, c);

            controller.Dispose();

            controller.Scene.DesigningObjects.Add(2);

            Assert.AreEqual(1, controller.DesignUnits.Count);
            Assert.AreEqual(1, controller.DesignObjectUnitMap.Count);
            Assert.AreEqual(1, controller.DesignUnitMap.Count);
        }
        [TestMethod]
        public void Reset()
        {
            var controller = new ValueDesignSceneController();
            
            controller.Initialize();
            Assert.IsTrue(controller.IsInitialized);
            controller.Initialize();
            Assert.IsTrue(controller.IsInitialized);

            controller.Scene.DesigningObjects.Add(1);
            controller.Scene.DesigningObjects.Clear();

            Assert.AreEqual(0, controller.DesignUnits.Count);
            Assert.AreEqual(0, controller.DesignObjectUnitMap.Count);
            Assert.AreEqual(0, controller.DesignUnitMap.Count);
        }
        [TestMethod]
        public void Remove()
        {
            var controller = new ValueDesignSceneController();

            controller.Initialize();

            controller.Scene.DesigningObjects.Add(1);
            controller.Scene.DesigningObjects.Add(2);
            controller.Scene.DesigningObjects.Add(3);
            controller.Scene.DesigningObjects.Add(4);

            controller.Scene.DesigningObjects.RemoveAt(1);
            controller.Scene.DesigningObjects.RemoveAt(1);

            Assert.AreEqual(2, controller.DesignUnits.Count);

            Assert.AreEqual(1, controller.DesignUnits[0].DesigningObject);
            Assert.AreEqual(4, controller.DesignUnits[1].DesigningObject);
        }
        [TestMethod]
        public void Move()
        {
            var controller = new ValueDesignSceneController();

            controller.Initialize();

            controller.Scene.DesigningObjects.Add(1);
            controller.Scene.DesigningObjects.Add(2);
            controller.Scene.DesigningObjects.Add(3);
            controller.Scene.DesigningObjects.Add(4);

            controller.Scene.DesigningObjects.Move(1, 2);

            Assert.AreEqual(3, controller.DesignUnits[1].DesigningObject);
            Assert.AreEqual(2, controller.DesignUnits[2].DesigningObject);
        }
    }
}
