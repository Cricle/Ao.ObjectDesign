using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Designing.Test.Level
{
    internal class ValueScene<T> : IObservableDesignScene<T>
    {
        public ValueScene()
        {
            DesigningObjects = new SilentObservableCollection<T>();
        }

        public SilentObservableCollection<T> DesigningObjects { get; }

        IList<T> IDesignScene<T>.DesigningObjects => DesigningObjects;
    }
}
