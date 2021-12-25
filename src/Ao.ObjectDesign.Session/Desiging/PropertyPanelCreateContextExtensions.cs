using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public static class PropertyPanelCreateContextExtensions
    {
        public static IEnumerable CreateContexts<TScene, TSetting>(this IPropertyPanel<TScene, TSetting> panel, UIElement ui)
            where TScene : IDesignScene<TSetting>
        {
            if (panel is null)
            {
                throw new ArgumentNullException(nameof(panel));
            }

            if (ui is null)
            {
                throw new ArgumentNullException(nameof(ui));
            }

            var pair = panel.Session.SceneManager.CurrentSceneController.Find((x, y) => y.UI == ui).FirstOrDefault();
            if (pair is null)
            {
                return null;
            }
            return panel.CreateContexts(pair);
        }
        public static IEnumerable CreateContexts<TScene, TSetting>(this IPropertyPanel<TScene, TSetting> panel, TSetting setting)
            where TScene : IDesignScene<TSetting>
        {
            if (panel is null)
            {
                throw new ArgumentNullException(nameof(panel));
            }

            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            var pair = panel.Session.SceneManager.CurrentSceneController
                .Find((x, y) =>EqualityComparer<TSetting>.Default.Equals(y.DesigningObject ,setting))
                .FirstOrDefault();
            if (pair is null)
            {
                return null;
            }
            return panel.CreateContexts(pair);
        }
    }
}
