using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(TileBrush))]
    public abstract class TileBrushDesigner : NotifyableObject, IMiddlewareDesigner<TileBrush>
    {
        private static readonly string ViewportPropertyName = "Viewport.$";
        private static readonly string ViewboxPropertyName = "Viewbox.$";
        public static readonly IReadOnlyHashSet<string> IncludePropertyNames = new ReadOnlyHashSet<string>(new HashSet<string>
        {
            nameof(TileMode),
            nameof(Stretch),
            nameof(Viewbox),
            nameof(Viewport),
            nameof(AlignmentY),
            nameof(AlignmentX),
            nameof(ViewportUnits),
            nameof(ViewboxUnits),
            ViewportPropertyName,
            ViewboxPropertyName
        });
        protected TileBrushDesigner()
        {
        }
        private TileMode tileMode;
        private Stretch stretch;
        private RectDesigner viewbox;
        private RectDesigner viewport;
        private AlignmentY alignmentY;
        private BrushMappingMode viewportUnits;
        private AlignmentX alignmentX;
        private BrushMappingMode viewboxUnits;

        public virtual BrushMappingMode ViewboxUnits
        {
            get => viewboxUnits;
            set
            {
                Set(ref viewboxUnits, value);
            }
        }

        public virtual AlignmentX AlignmentX
        {
            get => alignmentX;
            set
            {
                Set(ref alignmentX, value);
            }
        }

        public virtual BrushMappingMode ViewportUnits
        {
            get => viewportUnits;
            set
            {
                Set(ref viewportUnits, value);
            }
        }

        public virtual AlignmentY AlignmentY
        {
            get => alignmentY;
            set
            {
                Set(ref alignmentY, value);
            }
        }

        public virtual RectDesigner Viewport
        {
            get => viewport;
            set
            {
                if (viewport != null)
                {
                    viewport.PropertyChanged -= OnViewportPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnViewportPropertyChanged;
                }
                Set(ref viewport, value);
            }
        }

        private void OnViewportPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(ViewportPropertyName);
        }

        public virtual RectDesigner Viewbox
        {
            get => viewbox;
            set
            {
                if (viewbox != null)
                {
                    viewbox.PropertyChanged -= OnViewboxPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnViewboxPropertyChanged;
                }
                Set(ref viewbox, viewbox);
            }
        }

        private void OnViewboxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(ViewboxPropertyName);
        }

        public virtual Stretch Stretch
        {
            get => stretch;
            set
            {
                Set(ref stretch, value);
            }
        }

        public virtual TileMode TileMode
        {
            get => tileMode;
            set
            {
                Set(ref tileMode, value);
            }
        }

        public virtual void SetDefault()
        {
            TileMode = TileMode.None;
            Stretch = Stretch.None;
            Viewbox = null;
            Viewport = null;
            AlignmentY = AlignmentY.Center;
            AlignmentX = AlignmentX.Center;
            ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
        }
        public virtual void WriteTo(TileBrush brush)
        {
            if (brush != null)
            {
                brush.TileMode = tileMode;
                brush.Stretch = stretch;
                if (viewbox != null)
                {
                    brush.Viewbox = viewbox.Rect;
                }
                if (viewport != null)
                {
                    brush.Viewport = viewport.Rect;
                }
                brush.AlignmentY = alignmentY;
                brush.AlignmentX = alignmentX;
                brush.ViewportUnits = viewportUnits;
                brush.ViewboxUnits = viewboxUnits;
            }
        }
        public virtual void Apply(TileBrush brush)
        {
            if (brush is null)
            {
                SetDefault();
            }
            else
            {
                TileMode = brush.TileMode;
                Stretch = brush.Stretch;
                Viewbox = new RectDesigner { Rect = brush.Viewbox };
                Viewport = new RectDesigner { Rect = brush.Viewport };
                AlignmentY = brush.AlignmentY;
                AlignmentX = brush.AlignmentX;
                ViewportUnits = brush.ViewportUnits;
                ViewboxUnits = brush.ViewboxUnits;
            }
        }
    }
}
