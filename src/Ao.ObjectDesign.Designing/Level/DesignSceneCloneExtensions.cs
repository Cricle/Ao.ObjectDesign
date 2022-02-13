using System;

namespace Ao.ObjectDesign.Designing.Level
{
    public static class DesignSceneCloneExtensions
    {
        public static IDesignScene<TDesignObject> Clone<TDesignObject>(this IDesignScene<TDesignObject> scene,
            Func<IDesignScene<TDesignObject>, IDesignScene<TDesignObject>> sceneClone,
            Func<TDesignObject, TDesignObject> objectClone)
        {
            if (scene is null)
            {
                throw new ArgumentNullException(nameof(scene));
            }

            if (sceneClone is null)
            {
                throw new ArgumentNullException(nameof(sceneClone));
            }

            if (objectClone is null)
            {
                throw new ArgumentNullException(nameof(objectClone));
            }

            var val = sceneClone(scene);
            return val;
        }
    }
}
