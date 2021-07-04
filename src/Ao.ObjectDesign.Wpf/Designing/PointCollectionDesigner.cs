using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                foreach (var item in e.NewItems.OfType<PointDesigner>())
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<PointDesigner>())
                {
                    item.PropertyChanged -= OnItemPropertyChanged;
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
