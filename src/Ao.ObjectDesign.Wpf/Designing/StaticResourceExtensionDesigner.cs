using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
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
        private static readonly PropertyChangedEventArgs staticResourceExtensionEventArgs = new PropertyChangedEventArgs(nameof(StaticResourceExtension));
        protected void RaiseStaticResourceExtensionChanged()
        {
            RaisePropertyChanged(staticResourceExtensionEventArgs);
        }
    }
}
