using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class GetSet
    {
        private readonly PropertyVisitor visitor;
        private readonly CompiledPropertyVisitor compiledVisitor;
        private readonly Student student;

        public GetSet()
        {
            student= new Student { Name = "hewahdoas" };
            PropertyInfo prop = student.GetType().GetProperty(nameof(Student.Name));
            visitor = new PropertyVisitor(student, prop);
            compiledVisitor = new CompiledPropertyVisitor(student, prop);

            _ = visitor.Value;
            _ = compiledVisitor.Value;
        }
        [Benchmark(Baseline =true)]
        public void GetSetNormal()
        {
            _ = student.Name;
            student.Name = "asd";
        }
        [Benchmark]
        public void GetSetReflection()
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
