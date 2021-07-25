using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [MappingFor(typeof(StaticResourceExtension))]
    public class StaticResourceExtensionDesigner : NotifyableObject
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
        public virtual StaticResourceExtension StaticResourceExtension
        {
            get => string.IsNullOrEmpty(key) ? null : new StaticResourceExtension { ResourceKey = key };
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
