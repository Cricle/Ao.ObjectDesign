using Ao.ObjectDesign.Abstract.Store;
using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Wpf.Json;
using BenchmarkDotNet.Attributes;
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
        [Benchmark(Baseline = true)]
        public void JsonSerialize()
        {
            JsonDesignInterop.Default.SerializeToString(buttonSetting);
        }
        [Benchmark]
        public void BsonSerialize()
        {
            BsonDesignInterop.Default.SerializeToByte(buttonSetting);
        }
    }
}
