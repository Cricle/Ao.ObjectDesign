using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class SilentObservableCollectionTest
    {
        [TestMethod]
        public void GivenValueInit_MustContains()
        {
            new SilentObservableCollection<int>();

            var coll = new SilentObservableCollection<int>(new[] { 1, 2, 3, 4 });
            Assert.AreEqual(4, coll.Count);
            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(i, coll[i-1]);
            }
            coll = new SilentObservableCollection<int>(new List<int> { 1, 2, 3, 4 });
            Assert.AreEqual(4, coll.Count);
            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(i, coll[i - 1]);
            }
        }
        [TestMethod]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 1 })]
        [DataRow(new int[] { 1, 2, 3 })]
        [DataRow(new int[] { 1, 1, 0, 34, 1, 2, 4 })]
        public void AddRange_AllMustAdded(int[] datas)
        {
            var coll = new SilentObservableCollection<int>();
            coll.AddRange(datas);
            Assert.AreEqual(datas.Length, coll.Count);
            for (int i = 0; i < datas.Length; i++)
            {
                Assert.AreEqual(datas[i], coll[i], $"Fail to equal {datas[i]} and {coll[i]}");
            }
        }
        [TestMethod]
        public void BatchClear_MustCleared()
        {
            var coll = new SilentObservableCollection<int>
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
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, eg.Action);
            Assert.IsNull(eg.NewItems);
            Assert.AreEqual(6,eg.OldItems.Count);
        }
        [TestMethod]
        public void Sort_MustSortedByProperty()
        {
            var coll = new SilentObservableCollection<int>
            {
                5,1,2,3,4,5,7,1,-1
            };
            var order = coll.OrderBy(x => x).ToArray();
            var orderDesc = coll.OrderByDescending(x => x).ToArray();

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
