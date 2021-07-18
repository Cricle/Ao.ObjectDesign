using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(RelativeSource))]
    public class RelativeSourceDesigner : NotifyableObject, IDefaulted
    {
        private RelativeSourceMode mode;
        private string ancestorType;
        private int ancestorLevel;

        public virtual RelativeSourceMode Mode
        {
            get => mode;
            set
            {
                Set(ref mode, value);
            }
        }
        [TransferOrigin(typeof(Type))]
        public virtual string AncestorType
        {
            get => ancestorType;
            set
            {
                Set(ref ancestorType, value);
            }
        }
        public virtual int AncestorLevel
        {
            get => ancestorLevel;
            set
            {
                Set(ref ancestorLevel, value);
            }
        }
        [PlatformTargetProperty]
        public virtual RelativeSource RelativeSource
        {
            get
            {
                if (string.IsNullOrEmpty(ancestorType))
                {
                    return new RelativeSource(mode);
                }
                return new RelativeSource(mode, System.Type.GetType(ancestorType), ancestorLevel);
            }
            set
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
        }

        public void SetDefault()
        {
            Mode = RelativeSourceMode.Self;
            AncestorType = null;
            AncestorLevel = 1;
        }
    }
}
