using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Working
{
    public static class CopyNameBuilderExtensions
    {
        public static string GenerateCopyName(this ICopyNameBuilder builder, string name, IEnumerable<string> sameNames)
        {
            string origin = builder.GetOrigin(name);
            int? lastIndex = sameNames
                .Where(x => x.StartsWith(origin))
                .Select(x => builder.GetIndex(x))
                .Where(x => x.HasValue)
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();
            if (!lastIndex.HasValue)
            {
                lastIndex = 0;
            }
            Debug.Assert(lastIndex != null);
            return builder.CreateCopyName(origin, lastIndex.Value + 1);
        }
    }
}
