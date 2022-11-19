using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf
{
    public partial class SceneMakerRuntime<TDesignTool, TScene, TDesignObject>
    {
        public SilentObservableCollection<WpfForViewBuildContextBase> DesigningContexts { get; }
            = new SilentObservableCollection<WpfForViewBuildContextBase>();

        public void SwithDesigningContexts(UIElement ui)
        {
            if (HasSession)
            {
                var pair = currentSession.GetPair(ui).FirstOrDefault();
                if (pair != null)
                {
                    SwithDesigningContexts(pair);
                }
            }
        }
        public void SwithDesigningContexts(TDesignObject setting)
        {
            if (HasSession)
            {
                var pair = currentSession.GetPair(setting).FirstOrDefault();
                if (pair != null)
                {
                    SwithDesigningContexts(pair);
                }
            }
        }
        public void SwithDesigningContexts(IDesignPair<UIElement, TDesignObject> pair)
        {
            if (HasSession)
            {
                var enu = currentSession.PropertyPanel.CreateContexts(pair);
                DesigningContexts.Clear();
                foreach (var item in enu)
                {
                    if (item is WpfForViewBuildContextBase ctx)
                    {
                        DesigningContexts.Add(ctx);
                    }
                }
            }
        }
    }
}
