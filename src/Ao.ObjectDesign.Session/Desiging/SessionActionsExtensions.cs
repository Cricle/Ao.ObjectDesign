using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public static class SessionActionsExtensions
    {
        public static void SetDesignObjectsByElements<TScene, TSetting>(this IDesignSession<TScene, TSetting> session,
            IReadOnlyList<UIElement> elements,
            Func<IDesignPair<UIElement, TSetting>, bool> keepFunc)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (elements is null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            if (keepFunc is null)
            {
                throw new ArgumentNullException(nameof(keepFunc));
            }

            DesignSufaceActionsExtensions.SetDesignObjectsByElements<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                elements,
                keepFunc);
        }
        public static void SetDesignObjectsBySettings<TScene, TSetting>(this IDesignSession<TScene, TSetting> session,
           IReadOnlyList<TSetting> settings,
           Func<IDesignPair<UIElement, TSetting>, bool> keepFunc)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (keepFunc is null)
            {
                throw new ArgumentNullException(nameof(keepFunc));
            }

            DesignSufaceActionsExtensions.SetDesignObjectsBySettings<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                settings,
                keepFunc);
        }
        public static void ToggleDesignObjectsBySettings<TScene, TSetting>(this IDesignSession<TScene, TSetting> session,
            IReadOnlyList<TSetting> settings)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            DesignSufaceActionsExtensions.ToggleDesignObjectsBySettings<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                settings);
        }
        public static void ToggleDesignObjectsByElements<TScene, TSetting>(this IDesignSession<TScene, TSetting> session,
            IReadOnlyList<UIElement> elements)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (elements is null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            DesignSufaceActionsExtensions.ToggleDesignObjectsByElements<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                elements);
        }
        public static void ToggleDesignObjectsByElements<TScene, TSetting>(this IDesignSession<TScene, TSetting> session,
           UIElement element)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            DesignSufaceActionsExtensions.ToggleDesignObjectsByElements<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                element);
        }
        public static void Only<TScene, TSetting>(this IDesignSession<TScene, TSetting> session, UIElement element)
            where TScene : IDesignScene<TSetting>
        {
            DesignSufaceActionsExtensions.Only<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                element);
        }
        public static void Only<TScene, TSetting>(this IDesignSession<TScene, TSetting> session, TSetting setting)
            where TScene : IDesignScene<TSetting>
        {
            DesignSufaceActionsExtensions.Only<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                setting);
        }
        public static void Only<TScene, TSetting>(this IDesignSession<TScene, TSetting> session,
          IDesignPair<UIElement, TSetting> pair)
            where TScene : IDesignScene<TSetting>
        {
            DesignSufaceActionsExtensions.Only<TScene, TSetting>(session.Suface,
                session.SceneManager.CurrentSceneController,
                pair);
        }
        public static IEnumerable<IDesignPair<UIElement, TSetting>> GetPair<TScene, TSetting>(this IDesignSession<TScene, TSetting> session, TSetting setting)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            session.ThrowIfNoInitialized();
            return session.SceneManager.CurrentSceneController
                .Find((x, y) => EqualityComparer<TSetting>.Default.Equals(y.DesigningObject, setting));
        }
        public static IEnumerable<IDesignPair<UIElement, TSetting>> GetPair<TScene, TSetting>(this IDesignSession<TScene, TSetting> session, UIElement element)
            where TScene : IDesignScene<TSetting>
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            session.ThrowIfNoInitialized();
            return session.SceneManager.CurrentSceneController.Find((x, y) => y.UI == element);
        }
    }
}
