using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var wasy = new CommandWays<int>();
            wasy.MaxSize = 10;
            for (int i = 0; i < count; i++)
            {
                wasy.Push(i);
            }
            Assert.AreEqual(count, wasy.Count);
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, wasy.Pop());
            }
        }
        [TestMethod]
        public void GivenZeroMaxSize_AddOne_MustNotStore()
        {
            var wasy = new CommandWays<int>();
            wasy.MaxSize = 0;

            var idx = 0;
            var sender = new object[2];
            var args = new CommandWaysOperatorEventArgs<int>[2];
            wasy.WayChanged += (o, e) =>
            {
                sender[idx] = o;
                args[idx] = e;
                idx++;
            };
            wasy.Push(1);

            Assert.AreEqual(wasy, sender[0]);
            Assert.AreEqual(CommandWaysOperatorTypes.Add, args[0].Type);
            Assert.AreEqual(1, args[0].Items.Single());

            Assert.AreEqual(wasy, sender[1]);
            Assert.AreEqual(CommandWaysOperatorTypes.Remove, args[1].Type);
            Assert.AreEqual(1, args[1].Items.Single());

            Assert.AreEqual(0, wasy.Count);
        }
        [TestMethod]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(30)]
        public void GivenAnyValueBound_AddOne_MustResize(int size)
        {
            var wasy = new CommandWays<int>();
            wasy.MaxSize = size;
            for (int i = 0; i < size; i++)
            {
                wasy.Push(i);
            }
            var idx = 0;
            var sender = new object[2];
            var args = new CommandWaysOperatorEventArgs<int>[2];
            wasy.WayChanged += (o, e) =>
            {
                sender[idx] = o;
                args[idx] = e;
                idx++;
            };
            wasy.Push(-1);
            Assert.AreEqual(wasy, sender[0]);
            Assert.AreEqual(CommandWaysOperatorTypes.Add, args[0].Type);
            Assert.AreEqual(-1, args[0].Items.Single());
            Assert.AreEqual(wasy, sender[1]);
            Assert.AreEqual(CommandWaysOperatorTypes.Remove, args[1].Type);
            Assert.AreEqual(0, args[1].Items.Single());

            Assert.AreEqual(size, wasy.Count);
            Assert.AreEqual(1, wasy.First);
            Assert.AreEqual(-1, wasy.Last);
        }
        [TestMethod]
        public void ResizeWhenStoreBound_MustRemoveRange()
        {
            var wasy = new CommandWays<int>();
            wasy.MaxSize = 10;
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
