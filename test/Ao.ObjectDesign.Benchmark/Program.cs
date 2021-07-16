using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Wpf;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

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
            ReflectionHelper.CloneIgnoreDesigners(setting);
        }
    }
    class Student
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
