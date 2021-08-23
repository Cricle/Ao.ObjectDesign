using Ao.ObjectDesign.Designing.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test.Working
{
    [TestClass]
    public class DefaultCopyNameBuilderTest
    {
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            var left = "[";
            var right = "]";

            Assert.ThrowsException<ArgumentException>(() => new DefaultCopyNameBuilder(left, null));
            Assert.ThrowsException<ArgumentException>(() => new DefaultCopyNameBuilder(null, right));

            var builder = DefaultCopyNameBuilder.CENBrackets;
            var name = "adsa";
            var enu = Enumerable.Empty<string>();

            Assert.ThrowsException<ArgumentNullException>(() => CopyNameBuilderExtensions.GenerateCopyName(null, name, enu));
            Assert.ThrowsException<ArgumentNullException>(() => CopyNameBuilderExtensions.GenerateCopyName(builder, null, enu));
            Assert.ThrowsException<ArgumentNullException>(() => CopyNameBuilderExtensions.GenerateCopyName(builder, name, null));
        }
        [TestMethod]
        public void Consts()
        {
            Assert.AreEqual(DefaultCopyNameBuilder.CENBrackets, DefaultCopyNameBuilder.CENBrackets);
            Assert.AreEqual(DefaultCopyNameBuilder.ENBrackets, DefaultCopyNameBuilder.ENBrackets);
        }
        [TestMethod]
        [DataRow("aaa", "aaa（1）")]
        [DataRow("aaa（1）", "aaa（2）")]
        [DataRow("aaa（9）", "aaa（10）")]
        [DataRow("aaa（22）", "aaa（23）")]
        [DataRow("aaa（1）", "aaa（2）")]
        public void CNBrackets_Create(string input,string output)
        {
            var res = DefaultCopyNameBuilder.CENBrackets.GenerateCopyName(input, new string[] { input });
            Assert.AreEqual(output, res);
        }
        [TestMethod]
        [DataRow("aaa", "aaa(1)")]
        [DataRow("aaa(1)", "aaa(2)")]
        [DataRow("aaa(9)", "aaa(10)")]
        [DataRow("aaa(22)", "aaa(23)")]
        [DataRow("aaa(1)", "aaa(2)")]
        public void ENBrackets_Create(string input, string output)
        {
            var res = DefaultCopyNameBuilder.ENBrackets.GenerateCopyName(input, new string[] { input });
            Assert.AreEqual(output, res);
        }
        [TestMethod]
        public void Init()
        {
            var left = "[";
            var right = "]";

            var builder = new DefaultCopyNameBuilder(left, right);
            Assert.AreEqual(left, builder.LeftSide);
            Assert.AreEqual(right, builder.RightSide);
        }
        [TestMethod]
        [DataRow("[", "]")]
        [DataRow("!", "@")]
        [DataRow("$", "$")]
        [DataRow("&", "&")]
        [DataRow("@@", "@@")]
        [DataRow("[[", "]]")]
        [DataRow("{{", "}}")]
        [DataRow("--", "--")]
        [DataRow("_+", "+_")]
        [DataRow("[", "}")]
        [DataRow("[{", "]}")]
        [DataRow("$#", "_=")]
        [DataRow("$#!@#@$#@!", "&*^%$#$%^#%#>>")]
        [DataRow("<", ">")]
        [DataRow("<<", ">>")]
        [DataRow("《《", "》》")]
        [DataRow("左括号", "右括号")]
        [DataRow("  ", "  ")]
        public void GenerateWithEmpty(string left,string right)
        {
            var val = "sdagwika123";
            var exp = val + left + "1" + right;

            var builder = new DefaultCopyNameBuilder(left, right);

            var reuslt = builder.GenerateCopyName(val, Enumerable.Empty<string>());

            Assert.AreEqual(exp,reuslt);
        }
        [TestMethod]
        [DataRow("[", "]", "aa[1]", 1)]
        [DataRow("[", "]", "aa[12]", 12)]
        [DataRow("[", "]", "aa[", null)]
        [DataRow("[", "]", "aa[1", null)]
        [DataRow("[{", "}]", "aa[{1}]", 1)]
        [DataRow("[{", "}]", "aa[{1}]", 1)]
        [DataRow("[{", "}]", "aa[{}]", null)]
        [DataRow("[{", "}]", "aa[{200}]", 200)]
        [DataRow("[{", "]", "aa[{1]", 1)]
        public void GetIndex(string left,string right,string value,int? index)
        {
            var builder = new DefaultCopyNameBuilder(left, right);

            var reuslt = builder.GetIndex(value);

            Assert.AreEqual(index, reuslt);

        }
    }
}
