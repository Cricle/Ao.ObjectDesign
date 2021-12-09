using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace Ao.ObjectDesign.Benchmark
{
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
            PropertyVisitor v = new PropertyVisitor(student, prop);
            _ = v.Value;
        }
        [Benchmark]
        public void CompiledFastExpression()
        {
            ExpressionPropertyVisitor v = new ExpressionPropertyVisitor(student, prop);
            _ = v.Value;
        }
    }
}
