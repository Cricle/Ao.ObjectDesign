using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Test.Level
{
    internal class ValueDesignSceneController : DesignSceneController<string, int>
    {
        public ValueDesignSceneController()
        {
            UIElements = new List<IDesignPair<string, int>>();
        }

        public List<IDesignPair<string, int>> UIElements { get; }
        public override IObservableDeisgnScene<int> GetScene()
        {
            return new ValueScene<int>();
        }

        protected override void AddUIElement(IDesignPair<string, int> unit)
        {
            UIElements.Add(unit);
        }

        protected override string CreateUI(int designingObject)
        {
            return designingObject.ToString();
        }

        protected override void RemoveUIElement(IDesignPair<string, int> unit)
        {
            UIElements.Remove(unit);
        }
    }
}
