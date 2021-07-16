using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ao.ObjectDesign.Wpf.Test
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
    }
}
