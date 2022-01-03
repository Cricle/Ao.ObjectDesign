using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Session.Desiging;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls
{
    public class DesignPanel : UserControl
    {
        public IDesignSession<Scene,UIElementSetting> DesignSession
        {
            get { return (IDesignSession<Scene, UIElementSetting>)GetValue(DesignSessionProperty); }
            set { SetValue(DesignSessionProperty, value); }
        }

        public FrameworkElement Suface
        {
            get { return (FrameworkElement)GetValue(SufaceProperty); }
        }

        public static readonly DependencyProperty SufaceProperty =
            DependencyProperty.Register("Suface", typeof(FrameworkElement), typeof(DesignPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty DesignSessionProperty =
            DependencyProperty.Register("DesignSession", typeof(IDesignSession<Scene, UIElementSetting>), typeof(DesignPanel), new PropertyMetadata(null, OnDesignSessionPropertyChangedCallback));


        private static void OnDesignSessionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (DesignPanel)d;
            if (e.NewValue is IDesignSession<Scene, UIElementSetting> session)
            {
                session.ThrowIfNoInitialized();
                panel.SetValue(ContentProperty, session.Root);
                panel.SetValue(SufaceProperty, session.Suface);
            }
            else
            {
                panel.SetValue(ContentProperty, null);
                panel.SetValue(SufaceProperty, null);
            }
        }

    }
}
