using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Controls.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class MediaElementSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> MediaElementSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> MediaElementSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return MediaElement.SourceProperty.Creator(nameof(MediaElementSetting.Source))
                .AddSetConfig(mode, trigger)
                .Add(x => x.Converter = StringUriConverter.Instance)
                .Build();
            yield return MediaElement.LoadedBehaviorProperty.Creator(nameof(MediaElementSetting.LoadedBehavior))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return MediaElement.StretchProperty.Creator(nameof(MediaElementSetting.Stretch))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return MediaElement.ScrubbingEnabledProperty.Creator(nameof(MediaElementSetting.ScrubbingEnabled))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetMediaElementSettingToUI(MediaElementSetting obj, DependencyObject ui)
        {
            ui.SetValue(MediaElement.LoadedBehaviorProperty, obj.LoadedBehavior);
            ui.SetValue(MediaElement.StretchProperty, obj.Stretch);
            ui.SetValue(MediaElement.ScrubbingEnabledProperty, obj.ScrubbingEnabled);
            if (Uri.TryCreate(obj.Source, UriKind.Absolute,out var uri))
            {
                ui.SetValue(MediaElement.SourceProperty, uri);
            }
        }
    }
}
