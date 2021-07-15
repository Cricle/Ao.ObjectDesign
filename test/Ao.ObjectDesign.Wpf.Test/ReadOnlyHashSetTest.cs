using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class ReadOnlyHashSetTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ReadOnlyHashSet<int>((IEnumerable<int>)null));
            Assert.ThrowsException<ArgumentNullException>(() => new ReadOnlyHashSet<int>((HashSet<int>)null));
        }
        private ReadOnlyHashSet<T> CreateReadOnlyHashSet<T>(T[] datas, bool useHashSet)
        {
            ReadOnlyHashSet<T> set;
            if (useHashSet)
            {
                set = new ReadOnlyHashSet<T>(new HashSet<T>(datas));
            }
            else
            {
                set = new ReadOnlyHashSet<T>(datas);
            }
            return set;
        }
        [TestMethod]
        [DataRow(new int[0], false)]
        [DataRow(new int[0], true)]
        [DataRow(new int[] { 1 }, false)]
        [DataRow(new int[] { 1 }, true)]
        [DataRow(new int[] { 11, 99 }, false)]
        [DataRow(new int[] { 11, 99 }, true)]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, false)]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, true)]
        public void GivenArrayInit_MustContainsThem(int[] datas, bool useHashSet)
        {
            var set = CreateReadOnlyHashSet(datas, useHashSet);
            Assert.AreEqual(datas.Length, set.Count);
            foreach (var item in datas)
            {
                Assert.IsTrue(set.Contains(item), $"Fail to contains {item}");
            }
        }
        [TestMethod]
        [DataRow(new int[0], false)]
        [DataRow(new int[0], true)]
        [DataRow(new int[] { 1 }, false)]
        [DataRow(new int[] { 1 }, true)]
        [DataRow(new int[] { 11, 99 }, false)]
        [DataRow(new int[] { 11, 99 }, true)]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, false)]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, true)]
        public void GivenArrayInit_GetEnumerable_MustGorEnumerater(int[] datas, bool useHashSet)
        {
            var set = CreateReadOnlyHashSet(datas, useHashSet);
            var enu = set.GetEnumerator();
            while (enu.MoveNext())
            {
                var exists = datas.Contains(enu.Current);
                Assert.IsTrue(exists, $"Fail to contains {enu.Current}");
            }

            var nenu = ((IEnumerable)set).GetEnumerator();
            while (nenu.MoveNext())
            {
                var exists = datas.Contains((int)nenu.Current);
                Assert.IsTrue(exists, $"Fail to contains {enu.Current}");
            }
        }
    }
}
