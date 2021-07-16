using BenchmarkDotNet.Attributes;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class GetSet
    {
        private readonly PropertyVisitor visitor;
        private readonly CompiledPropertyVisitor compiledVisitor;

        public GetSet()
        {
            Student inst = new Student { Name = "hewahdoas" };
            System.Reflection.PropertyInfo prop = inst.GetType().GetProperty(nameof(Student.Name));
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
