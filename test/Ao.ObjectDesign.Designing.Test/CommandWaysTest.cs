using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class CommandWaysTest
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(10)]
        public void GivenAnyValueBound_Pop_MustAsDequene(int count)
        {
            CommandWays<int> wasy = new CommandWays<int>
            {
                MaxSize = 10
            };
            for (int i = 0; i < count; i++)
            {
                wasy.Push(i);
            }
            Assert.AreEqual(count, wasy.Count);
            for (int i = count; i > 0; i--)
            {
                int val = wasy.Pop();
                Assert.AreEqual(i - 1, val);
            }
        }
        [TestMethod]
        public void GivenZeroMaxSize_AddOne_MustNotStore()
        {
            CommandWays<int> wasy = new CommandWays<int>
            {
                MaxSize = 0
            };

            int idx = 0;
            object sender = null;
            CommandWaysOperatorEventArgs<int> args = null;
            wasy.WayChanged += (o, e) =>
            {
                sender = o;
                args = e;
                idx++;
            };
            wasy.Push(1);
            Assert.IsNull(sender);
            Assert.IsNull(args);
            Assert.AreEqual(0, idx);
            Assert.AreEqual(0, wasy.Count);
        }
        [TestMethod]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(30)]
        public void GivenAnyValueBound_AddOne_MustResize(int size)
        {
            CommandWays<int> wasy = new CommandWays<int>
            {
                MaxSize = size
            };
            for (int i = 0; i < size; i++)
            {
                wasy.Push(i);
            }
            int idx = 0;
            object[] sender = new object[2];
            CommandWaysOperatorEventArgs<int>[] args = new CommandWaysOperatorEventArgs<int>[2];
            wasy.WayChanged += (o, e) =>
            {
                sender[idx] = o;
                args[idx] = e;
                idx++;
            };
            wasy.Push(-1);
            Assert.AreEqual(wasy, sender[0]);
            Assert.AreEqual(CommandWaysOperatorTypes.Remove, args[0].Type);
            Assert.AreEqual(0, args[0].Items.Single());
            Assert.AreEqual(wasy, sender[1]);
            Assert.AreEqual(CommandWaysOperatorTypes.Add, args[1].Type);
            Assert.AreEqual(-1, args[1].Items.Single());

            Assert.AreEqual(size, wasy.Count);
            Assert.AreEqual(-1, wasy.First);
            Assert.AreEqual(1, wasy.Last);
        }
        [TestMethod]
        public void ResizeWhenStoreBound_MustRemoveRange()
        {
            CommandWays<int> wasy = new CommandWays<int>
            {
                MaxSize = 10
            };
            for (int i = 0; i < wasy.MaxSize; i++)
            {
                wasy.Push(i);
            }
            object sender = null;
            CommandWaysOperatorEventArgs<int> args = null;
            wasy.WayChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };
            wasy.MaxSize = 5;
            Assert.AreEqual(wasy, sender);
            Assert.AreEqual(CommandWaysOperatorTypes.Remove, args.Type);
            Assert.AreEqual(5, args.Items.Count());
        }
        [TestMethod]
        public void PushRange_Empty_WasDoNothing()
        {
            var ways = new CommandWays<int>();

            ways.PushRange(Enumerable.Empty<int>());

            Assert.AreEqual(0, ways.Count);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 2, 3, 4 }, 5)]
        [DataRow(new int[] { 1 }, 1)]
        [DataRow(new int[] { 1, 2 }, 100)]
        [DataRow(new int[] { 1, 2, 3, 4, 1, 2, 5, 6, 8, 9 }, 20)]
        [DataRow(new int[] { 1, 2, 3, 4 }, 4)]
        [DataRow(new int[] { -1, -3 }, 2)]
        [DataRow(new int[] { 0, 0, 0 }, 4)]
        public void PushRange_AtLimited_NothingRemoved(int[] datas, int count)
        {
            var ways = new CommandWays<int>();
            ways.MaxSize = count;

            ways.PushRange(datas);

            Assert.AreEqual(datas.Length, ways.Count);

            var res = ways.Reverse().ToList();
            Assert.AreEqual(datas.Length, res.Count);
            for (int i = 0; i < res.Count; i++)
            {
                Assert.AreEqual(datas[i], res[i], $"{datas[i]} is not equals");
            }
        }
        [TestMethod]
        [DataRow(new int[] { 1, 2, 3, 4 }, 3)]
        [DataRow(new int[] { 1, 2, 3, 4 }, 1)]
        [DataRow(new int[] { 1, 2, 3, 4 }, 0)]
        [DataRow(new int[] { 1 }, 0)]
        [DataRow(new int[] { 1, 2 }, 1)]
        public void PushRange_OutOfLimited_WasRemoveHeads(int[] datas, int count)
        {
            var ways = new CommandWays<int>();
            ways.MaxSize = count;
            ways.PushRange(datas);

            var usingDatas = datas.Reverse().Skip(datas.Length-count).ToList();

            var res = ways.ToList();
            Assert.AreEqual(usingDatas.Count, res.Count);
            for (int i = 0; i < res.Count; i++)
            {
                Assert.AreEqual(usingDatas[i], res[i], $"{datas[i]} is not equals");
            }
        }
        [TestMethod]
        [DataRow(new int[] { -1,-2,-3,-4 }, 1)]
        [DataRow(new int[] { }, 1)]
        [DataRow(new int[] { 11 }, 1)]
        [DataRow(new int[] { 12,314,111}, 10)]
        [DataRow(new int[] { 1 }, 20)]
        public void PushRange_OriginHasLimited_WasRemoveOrigins(int[] datas,int outterCount)
        {
            var ways = new CommandWays<int>();
            ways.MaxSize = datas.Length;
            for (int i = 0; i < outterCount; i++)
            {
                ways.Push(i);
            }
            ways.PushRange(datas);

            var res = ways.Reverse().ToList();
            Assert.AreEqual(datas.Length, res.Count);

            for (int i = 0; i < datas.Length; i++)
            {
                Assert.AreEqual(datas[i], res[i], $"{datas[i]} is not equals");
            }
        }
        [TestMethod]
        public void Contains()
        {
            var ways = new CommandWays<int>();
            Assert.IsFalse(ways.Contains(-1));
            ways.Push(1);
            ways.Push(2);
            ways.Push(3);

            Assert.IsFalse(ways.Contains(-1));
            Assert.IsFalse(ways.Contains(99));
            Assert.IsTrue(ways.Contains(1));
            Assert.IsTrue(ways.Contains(2));
            Assert.IsTrue(ways.Contains(3));
        }
        [TestMethod]
        public void CopyTo()
        {
            var ways = new CommandWays<int>();
            ways.Push(1);
            ways.Push(2);
            ways.Push(3);
            var arr = new int[3];

            ways.CopyTo(arr, 0);

            Assert.AreEqual(1, arr[2]);
            Assert.AreEqual(2, arr[1]);
            Assert.AreEqual(3, arr[0]);
        }
        [TestMethod]
        public void PopWidthEmpty()
        {
            var ways = new CommandWays<int>();
            Assert.AreEqual(0, ways.Pop());

            var ways1 = new CommandWays<object>();
            Assert.IsNull(ways1.Pop());
        }
        [TestMethod]
        public void EmptyCalling()
        {
            var ways = new CommandWays<int>();
            Assert.AreEqual(0, ways.First);
            Assert.AreEqual(0, ways.Last);

            var ways1 = new CommandWays<object>();
            Assert.IsNull(ways1.First);
            Assert.IsNull(ways1.Last);
        }
        [TestMethod]
        public void Peek()
        {
            var ways = new CommandWays<int>();
            Assert.AreEqual(0, ways.Peek());
            ways.Push(1);
            Assert.AreEqual(1, ways.Peek());
            ways.Push(2);
            Assert.AreEqual(2, ways.Peek());

            var ways1 = new CommandWays<object>();
            Assert.IsNull(ways1.Peek());
            var obj = new object();
            ways1.Push(obj);
            Assert.AreEqual(obj, ways1.Peek());
            obj = new object();
            ways1.Push(obj);
            Assert.AreEqual(obj, ways1.Peek());
        }
        [TestMethod]
        public void Clear()
        {
            var ways = new CommandWays<int>();
            ways.Clear(true);
            Assert.AreEqual(0, ways.Count);


            ways = new CommandWays<int>();
            ways.Push(1);
            ways.Push(1);
            ways.Push(1);
            ways.Push(1);
            ways.Push(1);
            ways.Push(1);
            ways.Push(1);
            ways.Push(1);
            ways.Clear(true);
            Assert.AreEqual(0, ways.Count);
        }
    }
}
