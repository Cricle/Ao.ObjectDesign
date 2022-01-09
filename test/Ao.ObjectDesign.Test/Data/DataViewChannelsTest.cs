using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DataViewChannelsTest
    {
        class NullDataNotifyer : IDataNotifyer<string>
        {
            public void OnDataChanged(object sender, DataChangedEventArgs<string, IVarValue> e)
            {
            }
        }
        class ValueDataNotifyer : IDataNotifyer<string>
        {
            public object Sender { get; set; }

            public DataChangedEventArgs<string, IVarValue> Args { get; set; }

            public void OnDataChanged(object sender, DataChangedEventArgs<string, IVarValue> e)
            {
                Sender = sender;
                Args = e;
            }
        }
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            var dv = new DataView<string>();
            var channel = new DataViewChannels<string>(dv);
            var notifyer = new NullDataNotifyer();
            var name = "duadghsa";

            Assert.ThrowsException<ArgumentNullException>(() => new DataViewChannels<string>(null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.GetSubscribeCount(null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.Regist(null, notifyer));
            Assert.ThrowsException<ArgumentNullException>(() => channel.Regist(name, null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.UnRegist(name, null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.UnRegist(null, notifyer));
            Assert.ThrowsException<ArgumentNullException>(() => channel.IsSubscribedName(null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.IsSubscribedNotifyer(null, notifyer));
            Assert.ThrowsException<ArgumentNullException>(() => channel.IsSubscribedNotifyer(name, null));
        }
        [TestMethod]
        public void IsSubscribe()
        {
            var dv = new DataView<string>();
            var channel = new DataViewChannels<string>(dv);
            var notifyer = new NullDataNotifyer();
            var name = "duadghsa";

            Assert.IsFalse(channel.IsSubscribedName(name));
            Assert.IsFalse(channel.IsSubscribedNotifyer(name, notifyer));

            channel.Regist(name, notifyer);
            Assert.IsTrue(channel.IsSubscribedName(name));
            Assert.IsTrue(channel.IsSubscribedNotifyer(name, notifyer));

            var res = channel.UnRegist(name, notifyer);
            Assert.IsTrue(res);
            Assert.IsFalse(channel.IsSubscribedName(name));
            Assert.IsFalse(channel.IsSubscribedNotifyer(name, notifyer));
            res = channel.UnRegist(name, notifyer);
            Assert.IsFalse(res);
        }
        [TestMethod]
        public void Regist_UnRegist_Notify()
        {
            var dv = new DataView<string>();
            var channel = new DataViewChannels<string>(dv);
            var notifyer = new ValueDataNotifyer();
            var name = "duadghsa";

            var res = channel.Regist(name, notifyer);
            Assert.IsNotNull(res);
            var val = VarValue.FalseValue;
            dv.AddOrUpdate(name, val);
            Assert.AreEqual(dv, notifyer.Sender);
            Assert.IsNotNull(notifyer.Args);

            channel.UnRegist(name, notifyer);
            notifyer.Args = null;
            notifyer.Sender = null;
            dv.AddOrUpdate(name, VarValue.Decimal0Value);

            Assert.IsNull(notifyer.Args);
            Assert.IsNull(notifyer.Sender);
        }
        [TestMethod]
        public void GetSubscribeCount()
        {
            var dv = new DataView<string>();
            var channel = new DataViewChannels<string>(dv);
            var name = "duadghsa";
            var val = channel.GetSubscribeCount(name);
            Assert.IsNull(val);
            var notifyer = new ValueDataNotifyer();

            channel.Regist(name, notifyer);

            Assert.AreEqual(name, channel.SubscribeNames.Single());
            var m = channel.NotifyerMap;
            Assert.AreEqual(1, m.Count);
            Assert.AreEqual(name, m.Keys.Single());

            val = channel.GetSubscribeCount(name);
            Assert.AreEqual(1, val.Value);
        }
        [TestMethod]
        public void Subscribe_Raise()
        {
            var dv = new DataView<string>();
            var channel = new DataViewChannels<string>(dv);
            var name = "duadghsa";
            var notifyer = new ValueDataNotifyer();
            var token = channel.Regist(name, notifyer);
            Assert.AreEqual(name, token.Key);
            Assert.AreEqual(notifyer, token.Notifyer);
            Assert.IsTrue(token.IsSubscribed);
            Assert.AreEqual(channel, token.Channel);

            object sender = null;
            NotifyUnSubscribedEventArgs<string> args = null;

            token.UnSubscribed += (o, e) =>
            {
                sender = o;
                args = e;
            };
            var value = VarValue.Long0Value;
            dv.AddOrUpdate(name, value);

            Assert.AreEqual(dv, notifyer.Sender);
            Assert.AreEqual(name, notifyer.Args.Key);
            Assert.AreEqual(ChangeModes.New, notifyer.Args.Mode);
            Assert.IsNull(notifyer.Args.Old);
            Assert.AreEqual(value, notifyer.Args.New);

            var value1 = new StructValue(123);

            dv.AddOrUpdate(name, value1);

            Assert.AreEqual(dv, notifyer.Sender);
            Assert.AreEqual(name, notifyer.Args.Key);
            Assert.AreEqual(ChangeModes.Change, notifyer.Args.Mode);
            Assert.AreEqual(value, notifyer.Args.Old);
            Assert.AreEqual(value1, notifyer.Args.New);

            token.Dispose();

            Assert.AreEqual(sender, token);
            Assert.AreEqual(name, args.Key);
            Assert.AreEqual(notifyer, args.Notifyer);
            Assert.AreEqual(channel, args.Channels);
            Assert.IsFalse(token.IsSubscribed);
        }
    }
}
