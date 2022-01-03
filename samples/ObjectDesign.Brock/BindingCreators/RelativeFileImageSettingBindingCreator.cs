using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.WpfDesign;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System;
using System.ComponentModel;
using WpfAnimatedGif;
using ObjectDesign.Brock.Components;
using Ao.ObjectDesign.Session.Annotations;
using ObjectDesign.Brock.Controls.BindingCreators;
using Ao.ObjectDesign.Bindings;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Converters;
using ObjectDesign.Brock.Services;

namespace ObjectDesign.Brock.BindingCreators
{
    [BindingCreatorFor(typeof(RelativeFileImageSetting), typeof(Image))]
    public partial class RelativeFileImageSettingBindingCreator : FrameworkElementSettingBindingCreator
    {
        public RelativeFileImageSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        protected override void SetToUI()
        {
            base.SetToUI();
            var setting = (RelativeFileImageSetting)DesignUnit.DesigningObject;
            SetRelativeFileImageSettingToUI(setting, DesignUnit.UI);
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

        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            foreach (var item in base.GenerateBindings().Concat(RelativeFileImageSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject))))
            {
                yield return item;
            }
            //var imgMgr = State.GetRequiredService<ImageManager>();
            //yield return Image.SourceProperty.Creator(nameof(RelativeFileImageSetting.ResourceIdentity))
            //    .AddSetConfig(BindingMode.TwoWay, UpdateSourceTrigger.Default)
            //    .Add(x => 
            //    {
            //        x.Converter = new ResourceIdentityImageConverter { ImageManager = imgMgr };
            //        x.Delay = 30;
            //    })
            //    .Build()
            //    .ToWithSource(DesignUnit.DesigningObject);


        }
    }
}
