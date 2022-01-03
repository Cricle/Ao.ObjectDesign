using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Wpf;
using ObjectDesign.Brock.Components;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock
{
    public partial class SceneMakerRuntime<TDesignTool>
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
        public void SwithDesigningContexts(UIElementSetting setting)
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
        public void SwithDesigningContexts(IDesignPair<UIElement,UIElementSetting> pair)
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
