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
    class Student
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
