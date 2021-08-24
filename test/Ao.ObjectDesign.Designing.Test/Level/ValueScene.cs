using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Designing.Test.Level
{
    internal class ValueScene<T> : IObservableDeisgnScene<T>
    {
        public ValueScene()
        {
            DesigningObjects = new SilentObservableCollection<T>();
        }

        public SilentObservableCollection<T> DesigningObjects { get; }

        IList<T> IDeisgnScene<T>.DesigningObjects => DesigningObjects;
    }
}
