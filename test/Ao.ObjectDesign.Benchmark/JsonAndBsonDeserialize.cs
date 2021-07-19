using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Wpf.Json;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class JsonAndBsonDeserialize
    {
        private readonly ButtonSetting buttonSetting;
        private readonly string json;
        private readonly Stream bson;

        public JsonAndBsonDeserialize()
        {
            buttonSetting = new ButtonSetting();
            json= DesignJsonHelper.Serialize(buttonSetting);
            bson = DesignBsonHelper.Serialize(buttonSetting);
        }
        [Benchmark(Baseline = true)]
        public void JsonSerialize()
        {
            DesignJsonHelper.Deserialize<ButtonSetting>(json);
        }
        [Benchmark]
        public void BsonSerialize()
        {
            bson.Seek(0, SeekOrigin.Begin);
            DesignBsonHelper.Deserialize<ButtonSetting>(bson);
        }
    }
}
