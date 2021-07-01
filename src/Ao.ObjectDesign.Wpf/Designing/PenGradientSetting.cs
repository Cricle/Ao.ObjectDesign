using Ao.ObjectDesign.Wpf.Annotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class PenGradientSetting : NotifyableObject
    {
        private const string PenGradientStopsChangedName = nameof(PenGradientStops) + "." + "Items";

        public PenGradientSetting()
        {
            PenGradientStops.CollectionChanged += OnPenGradientStopsCollectionChanged;
        }

        private void OnPenGradientStopsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var news = e.NewItems.OfType<PenGradientStop>();
                foreach (var item in news)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                var news = e.NewItems.OfType<PenGradientStop>();
                foreach (var item in news)
                {
                    item.PropertyChanged -= OnItemPropertyChanged;
                }
            }
            RaisePropertyChanged(PenGradientStopsChangedName);
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(PenGradientStopsChangedName);
        }

        private ColorInterpolationMode colorInterpolationMode;
        private BrushMappingMode mappingMode;
        private GradientSpreadMethod spreadMethod;
        private double opacity = 1;

        public virtual double Opacity
        {
            get => opacity;
            set => Set(ref opacity, value);
        }

        public virtual GradientSpreadMethod SpreadMethod
        {
            get => spreadMethod;
            set => Set(ref spreadMethod, value);
        }

        public virtual BrushMappingMode MappingMode
        {
            get => mappingMode;
            set => Set(ref mappingMode, value);
        }

        public virtual ColorInterpolationMode ColorInterpolationMode
        {
            get => colorInterpolationMode;
            set => Set(ref colorInterpolationMode, value);
        }

        public virtual SilentObservableCollection<PenGradientStop> PenGradientStops { get; } = new SilentObservableCollection<PenGradientStop>();

        [PlatformTargetProperty]
        public virtual IEnumerable<GradientStop> GradientStops => PenGradientStops.Select(x => new GradientStop(x.Color, x.Offset));
    }
}
