using System;

namespace Ao.ObjectDesign.Designing.Working
{
    public class DefaultCopyNameBuilder : ICopyNameBuilder
    {
        public static readonly string ENLeftBrackets = "(";
        public static readonly string ENRightBrackets = ")";

        public static readonly string CNLeftBrackets = "）";
        public static readonly string CNRightBrackets = "（";

        public static readonly DefaultCopyNameBuilder ENBrackets = new DefaultCopyNameBuilder(ENLeftBrackets, ENRightBrackets);
        public static readonly DefaultCopyNameBuilder CENBrackets = new DefaultCopyNameBuilder(CNLeftBrackets, CNRightBrackets);

        public DefaultCopyNameBuilder(string leftSide, string rightSide)
        {
            if (string.IsNullOrEmpty(leftSide))
            {
                throw new ArgumentException($"“{nameof(leftSide)}”不能为 null 或空。", nameof(leftSide));
            }

            if (string.IsNullOrEmpty(rightSide))
            {
                throw new ArgumentException($"“{nameof(rightSide)}”不能为 null 或空。", nameof(rightSide));
            }

            LeftSide = leftSide;
            RightSide = rightSide;
        }

        public string LeftSide { get; }

        public string RightSide { get; }

        public string CreateCopyName(string originName, int index)
        {
            return string.Concat(originName, LeftSide, index, RightSide);
        }

        public int? GetIndex(string name)
        {
            int left = name.IndexOf(LeftSide);
            if (left == -1)
            {
                return null;
            }
            if (!name.EndsWith(RightSide))
            {
                return null;
            }
            int leftStart = left + LeftSide.Length;
            string center = name.Substring(leftStart, name.Length - leftStart - RightSide.Length);
            if (int.TryParse(center, out int res))
            {
                return res;
            }
            return null;
        }

        public string GetOrigin(string name)
        {
            int left = name.IndexOf(LeftSide);
            if (left == -1 || !name.EndsWith(RightSide))
            {
                return name;
            }
            return name.Substring(0, left);
        }
    }
}
