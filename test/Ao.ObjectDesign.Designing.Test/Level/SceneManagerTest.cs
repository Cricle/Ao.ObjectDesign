using Ao.ObjectDesign.Designing.Level;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test.Level
{
    internal class ValueSceneManager : SceneManager<string, ValueScene<int>, int>
    {
        public override DesignSceneController<string, int> CreateSceneController(ValueScene<int> scene)
        {
            return new ValueDesignSceneController();
        }
    }
    [TestClass]
    public class SceneManagerTest
    {
        [TestMethod]
        public void SetScene_BuildController()
        {
            var mgr = new ValueSceneManager();
            var scene = new ValueScene<int>();

            object sender1 = null;
            CurrentSceneChangedEventArgs<int> args1 = null;
            mgr.CurrentSceneChanged += (o1, e1) =>
            {
                sender1 = o1;
                args1 = e1;
            };
            object sender2 = null;
            CurrentSceneControllerChangedEventArgs<string, int> args2 = null;
            mgr.CurrentSceneControllerChanged += (o2, e2) =>
            {
                sender2 = o2;
                args2 = e2;
            };

            mgr.CurrentScene = scene;
            Assert.IsNotNull(mgr.CurrentSceneController);
            Assert.AreEqual(scene, mgr.CurrentScene);

            Assert.AreEqual(mgr, sender1);
            Assert.IsNull(args1.Old);
            Assert.AreEqual(scene, args1.New);


            Assert.AreEqual(mgr, sender2);
            Assert.IsNull(args2.Old);
            Assert.AreEqual(mgr.CurrentSceneController, args2.New);
        }
    }
}
