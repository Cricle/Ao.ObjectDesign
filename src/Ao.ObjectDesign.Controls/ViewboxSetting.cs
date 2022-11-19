using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Viewbox))]
    public class ViewboxSetting : FrameworkElementSetting, IMiddlewareDesigner<Viewbox>
    {
        private Stretch stretch;
        private StretchDirection stretchDirection;

        [DefaultValue(StretchDirection.Both)]
        public virtual StretchDirection StretchDirection
        {
            get => stretchDirection;
            set => Set(ref stretchDirection, value);
        }

        [DefaultValue(Stretch.None)]
        public virtual Stretch Stretch
        {
            get => stretch;
            set => Set(ref stretch, value);
        }

        public override void SetDefault()
        {
            ReflectionHelper.SetDefault(this, SetDefaultOptions.IgnoreNotNull | SetDefaultOptions.Deep | SetDefaultOptions.ClassGenerateNew);
        }

        public void Apply(Viewbox value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                FlatReflectionHelper.SpecularMapping(value, this);
                //Apply((FrameworkElement)value);
                //Stretch = value.Stretch;
                //StretchDirection = value.StretchDirection;
            }
        }

        public void WriteTo(Viewbox value)
        {
            if (value != null)
            {
                FlatReflectionHelper.SpecularMapping(this, value);
                //WriteTo((FrameworkElement)value);
                //value.Stretch = Stretch;
                //value.StretchDirection = StretchDirection;
            }
        }
    }
}
