using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(PropertyPath))]
    public class PropertyPathDesigner : NotifyableObject
    {
        private string path;

        [DefaultValue(null)]
        public virtual string Path
        {
            get => path;
            set
            {
                Set(ref path, value);
                RaisePropertyPathChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual PropertyPath GetPropertyPath()
        {
            return string.IsNullOrEmpty(path) ? null : new PropertyPath(path);
        }
        [PlatformTargetSetMethod]
        public virtual void SetPropertyPath(PropertyPath value)
        {
            if (value is null)
            {
                Path = null;
            }
            else
            {
                Path = value.Path;
            }
        }

        private static readonly PropertyChangedEventArgs propertyPathEventArgs = new PropertyChangedEventArgs(nameof(PropertyPath));
        protected void RaisePropertyPathChanged()
        {
            RaisePropertyChanged(propertyPathEventArgs);
        }
    }
}
