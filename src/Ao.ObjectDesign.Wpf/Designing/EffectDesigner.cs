using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.ComponentModel;
using System.Windows.Media.Effects;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Effect))]
    public class EffectDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs effectChangedEventArgs = new PropertyChangedEventArgs(nameof(Effect));

        private EffectTypes type;
        private BlurEffectDesigner blurEffect;
        private DropShadowEffectDesigner dropShadowEffect;

        public virtual BlurEffectDesigner BlurEffect
        {
            get => blurEffect;
            set
            {
                Set(ref blurEffect, value);
                RaiseEffectChanged();
            }
        }
        public virtual DropShadowEffectDesigner DropShadowEffect
        {
            get => dropShadowEffect;
            set
            {
                Set(ref dropShadowEffect, value);
                RaiseEffectChanged();
            }
        }

        public virtual EffectTypes Type
        {
            get => type;
            set
            {
                Set(ref type, value);
                RaiseEffectChanged();
            }
        }
        [PlatformTargetProperty]
        [PropertyProvideValue(nameof(BlurEffect), typeof(BlurEffect))]
        [PropertyProvideValue(nameof(DropShadowEffect), typeof(DropShadowEffect))]
        public virtual Effect Effect
        {
            get
            {
                switch (type)
                {
                    case EffectTypes.BlurEffect:
                        return blurEffect?.BlurEffect;
                    case EffectTypes.DropShadowEffect:
                        return dropShadowEffect?.DropShadowEffect;
                    default:
                        return null;
                }
            }
            set
            {
                if (value is null)
                {
                    Type = EffectTypes.None;
                }
                else if (value is BlurEffect bf)
                {
                    Type = EffectTypes.BlurEffect;
                    BlurEffect = new BlurEffectDesigner { BlurEffect = bf };
                }
                else if (value is DropShadowEffect dse)
                {
                    Type = EffectTypes.DropShadowEffect;
                    DropShadowEffect = new DropShadowEffectDesigner { DropShadowEffect = dse };
                }
                else
                {
                    throw new NotSupportedException(value.GetType().FullName);
                }
            }
        }
        protected void RaiseEffectChanged()
        {
            RaisePropertyChanged(effectChangedEventArgs);
        }
    }
}
