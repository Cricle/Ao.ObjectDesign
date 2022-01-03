using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public static class DesigningDataHelper<TScene, TDesignObject>
        where TScene : IDesignScene<TDesignObject>
    {
        public static IPropertyPanel<TScene, TDesignObject> GetPropertyPanel(DependencyObject obj)
        {
            return (IPropertyPanel<TScene, TDesignObject>)obj.GetValue(PropertyPanelProperty);
        }

        public static void SetPropertyPanel(DependencyObject obj, IPropertyPanel<TScene, TDesignObject> value)
        {
            obj.SetValue(PropertyPanelProperty, value);
        }



        public static SceneEngine<TScene, TDesignObject> GetEngine(DependencyObject obj)
        {
            return (SceneEngine<TScene, TDesignObject>)obj.GetValue(EngineProperty);
        }

        public static void SetEngine(DependencyObject obj, SceneEngine<TScene, TDesignObject> value)
        {
            obj.SetValue(EngineProperty, value);
        }

        public static readonly DependencyProperty EngineProperty =
            DependencyProperty.RegisterAttached("Engine", typeof(SceneEngine<TScene, TDesignObject>), typeof(DesigningDataHelper<TScene, TDesignObject>), new PropertyMetadata(null));

        public static readonly DependencyProperty PropertyPanelProperty =
            DependencyProperty.RegisterAttached("PropertyPanel", typeof(IPropertyPanel<TScene, TDesignObject>), typeof(DesigningDataHelper<TScene, TDesignObject>), new PropertyMetadata(null));


    }
}
