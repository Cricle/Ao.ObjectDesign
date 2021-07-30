using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class SilentObservableCollectionTest
    {
        [TestMethod]
        public void GivenValueInit_MustContains()
        {
            new SilentObservableCollection<int>();

            SilentObservableCollection<int> coll = new SilentObservableCollection<int>(new[] { 1, 2, 3, 4 });
            Assert.AreEqual(4, coll.Count);
            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(i, coll[i - 1]);
            }
            coll = new SilentObservableCollection<int>(new List<int> { 1, 2, 3, 4 });
            Assert.AreEqual(4, coll.Count);
            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(i, coll[i - 1]);
            }
        }
        public void AddRange_AllMustAdded<T>(T[] datas)
        {
            SilentObservableCollection<T> coll = new SilentObservableCollection<T>();
            object sender = null;
            NotifyCollectionChangedEventArgs args = null;
            coll.CollectionChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };
            coll.AddRange(datas);
            Assert.AreEqual(datas.Length, coll.Count);
            for (int i = 0; i < datas.Length; i++)
            {
                Assert.AreEqual(datas[i], coll[i], $"Fail to equal {datas[i]} and {coll[i]}");
            }
            Assert.AreEqual(coll, sender);
            Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);
            var contains = new HashSet<T>(args.NewItems.OfType<T>());
            foreach (var item in coll)
            {
                if (!contains.Contains(item))
                {
                    Assert.Fail("Not contains {0}", item);
                }
            }
        }

        [TestMethod]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 1 })]
        [DataRow(new int[] { 1, 2, 3 })]
        [DataRow(new int[] { 1, 1, 0, 34, 1, 2, 4 })]
        public void AddValueTypeRange_AllMustAdded(int[] datas)
        {
            AddRange_AllMustAdded(datas);
        }
        [TestMethod]
        public void AddRefTypesRange_AllMustAdded()
        {
            var objs = new object[10];
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = new object();
            }
            AddRange_AllMustAdded(objs);
        }
        [TestMethod]
        public void BatchClear_MustCleared()
        {
            SilentObservableCollection<int> coll = new SilentObservableCollection<int>
            {
                1,2,3,4,5,6
            };
            object sender = null;
            NotifyCollectionChangedEventArgs eg = null;
            coll.CollectionChanged += (o, e) =>
            {
                sender = o;
                eg = e;
            };
            coll.BatchClear();
            Assert.AreEqual(0, coll.Count);
            Assert.AreEqual(coll, sender);
            Assert.IsNotNull(eg);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset,eg.Action);
        }
        [TestMethod]
        public void Sort_MustSortedByProperty()
        {
            SilentObservableCollection<int> coll = new SilentObservableCollection<int>
            {
                5,1,2,3,4,5,7,1,-1
            };
            int[] order = coll.OrderBy(x => x).ToArray();
            int[] orderDesc = coll.OrderByDescending(x => x).ToArray();

            coll.Sort(x => x);
            for (int i = 0; i < order.Length; i++)
            {
                Assert.AreEqual(coll[i], order[i], $"Fail to equal order {i}");
            }
            coll.SortDescending(x => x);
            for (int i = 0; i < orderDesc.Length; i++)
            {
                Assert.AreEqual(coll[i], orderDesc[i], $"Fail to equal order desc {i}");
            }
        }
    }
}
