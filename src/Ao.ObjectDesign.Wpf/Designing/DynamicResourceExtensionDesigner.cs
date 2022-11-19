using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Designing
{
    [MappingFor(typeof(DynamicResourceExtension))]
    public class DynamicResourceExtensionDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs dynamicResourceExtensionChangedEventArgs = new PropertyChangedEventArgs(nameof(DynamicResourceExtension));
        private string key;

        [DefaultValue(null)]
        public virtual string Key
        {
            get => key;
            set
            {
                Set(ref key, value);
                RaisePropertyChanged(dynamicResourceExtensionChangedEventArgs);
            }
        }
        [PlatformTargetGetMethod]
        public virtual DynamicResourceExtension GetDynamicResourceExtension()
        {
            return string.IsNullOrEmpty(key) ? null : new DynamicResourceExtension { ResourceKey = key };
        }
        [PlatformTargetSetMethod]
        public virtual void SetDynamicResourceExtension(DynamicResourceExtension value)
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

    }
}
