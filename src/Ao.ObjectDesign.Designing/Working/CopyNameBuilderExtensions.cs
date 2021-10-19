using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Working
{
    public static class CopyNameBuilderExtensions
    {
        public static string GenerateCopyName(this ICopyNameBuilder builder, string name, IEnumerable<string> sameNames)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (sameNames is null)
            {
                throw new ArgumentNullException(nameof(sameNames));
            }

            string origin = builder.GetOrigin(name);
            int? lastIndex = null;
            foreach (var item in sameNames)
            {
                if (item.StartsWith(origin))
                {
                    var idx = builder.GetIndex(item);
                    if (idx != null)
                    {
                        if (lastIndex is null)
                        {
                            lastIndex = idx;
                        }
                        else
                        {
                            lastIndex = Math.Max(idx.Value, lastIndex.Value);
                        }
                    }
                }
            }
            if (!lastIndex.HasValue)
            {
                lastIndex = 0;
            }
            Debug.Assert(lastIndex != null);
            return builder.CreateCopyName(origin, lastIndex.Value + 1);
        }
    }
}
