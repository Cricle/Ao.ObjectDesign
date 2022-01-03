using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class CtrlCopyInputBinding : PreviewMouseInputBase
    {
        public CtrlCopyInputBinding(IDesignSession<Scene,UIElementSetting> session,
            ActionSettingService actionSettingService)
        {
            Debug.Assert(session != null);
            Debug.Assert(actionSettingService != null);
            Session = session;
            ActionSettingService = actionSettingService;
        }

        public ActionSettingService ActionSettingService { get; }

        public IDesignSession<Scene, UIElementSetting> Session { get; }

        private bool ok;
        private bool created;
        private Point startPoint;

        public override void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                var objs = Session.Suface.DesigningObjects;
                if (objs is null || objs.Length == 0)
                {
                    return;
                }
                startPoint = e.GetPosition(Session.Root);
                ok = true;
                created = false;
            }
        }
        public override void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!created && ok)
            {
                var pos = e.GetPosition(Session.Root);
                if (CanSelect(startPoint, pos))
                {
                    ActionSettingService.MulityCopy(1);
                    created = true;
                }
            }
        }
        public override void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ok = false;
            created = false;
            startPoint = default;
        }
        private static bool CanSelect(in Point p1, in Point p2)
        {
            return Math.Abs(p2.X - p1.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(p2.Y - p1.Y) >= SystemParameters.MinimumVerticalDragDistance;
        }
    }
}
