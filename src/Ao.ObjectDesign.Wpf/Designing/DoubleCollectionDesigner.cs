using Ao.ObjectDesign.Wpf.Annotations;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(DoubleCollection))]
    public class DoubleCollectionDesigner : SilentObservableCollection<double>
    {
        private static readonly PropertyChangedEventArgs DoubleCollectionChangedEventArgs = new PropertyChangedEventArgs(nameof(DoubleCollection));
        public DoubleCollectionDesigner()
        {
            CollectionChanged += OnDoubleCollectionSettingCollectionChanged;
        }

        private void OnDoubleCollectionSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(DoubleCollectionChangedEventArgs);
        }

        [PlatformTargetProperty]
        public virtual DoubleCollection DoubleCollection
        {
            get => new DoubleCollection(this);
            set
            {
                Clear();
                if (value != null)
                {
                    AddRange(value);
                }
            }
        }
    }
}
