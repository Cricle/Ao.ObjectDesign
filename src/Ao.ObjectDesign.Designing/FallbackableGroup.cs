using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Count = {Count}")]
    public class FallbackableGroup : List<IFallbackable>, IFallbackable
    {
        public FallbackableGroup()
            : this(FallbackModes.Forward)
        {
        }

        public FallbackableGroup(FallbackModes mode)
        {
            Mode= mode;
        }

        public FallbackableGroup(FallbackModes mode, int capacity) : base(capacity)
        {
            Mode = mode;
        }

        public FallbackableGroup(FallbackModes mode, IEnumerable<IFallbackable> collection) : base(collection)
        {
            Mode = mode;
        }
        
        public FallbackModes Mode { get; }

        public IFallbackable Copy(FallbackModes? mode)
        {
            var m = Mode;
            if (mode != null)
            {
                m = mode.Value;
            }
            var group = new FallbackableGroup(m, Count);
            foreach (var item in this)
            {
                var copied = item.Copy(mode);
                group.Add(copied);
            }
            return group;
        }

        public void Fallback()
        {
            foreach (var item in this)
            {
                item.Fallback();
            }
        }

        public bool IsReverse(IFallbackable fallbackable)
        {
            if (fallbackable is FallbackableGroup group &&
                group.Count == Count)
            {
                for (int i = 0; i < Count; i++)
                {
                    var me = this[i];
                    var other = group[i];
                    if (!me.IsReverse(other))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public void ListReverse()
        {
            base.Reverse();
        }
        public new IFallbackable Reverse()
        {
            var group = new FallbackableGroup(FallbackModes.Reverse, Count);
            foreach (var item in this)
            {
                var copied = item.Reverse();
                group.Add(copied);
            }
            return group;
        }
    }
}