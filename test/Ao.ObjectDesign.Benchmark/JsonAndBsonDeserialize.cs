using Ao.ObjectDesign.Store;
using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Wpf.Json;
using BenchmarkDotNet.Attributes;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class JsonAndBsonDeserialize
    {
        private readonly ButtonSetting buttonSetting;
        private readonly string json;
        private readonly byte[] bson;

        public JsonAndBsonDeserialize()
        {
            buttonSetting = new ButtonSetting();
            json = JsonDesignInterop.Default.SerializeToString(buttonSetting);
            bson = BsonDesignInterop.Default.SerializeToByte(buttonSetting);
        }
        [Benchmark(Baseline = true)]
        public void JsonSerialize()
        {
            JsonDesignInterop.Default.DeserializeByString<ButtonSetting>(json);
        }
        [Benchmark]
        public void BsonSerialize()
        {
            BsonDesignInterop.Default.DeserializeByByte<ButtonSetting>(bson);
        }
    }
}
