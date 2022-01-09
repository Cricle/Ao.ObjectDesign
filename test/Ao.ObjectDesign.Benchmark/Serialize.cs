using Ao.ObjectDesign.Abstract.Store;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Json;
using Ao.ObjectDesign.Wpf.MessagePack;
using Ao.ObjectDesign.Wpf.Yaml;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class Deserialize
    {
        private readonly List<NotifyableObject> brushes;
        private string json;
        private byte[] bson;
        private string yaml;
        private byte[] mp;
        public Deserialize()
        {
            brushes = SerializeHelper.MakeDatas(1000);
            json = JsonDesignInterop.Default.SerializeToString(brushes);
            bson = BsonDesignInterop.Default.SerializeToByte(brushes);
            yaml = YamlDesignInterop.Default.SerializeToString(brushes);
            mp = MessagePackDesignInterop.Default.SerializeToByte(brushes);
        }
        [Benchmark]
        public void Json()
        {
            JsonDesignInterop.Default.SerializeToString(json);
        }
        [Benchmark]
        public void Bson()
        {
            BsonDesignInterop.Default.SerializeToByte(brushes);
        }
        //[Benchmark]
        //public void Xml()
        //{
        //    DeisgnXmlSerializer.Serialize(brushes);
        //}
        //[Benchmark]
        //public void Yaml()
        //{
        //    DeisgnYamlSerializer.Deserializer(yaml,brushes.GetType());
        //}
        [Benchmark]
        public void MessagePack()
        {
            YamlDesignInterop.Default.SerializeToString(brushes);
        }
    }

    [MemoryDiagnoser]
    public class Serialize
    {
        private readonly List<NotifyableObject> brushes;

        public Serialize()
        {
            brushes = SerializeHelper.MakeDatas(1000);
        }
        [Benchmark]
        public void Json()
        {
            JsonDesignInterop.Default.SerializeToString(brushes);
        }
        [Benchmark]
        public void Bson()
        {
            BsonDesignInterop.Default.SerializeToByte(brushes);
        }
        //[Benchmark]
        //public void Xml()
        //{
        //    DeisgnXmlSerializer.Serialize(brushes);
        //}
        [Benchmark]
        public void Yaml()
        {
            YamlDesignInterop.Default.SerializeToString(brushes);
        }
        [Benchmark]
        public void MessagePack()
        {
            MessagePackDesignInterop.Default.SerializeToByte(brushes);
        }
    }
}
