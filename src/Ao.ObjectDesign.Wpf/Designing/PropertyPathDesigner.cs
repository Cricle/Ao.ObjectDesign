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
            }
        }
        [PlatformTargetProperty]
        public virtual PropertyPath PropertyPath
        {
            get => string.IsNullOrEmpty(path) ? null : new PropertyPath(path);
            set
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
        }
    }
}
