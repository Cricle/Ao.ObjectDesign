using FastExpressionCompiler;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    internal static class VirtualPropertyCompiler
    {
        public static PropertyGetter BuildGetter(MethodInfo info)
        {
            if (info.GetParameters().Length != 0||info.ReturnType==typeof(void))
            {
                throw new ArgumentException($"Virtual get property method {info} must be 0 paramters and no return void");
            }

            var par1 = Expression.Parameter(typeof(object));

            var body = Expression.Convert(Expression.Call(Expression.Convert(par1, info.DeclaringType), info),typeof(object));

            return Expression.Lambda<PropertyGetter>(body, par1).CompileSys();
        }
        public static PropertySetter BuildSetter(MethodInfo info)
        {
            var paramters = info.GetParameters();
            if (paramters.Length != 1 || info.ReturnType != typeof(void))
            {
                throw new ArgumentException($"Virtual get property method {info} must be 1 paramter and return void");
            }
            var par1 = Expression.Parameter(typeof(object));
            var par2 = Expression.Parameter(typeof(object));

            var body = Expression.Call(Expression.Convert(par1, info.DeclaringType), info,
                Expression.Convert(par2, paramters[0].ParameterType));

            return Expression.Lambda<PropertySetter>(body, par1, par2).CompileSys();
        }
    }
}
