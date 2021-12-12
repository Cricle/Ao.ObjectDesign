using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Ao.ObjectDesign.Wpf.Json;
using Ao.ObjectDesign.Wpf.Xaml;
using Ao.ObjectDesign.Wpf.Yaml;
using Ao.ObjectDesign.Wpf.MessagePack;
using System.IO;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class Deserialize
    {
        private readonly List<NotifyableObject> brushes;
        private string json;
        private Stream bson;
        private string yaml;
        private byte[] mp;
        public Deserialize()
        {
            brushes = SerializeHelper.MakeDatas(1000);
            json = DesignJsonHelper.Serialize(brushes);
            bson = DesignBsonHelper.Serialize(brushes);
            yaml = DeisgnYamlSerializer.Serialize(brushes);
            mp = DesignMessagePackHelper.Serialize(brushes.GetType(), brushes);
        }
        [Benchmark]
        public void Json()
        {
            DesignJsonHelper.Deserialize<List<NotifyableObject>>(json);
        }
        [Benchmark]
        public void Bson()
        {
            bson.Seek(0, SeekOrigin.Begin);
            DesignBsonHelper.Serialize(brushes);
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
            DesignMessagePackHelper.Deserialize(brushes.GetType(), mp);
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
            DesignJsonHelper.Serialize(brushes);
        }
        [Benchmark]
        public void Bson()
        {
            DesignBsonHelper.Serialize(brushes);
        }
        //[Benchmark]
        //public void Xml()
        //{
        //    DeisgnXmlSerializer.Serialize(brushes);
        //}
        [Benchmark]
        public void Yaml()
        {
            DeisgnYamlSerializer.Serialize(brushes);
        }
        [Benchmark]
        public void MessagePack()
        {
            DesignMessagePackHelper.Serialize(brushes.GetType(),brushes);
        }
    }
}
