using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Point))]
    public class PointDesigner : NotifyableObject
    {
        private double x;
        private double y;

        [DefaultValue(0d)]
        public virtual double X
        {
            get => x;
            set
            {
                Set(ref x, value);
                RaisePointChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Y
        {
            get => y;
            set
            {
                Set(ref y, value);
                RaisePointChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual Point GetPoint()
        {
            return new Point(x, y);
        }
        [PlatformTargetSetMethod]
        public virtual void SetPoint(Point value)
        {
            X = value.X;
            Y = value.Y;
        }


        private static readonly PropertyChangedEventArgs pointChangedEventArgs = new PropertyChangedEventArgs(nameof(Point));
        protected void RaisePointChanged()
        {
            RaisePropertyChanged(pointChangedEventArgs);
        }
    }
}
