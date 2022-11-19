using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Designing
{
    [MappingFor(typeof(StaticResourceExtension))]
    public class StaticResourceExtensionDesigner : NotifyableObject
    {
        private string key;

        [DefaultValue(null)]
        public virtual string Key
        {
            get => key;
            set
            {
                Set(ref key, value);
                RaiseStaticResourceExtensionChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual StaticResourceExtension GetStaticResourceExtension()
        {
            return string.IsNullOrEmpty(key) ? null : new StaticResourceExtension { ResourceKey = key };
        }
        [PlatformTargetSetMethod]
        public virtual void SetStaticResourceExtension(StaticResourceExtension value)
        {
            if (value is null)
            {
                Key = null;
            }
            else
            {
                Key = value.ResourceKey?.ToString();
            }
        }

        private static readonly PropertyChangedEventArgs staticResourceExtensionEventArgs = new PropertyChangedEventArgs(nameof(StaticResourceExtension));
        protected void RaiseStaticResourceExtensionChanged()
        {
            RaisePropertyChanged(staticResourceExtensionEventArgs);
        }
    }
}
