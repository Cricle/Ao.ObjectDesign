using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign
{
    [TestClass]
    public class ReadOnlyHashSetTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ReadOnlyHashSet<int>((IEnumerable<int>)null));
            Assert.ThrowsException<ArgumentNullException>(() => new ReadOnlyHashSet<int>((HashSet<int>)null));
            Assert.ThrowsException<ArgumentNullException>(() => new ReadOnlyHashSet<string>(null,StringComparer.OrdinalIgnoreCase));
            Assert.ThrowsException<ArgumentNullException>(() => new ReadOnlyHashSet<string>(new string[0], null));
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
            ReadOnlyHashSet<int> set = CreateReadOnlyHashSet(datas, useHashSet);
            Assert.AreEqual(datas.Length, set.Count);
            foreach (int item in datas)
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
            ReadOnlyHashSet<int> set = CreateReadOnlyHashSet(datas, useHashSet);
            IEnumerator<int> enu = set.GetEnumerator();
            while (enu.MoveNext())
            {
                bool exists = datas.Contains(enu.Current);
                Assert.IsTrue(exists, $"Fail to contains {enu.Current}");
            }

            IEnumerator nenu = ((IEnumerable)set).GetEnumerator();
            while (nenu.MoveNext())
            {
                bool exists = datas.Contains((int)nenu.Current);
                Assert.IsTrue(exists, $"Fail to contains {enu.Current}");
            }
        }

        [TestMethod]
        public void OtherMethods()
        {
            var set = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var readonlySet = new ReadOnlyHashSet<int>(set);
            var arr = new int[] { 3, 4, 5, 6, 7 };

            var res = set.IsProperSubsetOf(arr);
            var res1 = readonlySet.IsProperSubsetOf(arr);
            Assert.AreEqual(res, res1);

            res = set.IsProperSupersetOf(arr);
            res1 = readonlySet.IsProperSupersetOf(arr);
            Assert.AreEqual(res, res1);

            res = set.IsSubsetOf(arr);
            res1 = readonlySet.IsSubsetOf(arr);
            Assert.AreEqual(res, res1);

            res = set.IsSupersetOf(arr);
            res1 = readonlySet.IsSupersetOf(arr);
            Assert.AreEqual(res, res1);

            res = set.Overlaps(arr);
            res1 = readonlySet.Overlaps(arr);
            Assert.AreEqual(res, res1);

            res = set.SetEquals(arr);
            res1 = readonlySet.SetEquals(arr);
            Assert.AreEqual(res, res1);
        }
    }
}
