using BenchmarkDotNet.Attributes;
using System;
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
        private readonly Func<Student, string> getter;
        private readonly Action<Student, string> setter;

        public GetSet()
        {
            student= new Student { Name = "hewahdoas" };
            PropertyInfo prop = student.GetType().GetProperty(nameof(Student.Name));
            getter = (Func<Student, string>)Delegate.CreateDelegate(typeof(Func<Student,string>), prop.GetMethod);
            setter = (Action<Student, string>)Delegate.CreateDelegate(typeof(Action<Student, string>), prop.SetMethod);
            visitor = new PropertyVisitor(student, prop);
            compiledVisitor = new CompiledPropertyVisitor(student, prop);

            _ = visitor.Value;
            _ = compiledVisitor.Value;
            SetCompiledDirect();
            GetCompiledDirect();
            GetCompiledDirect();
            GetDelegate();
            SetDelegate();
            GetReflection();
            SetReflection();
            GetNormal();
            SetNormal();
        }
        [Benchmark(OperationsPerInvoke =operators)]
        public void GetNormal()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = student.Name;
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetNormal()
        {
            for (int i = 0; i < operators; i++)
            {

                student.Name = "asd";
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void GetReflection()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = visitor.Value;
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetReflection()
        {
            for (int i = 0; i < operators; i++)
            {

                visitor.SetValue("asd");
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void GetCompiled()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = compiledVisitor.Value;
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetCompiled()
        {
            for (int i = 0; i < operators; i++)
            {

                compiledVisitor.SetValue("asd");
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void GetDelegate()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = getter(student);
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetDelegate()
        {
            for (int i = 0; i < operators; i++)
            {

                setter(student, "asd");
            }
        }

        [Benchmark(OperationsPerInvoke =operators)]
        public void GetCompiledDirect()
        {
            var identity = new PropertyIdentity(typeof(Student), "Name");
            var pg = CompiledPropertyInfo.GetGetter(identity);
            for (int i = 0; i < operators; i++)
            {
                pg(student);
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetCompiledDirect()
        {
            var identity = new PropertyIdentity(typeof(Student), "Name");
            var ps = CompiledPropertyInfo.GetSetter(identity);
            for (int i = 0; i < operators; i++)
            {
                ps(student, "asd");
            }
        }
    }
}
