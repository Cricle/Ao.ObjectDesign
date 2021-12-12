using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Thickness))]
    public class ThicknessDesigner : NotifyableObject
    {
        private double left;
        private double top;
        private double right;
        private double bottom;
        [PlatformTargetGetMethod]
        public virtual Thickness GetThickness()
        {
            return new Thickness(left, top, right, bottom);
        }
        [PlatformTargetSetMethod]
        public virtual void SetThickness(Thickness value)
        {
            Left = value.Left;
            Top = value.Top;
            Right = value.Top;
            Bottom = value.Bottom;
        }


        [DefaultValue(0d)]
        public virtual double Bottom
        {
            get => bottom;
            set
            {
                Set(ref bottom, value);
                RaiseThicknessChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Right
        {
            get => right;
            set
            {
                Set(ref right, value);
                RaiseThicknessChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Top
        {
            get => top;
            set
            {
                Set(ref top, value);
                RaiseThicknessChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Left
        {
            get => left;
            set
            {
                Set(ref left, value);
                RaiseThicknessChanged();
            }
        }

        public void Uniform(double uniformLength)
        {
            SetThickness(new Thickness(uniformLength));
        }

        private static readonly PropertyChangedEventArgs thicknessEventArgs = new PropertyChangedEventArgs(nameof(Thickness));
        protected void RaiseThicknessChanged()
        {
            RaisePropertyChanged(thicknessEventArgs);
        }
    }
}
