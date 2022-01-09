using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(DoubleCollection))]
    public class DoubleCollectionDesigner : DynamicSilentObservableCollection<double>
    {
        private static readonly PropertyChangedEventArgs DoubleCollectionChangedEventArgs = new PropertyChangedEventArgs(nameof(DoubleCollection));
        public DoubleCollectionDesigner()
        {
            CollectionChanged += OnDoubleCollectionSettingCollectionChanged;
        }

        private void OnDoubleCollectionSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(DoubleCollectionChangedEventArgs);
        }
        [PlatformTargetGetMethod]
        public virtual DoubleCollection GetDoubleCollection()
        {
            return new DoubleCollection(this);
        }
        [PlatformTargetSetMethod]
        public virtual void SetDoubleCollection(DoubleCollection value)
        {
            Clear();
            if (value != null)
            {
                AddRange(value);
            }
        }
    }
}
