using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using System.Reflection.Emit;
using System.Reflection;
using FastExpressionCompiler;

namespace Ao.ObjectDesign.Benchmark
{
    [MemoryDiagnoser]
    public class ExpressionAndDynamic
    {
        private delegate object ObjectCreator();
        private ObjectCreator exp;
        private ObjectCreator dync;
        private ObjectCreator fastExp;
        private ConstructorInfo info;
        public class Student
        {

        }

        public ExpressionAndDynamic()
        {
            info = typeof(Student).GetConstructor(Type.EmptyTypes);
            exp = GenExpression();
            dync = GenDyncmic();
            fastExp = GenFastExpression();

            _ = exp();
            _ = dync();
            _= fastExp();
        }
        [Benchmark]
        public void GenExp()
        {
            _ = GenExpression();
        }
        [Benchmark]
        public void GenDync()
        {
            _ = GenDyncmic();
        }
        [Benchmark]
        public void GenFast()
        {
            _ = GenFastExpression();
        }
        [Benchmark]
        public void NewByExp()
        {
            _ = exp();
        }
        [Benchmark]
        public void NewByDync()
        {
            _ = dync();
        }
        [Benchmark]
        public void NewByFastExp()
        {
            _ = dync();
        }

        private ObjectCreator GenFastExpression() 
        {
            return Expression.Lambda<ObjectCreator>(Expression.Convert(Expression.New(info), typeof(object))).CompileFast();
        }

        private ObjectCreator GenDyncmic()
        {
            var dn = new DynamicMethod("create_student", typeof(object), Type.EmptyTypes,true);
            var il = dn.GetILGenerator();
            il.Emit(OpCodes.Newobj, info);
            il.Emit(OpCodes.Ret);
            return (ObjectCreator)dn.CreateDelegate(typeof(ObjectCreator));
        }
        private ObjectCreator GenExpression()
        {
            return Expression.Lambda<ObjectCreator>(Expression.Convert(Expression.New(info), typeof(object))).Compile();
        }
    }
}
