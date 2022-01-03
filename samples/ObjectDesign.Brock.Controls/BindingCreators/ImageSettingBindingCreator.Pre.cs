using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class ImageSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> ImageSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> ImageSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return Image.StretchProperty.Creator(nameof(ImageSetting.Stretch))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Image.StretchDirectionProperty.Creator(nameof(ImageSetting.StretchDirection))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Image.SourceProperty.Creator(nameof(ImageSetting.Source)+ ".ImageSource" )
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetImageSettingToUI(ImageSetting obj, DependencyObject ui)
        {
            ui.SetValue(Image.StretchProperty, obj.Stretch);
            ui.SetValue(Image.StretchDirectionProperty, obj.StretchDirection);
            ui.SetValue(Image.SourceProperty, obj.Source?.GetImageSource());
        }
    }
}
