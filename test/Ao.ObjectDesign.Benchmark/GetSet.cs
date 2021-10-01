using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class GetSet
    {
        const int operators = 1000;

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
            GetSetCompiledDirect();
        }
        [Benchmark(Baseline =true,OperationsPerInvoke =operators)]
        public void GetSetNormal()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = student.Name;
                student.Name = "asd";
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void GetSetReflection()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = visitor.Value;
                visitor.SetValue("asd");
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void GetSetCompiled()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = compiledVisitor.Value;
                compiledVisitor.SetValue("asd");
            }
        }
        [Benchmark(OperationsPerInvoke =operators)]
        public void GetSetCompiledDirect()
        {
            var identity = new PropertyIdentity(typeof(Student), "Name");
            for (int i = 0; i < operators; i++)
            {
                CompiledPropertyInfo.GetGetter(identity)(student);
                CompiledPropertyInfo.GetSetter(identity)(student, "asd");
            }
        }
    }
}
