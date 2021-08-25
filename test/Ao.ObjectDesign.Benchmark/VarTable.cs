using Ao.ObjectDesign.Data;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class VarTable
    {
        class SafeNotifyableMap : NotifyableMap<string, string>
        {
            protected override IDictionary<string, string> CreateMap()
            {
                return new ConcurrentDictionary<string, string>();
            }
        }
        private readonly NotifyableMap<string, string> map;
        private readonly SafeNotifyableMap smap;
        private readonly Dictionary<string, string> dic;
        public VarTable()
        {
            map = new NotifyableMap<string, string>();
            dic = new Dictionary<string, string>();
            smap = new SafeNotifyableMap();
        }
        [Benchmark(Baseline = true)]
        public void AddOrUpdate()
        {
            if (dic.ContainsKey("wdaw"))
            {
                dic["wdaw"] = "weawdasda";
            }
            else
            {
                dic.Add("wdaw", "weawdasda");
            }
        }
        [Benchmark]
        public void AddOrUpdateTs()
        {
            map.AddOrUpdate("wdaw", "weawdasda");
        }
        [Benchmark]
        public void SafeAddOrUpdateTs()
        {
            smap.AddOrUpdate("wdaw", "weawdasda");
        }
    }
}
