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
    [MemoryDiagnoser]
    public class Compile
    {
        private readonly Student student;
        private readonly PropertyInfo prop;
        public Compile()
        {
            student = new Student { Name = "hewahdoas" };
            prop = student.GetType().GetProperty(nameof(Student.Name));
        }
        [Benchmark(Baseline = true)]
        public void Normal()
        {
            var v = new PropertyVisitor(student, prop);
            _ = v.Value;
        }
        [Benchmark]
        public void Compiled()
        {
            var v = new CompiledPropertyVisitor(student, prop);
            _ = v.Value;
        }
    }
    [MemoryDiagnoser]
    public class GetSet
    {
        private readonly PropertyVisitor visitor;
        private readonly CompiledPropertyVisitor compiledVisitor;

        public GetSet()
        {
            var inst = new Student { Name = "hewahdoas" };
            var prop = inst.GetType().GetProperty(nameof(Student.Name));
            visitor = new PropertyVisitor(inst, prop);
            compiledVisitor = new CompiledPropertyVisitor(inst, prop);

            _ = visitor.Value;
            _ = compiledVisitor.Value;
        }

        [Benchmark(Baseline = true)]
        public void GetSetNormal()
        {
            _ = visitor.Value;
            visitor.SetValue("asd");
        }
        [Benchmark]
        public void GetSetCompiled()
        {
            _ = compiledVisitor.Value;
            compiledVisitor.SetValue("asd");
        }
    }
}
