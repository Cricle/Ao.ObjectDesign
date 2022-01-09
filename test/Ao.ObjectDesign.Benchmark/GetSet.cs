using BenchmarkDotNet.Attributes;
using FastExpressionCompiler;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class GetSet
    {
        const int operators = 1000;

        private readonly PropertyVisitor visitor;
        private readonly ExpressionPropertyVisitor expressionCompiledVisitor;
        private readonly Student student;
        private readonly Func<Student, string> getter;
        private readonly Action<Student, string> setter;

        private readonly PropertySetter dynSet;
        private readonly PropertyGetter dynGet;

        private readonly PropertySetter fastSet;
        private readonly PropertyGetter fastGet;

        public GetSet()
        {
            student = new Student { Name = "hewahdoas" };
            PropertyInfo prop = student.GetType().GetProperty(nameof(Student.Name));
            getter = (Func<Student, string>)Delegate.CreateDelegate(typeof(Func<Student, string>), prop.GetMethod);
            setter = (Action<Student, string>)Delegate.CreateDelegate(typeof(Action<Student, string>), prop.SetMethod);
            visitor = new PropertyVisitor(student, prop);
            expressionCompiledVisitor = new ExpressionPropertyVisitor(student, prop);
            var identity = new PropertyIdentity(typeof(Student), "Name");
            dynGet = CompiledPropertyInfo.GetGetter(identity);
            dynSet = CompiledPropertyInfo.GetSetter(identity);

            fastGet = GetPropertyGetter(identity);
            fastSet = GetPropertySetter(identity);

            dynGet(student);
            dynSet(student, "aa");

            fastGet(student);
            fastSet(student, "aa");

            _ = visitor.Value;
            _ = expressionCompiledVisitor.Value;
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
        protected PropertyGetter GetPropertyGetter(in PropertyIdentity identity)
        {
            var par1 = Expression.Parameter(typeof(object));

            var exp = Expression.Convert(
                Expression.Call(
                    Expression.Convert(par1, identity.Type), identity.PropertyInfo.GetMethod), typeof(object));

            return Expression.Lambda<PropertyGetter>(exp, par1).CompileFast();
        }

        protected PropertySetter GetPropertySetter(in PropertyIdentity identity)
        {
            var par1 = Expression.Parameter(typeof(object));
            var par2 = Expression.Parameter(typeof(object));

            var exp = Expression.Call(
                        Expression.Convert(par1, identity.Type), identity.PropertyInfo.SetMethod,
                            Expression.Convert(par2, identity.PropertyInfo.PropertyType));

            return Expression.Lambda<PropertySetter>(exp, par1, par2).CompileFast();
        }

        [Benchmark(OperationsPerInvoke = operators)]
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
        public void GetExpressionCompiled()
        {
            for (int i = 0; i < operators; i++)
            {

                _ = expressionCompiledVisitor.Value;
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetExpressionCompiled()
        {
            for (int i = 0; i < operators; i++)
            {

                expressionCompiledVisitor.SetValue("asd");
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

        [Benchmark(OperationsPerInvoke = operators)]
        public void GetCompiledDirect()
        {
            for (int i = 0; i < operators; i++)
            {
                dynGet(student);
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetCompiledDirect()
        {
            for (int i = 0; i < operators; i++)
            {
                dynSet(student, "asd");
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void GetFastExpCompiledDirect()
        {
            for (int i = 0; i < operators; i++)
            {
                fastGet(student);
            }
        }
        [Benchmark(OperationsPerInvoke = operators)]
        public void SetFastExpCompiledDirect()
        {
            for (int i = 0; i < operators; i++)
            {
                fastSet(student, "asd");
            }
        }
    }
}
