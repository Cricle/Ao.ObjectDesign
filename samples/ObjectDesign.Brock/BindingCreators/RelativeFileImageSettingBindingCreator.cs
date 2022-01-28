using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Annotations;
using Ao.ObjectDesign.Wpf.Data;
using Microsoft.Extensions.DependencyInjection;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Controls.BindingCreators;
using ObjectDesign.Brock.Converters;
using ObjectDesign.Brock.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfAnimatedGif;

namespace ObjectDesign.Brock.BindingCreators
{
    [BindingCreatorFor(typeof(RelativeFileImageSetting), typeof(Image))]
    public partial class RelativeFileImageSettingBindingCreator : BrockAutoBindingCreator<Image, RelativeFileImageSetting>
    {
        public RelativeFileImageSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        public override void Attack()
        {
            base.Attack();
            var img = (RelativeFileImageSetting)DesignUnit.DesigningObject;
            img.PropertyChangeTo += OnImgPropertyChangeTo;
            if (img.ResourceIdentity != null)
            {
                img.ResourceIdentity.PropertyChanged += OnIdentityPropertyChanged;
                OnIdentityPropertyChanged(img.ResourceIdentity, null);
            }
        }

        public override void Detack()
        {
            base.Detack();
            var img = (RelativeFileImageSetting)DesignUnit.DesigningObject;
            img.PropertyChangeTo -= OnImgPropertyChangeTo;
            if (img.ResourceIdentity != null)
            {
                img.ResourceIdentity.PropertyChanged -= OnIdentityPropertyChanged;
            }
        }

        private void OnImgPropertyChangeTo(object sender, PropertyChangeToEventArgs e)
        {
            if (e.PropertyName == nameof(RelativeFileImageSetting.ResourceIdentity))
            {
                var from = (ResourceIdentity)e.From;
                var to = (ResourceIdentity)e.To;
                if (from != null)
                {
                    from.PropertyChanged -= OnIdentityPropertyChanged;
                }
                if (to != null)
                {
                    to.PropertyChanged += OnIdentityPropertyChanged;
                }
            }
        }

        private void OnIdentityPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var ri = (ResourceIdentity)sender;
            var ui = DesignUnit.UI;
            if (ri.Type == ResourceTypes.Unknow)
            {
                ui.SetValue(Image.SourceProperty, ImageManager.TranslateImage);
            }
            else if (ri.ResourceName != null && ri.ResourceName.EndsWith(".gif", StringComparison.InvariantCultureIgnoreCase))
            {
                var old = DesignerProperties.GetIsInDesignMode(ui);
                if (old)
                {
                    DesignerProperties.SetIsInDesignMode(ui, false);
                }
                ResourceIdentityImageConverter.SetWhenGif(ui, ResourceIdentityImageConverter.defaultFileSystem, ri);
                if (old)
                {
                    DesignerProperties.SetIsInDesignMode(ui, true);
                }
            }
            else
            {
                DesignUnit.UI.ClearValue(ImageBehavior.AnimatedSourceProperty);
                var imgMgr = State.GetRequiredService<ImageManager>();
                var img = imgMgr.GetImage(ri);
                ui.SetValue(Image.SourceProperty, img);
            }
        }

    }
}
