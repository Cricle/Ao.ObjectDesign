using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Session.BindingCreators;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public partial class MediaElementSettingBindingCreator : FrameworkElementSettingBindingCreator
    {
        public MediaElementSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        protected override void SetToUI()
        {
            base.SetToUI();
            SetMediaElementSettingToUI((MediaElementSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        public override void Attack()
        {
            base.Attack();
            var media = (MediaElement)DesignUnit.UI;
            var setting = (MediaElementSetting)DesignUnit.DesigningObject;
            media.MediaEnded += OnMediaMediaEnded;
            media.MediaFailed += OnMediaMediaFailed;
            setting.PropertyChanged += OnSettingPropertyChanged;
        }

        private void OnSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName==nameof(MediaElementSetting.Source))
            {
                var setting = (MediaElementSetting)DesignUnit.DesigningObject;
                OnSourceChanged(setting.Source);
            }
        }

        private void OnSourceChanged(object value)
        {
            var media = (MediaElement)DesignUnit.UI;
            media.Play();
        }

        private void OnMediaMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }

        private void OnMediaMediaEnded(object sender, RoutedEventArgs e)
        {
            var setting = (MediaElementSetting)DesignUnit.DesigningObject;
            if (setting.IsForever)
            {
                var media = (MediaElement)sender;
                media.Position = TimeSpan.Zero;
            }
        }

        public override void Detack()
        {
            base.Detack();
            var media = (MediaElement)DesignUnit.UI;
            var setting = (MediaElementSetting)DesignUnit.DesigningObject;
            media.MediaEnded -= OnMediaMediaEnded;
            media.MediaFailed -= OnMediaMediaFailed;
            setting.PropertyChanged -= OnSettingPropertyChanged;
            media.Close();
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings().Concat(MediaElementSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}
