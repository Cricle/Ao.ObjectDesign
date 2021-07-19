using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Wpf.Json;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Bson;
using System.IO;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class JsonAndBsonSerialize
    {
        private readonly ButtonSetting buttonSetting;
        private MemoryStream stream;

        public JsonAndBsonSerialize()
        {
            buttonSetting = new ButtonSetting();
            stream = new MemoryStream(1024 * 1024 * 1);
        }
        [Benchmark(Baseline =true)]
        public void JsonSerialize()
        {
            DesignJsonHelper.Serialize(buttonSetting);
        }
        [Benchmark]
        public void BsonSerialize()
        {
            stream.Seek(0, SeekOrigin.Begin);
            DesignBsonHelper.Serialize(new BsonDataWriter(stream));
        }
    }
}
