using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public struct NotifyableSetValidaterContext : IEquatable<NotifyableSetValidaterContext>
    {
        private bool isStopValidate;
        private bool isSkipGlobalValidate;
        private bool isSkipWithKeyValidate;

        public bool IsStopValidate => isStopValidate;
        public bool IsSkipGlobalValidate => isSkipGlobalValidate;
        public bool IsSkipWithKeyValidate => isSkipWithKeyValidate;

        public void StopValidate()
        {
            isStopValidate = true;
        }
        public void SkipGlobalValidate()
        {
            isSkipGlobalValidate = true;
        }
        public void SkipWithKeyValidate()
        {
            isSkipWithKeyValidate = true;
        }

        public bool Equals(NotifyableSetValidaterContext other)
        {
            return other.isSkipGlobalValidate == isSkipGlobalValidate &&
                other.isSkipWithKeyValidate == isSkipWithKeyValidate &&
                other.isStopValidate == isStopValidate;
        }
        public override bool Equals(object obj)
        {
            if (obj is NotifyableSetValidaterContext ctx)
            {
                return Equals(ctx);
            }
            return false;
        }
        public override int GetHashCode()
        {
            var code = 0;
            if (isStopValidate)
            {
                code |= 1;
            }
            if (isSkipWithKeyValidate)
            {
                code |= 1 << 1;
            }
            if (isSkipGlobalValidate)
            {
                code |= 1 << 2;
            }
            return code;
        }
        public override string ToString()
        {
            return $"{{IsStopValidate: {IsStopValidate} , IsSkipGlobalValidate: {IsSkipGlobalValidate}, IsSkipWithKeyValidate: {IsSkipWithKeyValidate}}}";
        }
    }
}
