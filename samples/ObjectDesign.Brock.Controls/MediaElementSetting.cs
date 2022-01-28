using Ao.ObjectDesign.Designing.Annotations;
using ObjectDesign.Brock.Components;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(MediaElement))]
    public class MediaElementSetting : FrameworkElementSetting
    {
        private string source;
        private MediaState loadedBehavior;
        private Stretch stretch;
        private bool isForever;
        private bool scrubbingEnabled;

        public bool ScrubbingEnabled
        {
            get => scrubbingEnabled;
            set => Set(ref scrubbingEnabled, value);
        }

        public bool IsForever
        {
            get => isForever;
            set => Set(ref isForever, value);
        }

        public Stretch Stretch
        {
            get => stretch;
            set => Set(ref stretch, value);
        }

        public MediaState LoadedBehavior
        {
            get => loadedBehavior;
            set => Set(ref loadedBehavior, value);
        }

        public string Source
        {
            get => source;
            set => Set(ref source, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            IsForever = true;
            ScrubbingEnabled = true;
            LoadedBehavior = MediaState.Manual;
            Stretch = Stretch.Fill;
            Source = null;
        }
    }
}
