using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign
{
    public class CompiledPropertyVisitor : PropertyVisitor
    {
        protected delegate object MGetter();
        protected delegate void MSetter(object value);

        private static readonly Type ObjectType = typeof(object);
        private static readonly Type[] GetterArgTypes = new Type[] { ObjectType };
        private static readonly Type[] SetterArgTypes = new Type[] { ObjectType, ObjectType };
        private static readonly MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;

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
            RaiseValueChanged();
        }
        private MGetter BuildGetter(Type type, PropertyInfo info)
        {
            DynamicMethod dn = CreateObjectGetter(type, info);
            return (MGetter)dn.CreateDelegate(typeof(MGetter), DeclaringInstance);
        }

        private MSetter BuildSetter(Type type, PropertyInfo info)
        {
            DynamicMethod dn = CreateObjectSetter(type, info);
            return (MSetter)dn.CreateDelegate(typeof(MSetter), DeclaringInstance);
        }


        public static DynamicMethod CreateObjectGetter(Type type, PropertyInfo info)
        {
            if (!info.CanRead)
            {
                throw new MemberAccessException($"Type {type} property {info} can't read");
            }
            string name = "proxyget" + type.Name + info.Name;
            DynamicMethod dn = new DynamicMethod(name, methodAttributes, CallingConventions.Standard,
                ObjectType, GetterArgTypes, type, true);
            dn.InitLocals = false;
            ILGenerator ilg = dn.GetILGenerator();
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Callvirt, info.GetMethod);
            if (info.PropertyType.IsValueType)
            {
                ilg.Emit(OpCodes.Box, info.PropertyType);
            }
            else
            {
                ilg.Emit(OpCodes.Castclass, ObjectType);
            }
            ilg.Emit(OpCodes.Ret);
            return dn;
        }
        public static DynamicMethod CreateObjectSetter(Type type, PropertyInfo info)
        {
            if (!info.CanWrite)
            {
                throw new MemberAccessException($"Type {type} property {info} can't write");
            }
            string name = "proxyset" + type.Name + info.Name;
            DynamicMethod dn = new DynamicMethod(name, methodAttributes, CallingConventions.Standard,
                null, SetterArgTypes, type, true);
            dn.InitLocals = false;
            ILGenerator ilg = dn.GetILGenerator();
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
            return dn;
        }

    }
}