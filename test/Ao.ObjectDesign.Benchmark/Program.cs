using Ao.ObjectDesign.Controls;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Ao.ObjectDesign.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ExpressionAndDynamic().NewByDync();
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
            ReflectionHelper.Clone(setting);
        }
    }
    class Student
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
