using Ao.ObjectDesign.Bindings.Annotations;
using Ao.ObjectDesign.Designing.Annotations;
using ObjectDesign.Brock.BindingCreators;
using ObjectDesign.Brock.Controls;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Brock.Components
{
    [MappingFor(typeof(Image))]
    [BindingCreatorFactory(typeof(RelativeFileImageSettingBindingCreatorFactory))]
    public class RelativeFileImageSetting : FrameworkElementSetting
    {
        private static readonly PropertyChangedEventArgs resourceIdentityEventArgs = new PropertyChangedEventArgs(nameof(ResourceIdentity));

        private ResourceIdentity resourceIdentity;
        private Stretch stretch;
        private StretchDirection stretchDirection;

        public StretchDirection StretchDirection
        {
            get => stretchDirection;
            set => Set(ref stretchDirection, value);
        }

        public Stretch Stretch
        {
            get => stretch;
            set => Set(ref stretch, value);
        }

        public ResourceIdentity ResourceIdentity
        {
            get => resourceIdentity;
            set => Set(ref resourceIdentity, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            Stretch = Stretch.Fill;
            StretchDirection = StretchDirection.Both;
            ResourceIdentity = new ResourceIdentity();
        }
    }
}
