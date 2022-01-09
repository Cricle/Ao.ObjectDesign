using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public static class SceneManagerHitExtensions
    {
        public static IDesignPair<TUI, TDesignObject> HitTest<TUI, TScene, TDesignObject>(this SceneManager<TUI, TScene, TDesignObject> sm, IVector point)
            where TDesignObject : IPositionBounded
            where TScene : IDesignScene<TDesignObject>
        {
            return sm.HitTest(x => x.GetBounds(), point);
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> EnumerableHitTest<TUI, TScene, TDesignObject>(this SceneManager<TUI, TScene, TDesignObject> sm, IVector point)
           where TDesignObject : IPositionBounded
           where TScene : IDesignScene<TDesignObject>
        {
            return sm.EnumerableHitTest(x => x.GetBounds(), point);
        }
    }
}
