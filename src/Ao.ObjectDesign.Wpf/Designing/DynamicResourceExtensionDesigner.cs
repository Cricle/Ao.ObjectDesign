using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [MappingFor(typeof(DynamicResourceExtension))]
    public class DynamicResourceExtensionDesigner : NotifyableObject
    {
        private string key;

        public virtual string Key
        {
            get => key;
            set
            {
                Set(ref key, value);
                RaisePropertyChanged(nameof(StaticResourceExtension));
            }
        }
        [PlatformTargetProperty]
        public virtual DynamicResourceExtension DynamicResourceExtension
        {
            get => string.IsNullOrEmpty(key) ? null : new DynamicResourceExtension { ResourceKey = key };
            set
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
}
