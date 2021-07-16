using Ao.ObjectDesign.Wpf.Annotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(IEnumerable<GradientStop>))]
    public abstract class GradientBrushDesigner : NotifyableObject, IMiddlewareDesigner<GradientBrush>
    {
        protected static readonly IReadOnlyHashSet<string> IncludePropertyNames = new ReadOnlyHashSet<string>(new HashSet<string>
        {
            nameof(Opacity),
            nameof(SpreadMethod),
            nameof(MappingMode),
            nameof(ColorInterpolationMode)
        });
        private const string PenGradientStopsChangedName = nameof(PenGradientStops) + "." + "Items";

        protected GradientBrushDesigner()
        {
        }

        private void OnPenGradientStopsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var news = e.NewItems.OfType<GradientStopDesigner>();
                foreach (var item in news)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                var news = e.NewItems.OfType<GradientStopDesigner>();
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
        private BrushMappingMode mappingMode = BrushMappingMode.RelativeToBoundingBox;
        private GradientSpreadMethod spreadMethod = GradientSpreadMethod.Pad;
        private double opacity = 1;
        private SilentObservableCollection<GradientStopDesigner> penGradientStops;

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

        public virtual SilentObservableCollection<GradientStopDesigner> PenGradientStops
        {
            get => penGradientStops;
            set
            {
                if (penGradientStops!=null)
                {
                    penGradientStops.CollectionChanged -= OnPenGradientStopsCollectionChanged;
                }
                if (value!=null)
                {
                    value.CollectionChanged -= OnPenGradientStopsCollectionChanged;
                }
                Set(ref penGradientStops, value);
            }
        }

        [PlatformTargetProperty]
        public virtual IEnumerable<GradientStop> GradientStops
        {
            get
            {
                var stops = penGradientStops;
                if (stops is null)
                {
                    return Enumerable.Empty<GradientStop>();
                }
                return PenGradientStops.Select(x => x.GradientStop);
            }
        }

        public virtual void SetDefault()
        {
            ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;
            MappingMode = BrushMappingMode.RelativeToBoundingBox;
            Opacity = 1;
            SpreadMethod = GradientSpreadMethod.Pad;
            PenGradientStops = new SilentObservableCollection<GradientStopDesigner>();
        }

        public virtual void WriteTo(GradientBrush brush)
        {
            if (brush != null)
            {
                brush.ColorInterpolationMode = ColorInterpolationMode;
                brush.MappingMode = MappingMode;
                brush.Opacity = Opacity;
                brush.SpreadMethod = SpreadMethod;
                brush.GradientStops = new GradientStopCollection(GradientStops);
            }
        }

        public virtual void Apply(GradientBrush brush)
        {
            PenGradientStops.Clear();
            if (brush is null)
            {
                ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;
                MappingMode = BrushMappingMode.RelativeToBoundingBox;
                Opacity = 1;
                SpreadMethod = GradientSpreadMethod.Pad;
            }
            else
            {
                ColorInterpolationMode = brush.ColorInterpolationMode;
                MappingMode = brush.MappingMode;
                Opacity = brush.Opacity;
                SpreadMethod = brush.SpreadMethod;
                if (brush.GradientStops != null && brush.GradientStops.Count != 0)
                {
                    PenGradientStops = new SilentObservableCollection<GradientStopDesigner>(brush.GradientStops.Select(x => new GradientStopDesigner(x.Color, x.Offset)));
                }
            }
        }
    }
}
