using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Globalization;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(CultureInfo))]
    public class CultureInfoDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs cultureInfoChangedEventArgs = new PropertyChangedEventArgs(nameof(CultureInfo));
        private string name;

        [DefaultValue(null)]
        public virtual string Name
        {
            get => name;
            set
            {
                Set(ref name, value);
                RaisePropertyChanged(cultureInfoChangedEventArgs);
            }
        }
        [PlatformTargetGetMethod]
        public virtual CultureInfo GetCultureInfo()
        {
            return string.IsNullOrEmpty(name) ? null : new CultureInfo(name);
        }
        [PlatformTargetSetMethod]
        public virtual void SetCultureInfo(CultureInfo value)
        {
            if (value is null)
            {
                Name = null;
            }
            else
            {
                Name = value.Name;
            }
            RaiseCultureInfoChanged();
        }
        private static readonly PropertyChangedEventArgs CultureInfoChangedEventArgs = new PropertyChangedEventArgs(nameof(CultureInfo));
        protected void RaiseCultureInfoChanged()
        {
            RaisePropertyChanged(CultureInfoChangedEventArgs);
        }

    }
}
