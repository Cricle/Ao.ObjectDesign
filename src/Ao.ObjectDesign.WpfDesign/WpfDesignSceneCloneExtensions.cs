using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Level
{
    public static class WpfDesignSceneCloneExtensions
    {
        public static IDesignScene<TDesignObject> Clone<TDesignObject>(this IDesignScene<TDesignObject> scene,IReadOnlyHashSet<Type> ignoreTypes)
        {
            return scene.Clone(x =>(IDesignScene<TDesignObject>)ReflectionHelper.Clone(x.GetType(),x, ignoreTypes),
                x=> (TDesignObject)ReflectionHelper.Clone(x.GetType(), x, ignoreTypes));
        }
        public static IDesignScene<TDesignObject> Clone<TDesignObject>(this IDesignScene<TDesignObject> scene)
        {
            return scene.Clone(DesigningHelpers.KnowDesigningTypes);
        }
    }
}
