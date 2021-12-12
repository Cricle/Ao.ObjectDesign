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
                if (value != blurEffect)
                {
                    CoreSetBlurEffect(value);
                }
            }
        }
        public virtual DropShadowEffectDesigner DropShadowEffect
        {
            get => dropShadowEffect;
            set
            {
                if (value != dropShadowEffect)
                {
                    CoreSetDropShadowEffect(value);
                }
            }
        }

        public virtual EffectTypes Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    Set(ref type, value);
                    RaiseEffectChanged();
                }
            }
        }
        [PlatformTargetGetMethod]
        [return: ProvideMulityValues]
        [return:PropertyProvideValue(nameof(BlurEffect), typeof(BlurEffect))]
        [return: PropertyProvideValue(nameof(DropShadowEffect), typeof(DropShadowEffect))]
        public virtual Effect GetEffect()
        {
            switch (type)
            {
                case EffectTypes.BlurEffect:
                    return blurEffect?.GetBlurEffect();
                case EffectTypes.DropShadowEffect:
                    return dropShadowEffect?.GetDropShadowEffect();
                default:
                    return null;
            }
        }
        [PlatformTargetSetMethod]
        public virtual void SetEffect(Effect value)
        {
            if (value is null)
            {
                Type = EffectTypes.None;
            }
            else if (value is BlurEffect bf)
            {
                Type = EffectTypes.BlurEffect;
                BlurEffect = new BlurEffectDesigner ();
                BlurEffect.SetBlurEffect(bf);
            }
            else if (value is DropShadowEffect dse)
            {
                Type = EffectTypes.DropShadowEffect;
                DropShadowEffect = new DropShadowEffectDesigner ();
                DropShadowEffect.SetDropShadowEffect(dse);
            }
            else
            {
                throw new NotSupportedException(value.GetType().FullName);
            }
            RaiseEffectChanged();
        }

        private void CoreSetBlurEffect(BlurEffectDesigner value)
        {
            if (blurEffect != null)
            {
                blurEffect.PropertyChanged -= OnPropertyChanged;
            }
            if (value != null)
            {
                value.PropertyChanged += OnPropertyChanged;
            }
            blurEffect = value;
            RaiseEffectChanged();
        }
        private void CoreSetDropShadowEffect(DropShadowEffectDesigner value)
        {
            if (dropShadowEffect != null)
            {
                dropShadowEffect.PropertyChanged -= OnPropertyChanged;
            }
            if (value != null)
            {
                value.PropertyChanged += OnPropertyChanged;
            }
            dropShadowEffect = value;
            RaiseEffectChanged();
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseEffectChanged();
        }
        protected void RaiseEffectChanged()
        {
            RaisePropertyChanged(effectChangedEventArgs);
        }
    }
}
