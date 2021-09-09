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
            for (int i = 0; i < scene.DesigningObjects.Count; i++)
            {
                var v = scene.DesigningObjects[i];
                if (v is IDesignScene<TDesignObject> nextScene && nextScene is TDesignObject)
                {
                    var vx = (TDesignObject)Clone(nextScene, sceneClone, objectClone);
                    val.DesigningObjects.Add(vx);
                }
                else
                {
                    val.DesigningObjects.Add(objectClone(v));
                }
            }
            return val;
        }
    }
}
