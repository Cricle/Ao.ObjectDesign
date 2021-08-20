using System;

namespace Ao.ObjectDesign.Designing.Working
{
    public static class WorkplaceCopyExtensions
    {
        public static string GetCopyNameWithBrackets<TResource>(this IWorkplace<string, TResource> workplace, string sourceName)
        {
            return GetCopyName(workplace, sourceName, DefaultCopyNameBuilder.ENBrackets);
        }
        public static string GetCopyName<TResource>(this IWorkplace<string, TResource> workplace, string sourceName, ICopyNameBuilder builder)
        {
            if (workplace is null)
            {
                throw new ArgumentNullException(nameof(workplace));
            }

            return builder.GenerateCopyName(sourceName, workplace.Resources);
        }
    }
}
