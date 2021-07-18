using Ao.ObjectDesign.Wpf.Annotations;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(PropertyPath))]
    public class PropertyPathDesigner : NotifyableObject
    {
        private string path;

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
