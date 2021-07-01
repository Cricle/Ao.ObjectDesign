using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Ao.ObjectDesign
{
    public class CompiledPropertyVisitor : PropertyVisitor
    {
        public CompiledPropertyVisitor(object declaringInstance, PropertyInfo propertyInfo)
            : base(declaringInstance, propertyInfo)
        {
            if (CanSet)
            {
                setter = new Lazy<MSetter>(CreateSetter);
            }
            if (CanGet)
            {
                getter = new Lazy<MGetter>(CreateGetter);
            }
        }
        private readonly Lazy<MGetter> getter;
        private readonly Lazy<MSetter> setter;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private MSetter CreateSetter() => BuildSetter(PropertyInfo.DeclaringType, PropertyInfo);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private MGetter CreateGetter() => BuildGetter(PropertyInfo.DeclaringType, PropertyInfo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override object GetValue()
        {
            if (getter is null)
            {
                throw new InvalidOperationException("The property can't get");
            }
            return getter.Value();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void SetValue(object value)
        {
            if (setter is null)
            {
                throw new InvalidOperationException("The property can't set");
            }
            setter.Value(ConvertValue(value));
        }
        protected delegate object MGetter();
        protected delegate void MSetter(object value);
        private static readonly Type ObjectType = typeof(object);
        private static readonly Type[] GetterArgTypes = new Type[] { ObjectType };
        private static readonly Type[] SetterArgTypes = new Type[] { ObjectType , ObjectType };
        private static readonly MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
        private MGetter BuildGetter(Type type, PropertyInfo info)
        {
            var name = string.Concat("proxyget", type.Name, info.Name);
            var dn = new DynamicMethod(name, methodAttributes, CallingConventions.Standard,
                ObjectType, GetterArgTypes, type, true);
            var ilg = dn.GetILGenerator();
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Callvirt, info.GetMethod);
            if (info.PropertyType.IsValueType)
            {
                ilg.Emit(OpCodes.Box, info.PropertyType);
            }
            else
            {
                ilg.Emit(OpCodes.Castclass,ObjectType);
            }
            ilg.Emit(OpCodes.Ret);
            return (MGetter)dn.CreateDelegate(typeof(MGetter), DeclaringInstance);
        }
        private MSetter BuildSetter(Type type, PropertyInfo info)
        {
            var name = string.Concat("proxyset", type.Name, info.Name);
            var dn = new DynamicMethod(name, methodAttributes, CallingConventions.Standard,
                typeof(void), SetterArgTypes, type, true);
            var ilg = dn.GetILGenerator();
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Ldarg_1);
            if (info.PropertyType.IsValueType)
            {
                ilg.Emit(OpCodes.Unbox_Any, info.PropertyType);
            }
            else
            {
                ilg.Emit(OpCodes.Castclass, info.PropertyType);
            }
            ilg.Emit(OpCodes.Callvirt, info.SetMethod);
            ilg.Emit(OpCodes.Ret);
            return (MSetter)dn.CreateDelegate(typeof(MSetter),DeclaringInstance);
        }
    }
}