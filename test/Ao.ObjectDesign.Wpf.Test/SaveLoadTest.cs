using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Json;
using Ao.ObjectDesign.Wpf.Xaml;
using Ao.ObjectDesign.Wpf.Yaml;
using System.IO;
using Ao.ObjectDesign.Abstract.Store;
#if !NET452
using Ao.ObjectDesign.Wpf.MessagePack;
using Ao.ObjectDesign.Wpf.TextJson;
#endif

namespace Ao.ObjectDesign.Wpf.Test
{
    public class Box
    {
        public List<NotifyableObject> Objs { get; set; }
    }
    [TestClass]
    public class SaveLoadTest
    {
        [TestMethod]
        public void SaveAndLoad()
        {
            var lst = new Box { Objs = new List<NotifyableObject>() };
            var btn = new ButtonSetting();
            btn.SetDefault();
            lst.Objs.Add(btn);
            var tbx = new TextBlockSetting();
            tbx.SetDefault();
            lst.Objs.Add(tbx);

            var s1 = JsonDesignInterop.Default.SerializeToString(lst);
            var o1 = JsonDesignInterop.Default.DeserializeByString<Box>(s1);

            var s2 = BsonDesignInterop.Default.SerializeToByte(lst);
            var o2 = BsonDesignInterop.Default.DeserializeByByte<Box>(s2);
#if !NET452
            var mem = new MemoryStream();
            MessagePackDesignInterop.Default.SerializeToStream(lst, mem);
            mem.Seek(0, SeekOrigin.Begin);
            var o3 = MessagePackDesignInterop.Default.DeserializeByStream<Box>(mem);
            mem.Dispose();

            var s3x = MessagePackDesignInterop.Default.SerializeToByte(lst);
            var o3x = MessagePackDesignInterop.Default.DeserializeByByte<Box>(s3x);


            //var s4 = TextJsonDesignInterop.Default.SerializeToString(lst);
            //var o4 = TextJsonDesignInterop.Default.DeserializeByString<Box>(s4);

#endif
            var s5 = XamlDesignInterop.Default.SerializeToString(lst);
            var o5 = XamlDesignInterop.Default.DeserializeByString<Box>(s5);

            var s6 = YamlDesignInterop.Default.SerializeToString(lst);
            var o6 = YamlDesignInterop.Default.DeserializeByString<Box>(s6);


        }
    }
    
}
