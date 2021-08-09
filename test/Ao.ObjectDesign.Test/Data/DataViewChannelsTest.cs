using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DataViewChannelsTest
    {
        class NullDataNotifyer : IDataNotifyer<string>
        {
            public void OnDataChanged(object sender, DataChangedEventArgs<string, VarValue> e)
            {
            }
        }
        class ValueDataNotifyer:IDataNotifyer<string>
        {
            public object Sender { get; set; }

            public DataChangedEventArgs<string, VarValue> Args { get; set; }

            public void OnDataChanged(object sender, DataChangedEventArgs<string, VarValue> e)
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
            Assert.ThrowsException<ArgumentNullException>(() => channel.Regist(null,notifyer));
            Assert.ThrowsException<ArgumentNullException>(() => channel.Regist(name, null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.UnRegist(name, null));
            Assert.ThrowsException<ArgumentNullException>(() => channel.Regist(null, notifyer));
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
            Assert.IsFalse(channel.IsSubscribedNotifyer(name,notifyer));

            channel.Regist(name, notifyer);
            Assert.IsTrue(channel.IsSubscribedName(name));
            Assert.IsTrue(channel.IsSubscribedNotifyer(name, notifyer));

            channel.UnRegist(name, notifyer);
            Assert.IsFalse(channel.IsSubscribedName(name));
            Assert.IsFalse(channel.IsSubscribedNotifyer(name, notifyer));
        }
        [TestMethod]
        public void Regist_UnRegist_Notify()
        {
            var dv = new DataView<string>();
            var channel = new DataViewChannels<string>(dv);
            var notifyer = new ValueDataNotifyer();
            var name = "duadghsa";

            channel.Regist(name, notifyer);
            var val = VarValue.FalseValue;
            dv.AddOrUpdate(name,val);
            Assert.AreEqual(dv, notifyer.Sender);
            Assert.IsNotNull(notifyer.Args);

            channel.UnRegist(name, notifyer);
            notifyer.Args = null;
            notifyer.Sender = null;
            dv.AddOrUpdate(name, VarValue.Decimal0Value);

            Assert.IsNull(notifyer.Args);
            Assert.IsNull(notifyer.Sender);
        }
    }
}
