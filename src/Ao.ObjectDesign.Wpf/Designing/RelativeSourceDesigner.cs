using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(RelativeSource))]
    public class RelativeSourceDesigner : NotifyableObject, IDefaulted
    {
        private RelativeSourceMode mode;
        private string ancestorType;
        private int ancestorLevel;

        [DefaultValue(RelativeSourceMode.FindAncestor)]
        public virtual RelativeSourceMode Mode
        {
            get => mode;
            set
            {
                Set(ref mode, value);
                RaiseRelativeSourceChanged();
            }
        }
        [DefaultValue(null)]
        [TransferOrigin(typeof(Type))]
        public virtual string AncestorType
        {
            get => ancestorType;
            set
            {
                Set(ref ancestorType, value);
                RaiseRelativeSourceChanged();
            }
        }
        [DefaultValue(-1)]
        public virtual int AncestorLevel
        {
            get => ancestorLevel;
            set
            {
                Set(ref ancestorLevel, value);
                RaiseRelativeSourceChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual RelativeSource GetRelativeSource()
        {
            if (string.IsNullOrEmpty(ancestorType))
            {
                return new RelativeSource(mode);
            }
            return new RelativeSource(mode, System.Type.GetType(ancestorType), ancestorLevel);
        }
        [PlatformTargetSetMethod]
        public virtual void SetRelativeSource(RelativeSource value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                AncestorLevel = value.AncestorLevel;
                Mode = value.Mode;
                AncestorType = value.AncestorType?.FullName;
            }
        }

        private static readonly PropertyChangedEventArgs relativeSourceEventArgs = new PropertyChangedEventArgs(nameof(RelativeSource));
        protected void RaiseRelativeSourceChanged()
        {
            RaisePropertyChanged(relativeSourceEventArgs);
        }

        public void SetDefault()
        {
            Mode = RelativeSourceMode.Self;
            AncestorType = null;
            AncestorLevel = 1;
        }
    }
}
