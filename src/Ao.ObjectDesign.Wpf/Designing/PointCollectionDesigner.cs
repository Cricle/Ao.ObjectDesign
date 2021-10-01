using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [MappingFor(typeof(PointCollection))]
    public class PointCollectionDesigner : SilentObservableCollection<PointDesigner>
    {
        public PointCollectionDesigner()
        {
            CollectionChanged += OnPointCollectionSettingCollectionChanged;
        }

        private void OnPointCollectionSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var len = e.NewItems.Count;
                for (int i = 0; i < len; i++)
                {
                    var item = e.NewItems[i];
                    if (item is PointDesigner designer)
                    {
                        designer.PropertyChanged += OnItemPropertyChanged;
                    }
                }
            }
            if (e.OldItems != null)
            {
                var len = e.OldItems.Count;
                for (int i = 0; i < len; i++)
                {
                    var item = e.OldItems[i];
                    if (item is PointDesigner designer)
                    {
                        designer.PropertyChanged -= OnItemPropertyChanged;
                    }
                }
            }
            OnPropertyChanged(PointCollectionChangedEventArgs);
        }
        private static readonly PropertyChangedEventArgs PointCollectionChangedEventArgs =
            new PropertyChangedEventArgs(nameof(PointCollection));

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(PointCollectionChangedEventArgs);
        }
        [PlatformTargetProperty]
        public virtual PointCollection PointCollection
        {
            get => new PointCollection(this.Select(p => new Point(p.X, p.Y)));
            set
            {
                Clear();
                if (value != null)
                {
                    AddRange(value.Select(x => new PointDesigner { Point = x }));
                }
            }
        }
    }
}
