using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Input;
using System.Collections;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IDesignSessionDesignable<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        bool LazyBinding { get; set; }

        TScene Scene { get; }

        DesignSuface Suface { get; }

        WpfSceneManager<TScene, TSetting> SceneManager { get; }

        IPropertyPanel<TScene, TSetting> PropertyPanel { get; }

        WpfObjectDesigner ObjectDesigner { get; }

        FrameworkElement Root { get; }

        IList ElementContriner { get; }

        AccessInputBindings InputBindings { get; }
    }
}
