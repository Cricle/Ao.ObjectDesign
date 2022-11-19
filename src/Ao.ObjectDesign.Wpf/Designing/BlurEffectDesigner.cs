using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media.Effects;

namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(BlurEffect))]
    public class BlurEffectDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs blurEffectChangedEventArgs = new PropertyChangedEventArgs(nameof(BlurEffect));

        public BlurEffectDesigner()
        {
            SetBlurEffect(null);
        }

        private double radius;
        private KernelType kernelType;
        private RenderingBias renderingBias;

        [DefaultValue(5.0)]
        public virtual double Radius
        {
            get => radius;
            set
            {
                Set(ref radius, value);
                RaiseBlurEffectChanged();
            }
        }
        [DefaultValue(KernelType.Gaussian)]
        public virtual KernelType KernelType
        {
            get => kernelType;
            set
            {
                Set(ref kernelType, value);
                RaiseBlurEffectChanged();
            }
        }
        [DefaultValue(RenderingBias.Performance)]
        public virtual RenderingBias RenderingBias
        {
            get => renderingBias;
            set
            {
                Set(ref renderingBias, value);
                RaiseBlurEffectChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual BlurEffect GetBlurEffect()
        {
            return new BlurEffect
            {
                Radius = radius,
                KernelType = kernelType,
                RenderingBias = renderingBias
            };
        }
        [PlatformTargetSetMethod]
        public virtual void SetBlurEffect(BlurEffect value)
        {
            if (value == null)
            {
                Radius = 5.0;
                KernelType = KernelType.Gaussian;
                RenderingBias = RenderingBias.Performance;
            }
            else
            {
                Radius = value.Radius;
                KernelType = value.KernelType;
                RenderingBias = value.RenderingBias;
            }
        }
        protected void RaiseBlurEffectChanged()
        {
            RaisePropertyChanged(blurEffectChangedEventArgs);
        }
    }
}
