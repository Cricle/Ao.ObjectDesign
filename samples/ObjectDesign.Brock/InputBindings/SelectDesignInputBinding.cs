using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Input;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ObjectDesign.Brock.InputBindings
{
    internal class SelectDesignInputBinding : PreviewMouseInputBase
    {
        private readonly MySceneMakerRuntime runtime;
        private readonly UIDesignMap designMap;

        private Point startPoint;

        public SelectDesignInputBinding(MySceneMakerRuntime runtime, UIDesignMap designMap)
        {
            this.designMap = designMap;
            this.runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
        }

        public override void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (runtime.HasSession)
            {
                var session = runtime.CurrentSession;
                Point endPoint = e.GetPosition(session.Root);
                if (CanSelect(endPoint, startPoint))
                {
                    session.ThrowIfNoInitialized();

                    TopMostHitResult result = new TopMostHitResult();
                    result.designMap = designMap;

                    VisualTreeHelper.HitTest(
                        session.Root,
                        new HitTestFilterCallback(result.NoNested2DFilter),
                        new HitTestResultCallback(result.HitTestResult),
                        new PointHitTestParameters(endPoint));
                    var visual = result?.lasthit;
                    if (visual is null)
                    {
                        return;
                    }
                    var targetUnit = session.SceneManager.CurrentSceneController.Find((x, y) => y.UI == visual)
                        .FirstOrDefault();

                    var isControlOn = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
                    if (targetUnit != null)
                    {
                        if (isControlOn)
                        {
                            session.ToggleDesignObjectsByElements(targetUnit.UI);
                        }
                        else
                        {
                            session.Only(targetUnit);
                        }
                    }
                    else if (!isControlOn)
                    {
                        session.Suface.ClearDesignObjects();
                    }
                }
                session.Suface.UpdateInRender();
            }
        }
        internal class TopMostHitResult
        {
            internal HitTestResult _hitResult;
            internal Visual lasthit;
            internal UIDesignMap designMap;

            internal HitTestResultBehavior HitTestResult(HitTestResult result)
            {
                _hitResult = result;

                return HitTestResultBehavior.Stop;
            }

            internal HitTestFilterBehavior NoNested2DFilter(DependencyObject potentialHitTestTarget)
            {
                if (potentialHitTestTarget is Viewport2DVisual3D)
                {
                    return HitTestFilterBehavior.ContinueSkipChildren;
                }
                if (potentialHitTestTarget is Visual v && designMap.HasUIType(v.GetType()))
                {
                    lasthit = v;
                }
                return HitTestFilterBehavior.Continue;
            }
        }
        public override void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (runtime.HasSession)
            {
                startPoint = e.GetPosition(runtime.CurrentSession.Root);
            }
        }
        private static bool CanSelect(in Point p1, in Point p2)
        {
            return Math.Abs(p2.X - p1.X) < SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(p2.Y - p1.Y) < SystemParameters.MinimumVerticalDragDistance;
        }
    }
}
