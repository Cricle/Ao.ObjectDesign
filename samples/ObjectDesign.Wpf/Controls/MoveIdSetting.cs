using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows.Media;

namespace ObjectDesign.Wpf.Controls
{
    [MappingFor(typeof(MoveId))]
    public class MoveIdSetting : NotifyableObject, IMiddlewareDesigner<MoveId>
    {
        public MoveIdSetting()
        {
            SetDefault();
        }
        private string text;
        private BrushDesigner idBrush;
        private double idFontSize;

        public virtual double IdFontSize
        {
            get => idFontSize;
            set => Set(ref idFontSize, value);
        }
        public virtual BrushDesigner IdBrush
        {
            get => idBrush;
            set => Set(ref idBrush, value);
        }
        public virtual string Text
        {
            get => text;
            set => Set(ref text, value);
        }
        public void SetDefault()
        {
            IdFontSize = 12;
            IdBrush = new BrushDesigner { Brush = Brushes.Black };
            Text = null;
        }

        public void Apply(MoveId value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                IdFontSize = value.IdFontSize;
                IdBrush = new BrushDesigner { Brush = value.IdBrush };
                Text = value.Text;

            }
        }

        public void WriteTo(MoveId value)
        {
            if (value != null)
            {
                value.IdFontSize = idFontSize;
                value.IdBrush = idBrush?.Brush;
                value.Text = text;
            }
        }
    }
}
