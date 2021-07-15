using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Wpf;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Benchmarks().GetSetCompiled();
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
        }
    }
    [MemoryDiagnoser]
    public class Clone
    {
        private readonly ButtonSetting setting = new ButtonSetting();
        public Clone()
        {
            CloneSettings();
        }
        [Benchmark]
        public void CloneSettings()
        {
            CloneHelper.CloneIgnoreDesigners(setting);
        }
    }
    class Student
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
