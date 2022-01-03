using Ao.ObjectDesign;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign.Input;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Components;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class TextInputBinding: PreviewMouseInputBase
    {
        private static readonly IReadOnlyHashSet<Type> hiddenPanelTypes = new ReadOnlyHashSet<Type>(new Type[] 
        {
            typeof(TextBox)
        });

        public TextInputBinding(IDesignSession<Scene,UIElementSetting> session)
        {
            Session = session;
        }

        public IDesignSession<Scene, UIElementSetting> Session { get; }
        private DateTime? time;
        private Window currentWindow;
        private Point p1;
        private UIElement target;
        private static bool CanSelect(in Point p1, in Point p2)
        {
            return Math.Abs(p2.X - p1.X) < SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(p2.Y - p1.Y) < SystemParameters.MinimumVerticalDragDistance;
        }
        public override void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(sender as UIElement);
            if (DateTime.Now - time < TimeSpan.FromMilliseconds(500)&&
                CanSelect(p1,p))
            {
                var ds = Session.Suface.DesigningObjects;
                if (ds != null && ds.Length == 1 && hiddenPanelTypes.Contains(ds[0].GetType()))
                {
                    target = ds[0];
                    Session.Suface.Visibility = Visibility.Hidden;
                    currentWindow = Application.Current.MainWindow;
                    currentWindow.PreviewKeyDown += OnMainWindowPreviewKeyDown;
                }
                time = null;
                p1 = default;
            }
            time = DateTime.Now;
            p1 = p;
        }

        private void OnMainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                Session.Suface.Visibility = Visibility.Visible;
                Session.Suface.Focus();
                Keyboard.Focus(Session.Suface);
                Session.Only(target);
                Session.Suface.UpdateInRender();
                target = null;
                currentWindow.PreviewKeyDown -= OnMainWindowPreviewKeyDown;
            }
        }
    }
}
