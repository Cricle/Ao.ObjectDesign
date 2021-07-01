using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign
{
    public class PropertyVisitor : ObjectProxy, IPropertyVisitor
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> typeConverterMap = new ConcurrentDictionary<Type, TypeConverter>();

        public PropertyVisitor(object declaringInstance, PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            DeclaringInstance = declaringInstance ?? throw new ArgumentNullException(nameof(declaringInstance));

            Type = PropertyInfo.PropertyType;

            if (!PropertyInfo.DeclaringType.IsInstanceOfType(declaringInstance))
            {
                throw new ArgumentException($"declaringInstance is not equals or base on {PropertyInfo.DeclaringType.FullName}");
            }
            var typeDescAttr = PropertyInfo.GetCustomAttribute<TypeConverterAttribute>();
            if (typeDescAttr != null)
            {
                var type = Type.GetType(typeDescAttr.ConverterTypeName, true);
                typeConverter = typeConverterMap.GetOrAdd(type, t => (TypeConverter)Activator.CreateInstance(t));
            }
            else
            {
                typeConverter = TypeDescriptor.GetConverter(PropertyInfo.PropertyType);
            }
        }
        private TypeConverter typeConverter;

        public override object Instance => GetValue();

        public override Type Type { get; }

        public PropertyInfo PropertyInfo { get; }

        public bool CanGet => PropertyInfo.CanRead;

        public bool CanSet => PropertyInfo.CanWrite;

        public object DeclaringInstance { get; }

        public TypeConverter TypeConverter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => typeConverter;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => typeConverter = value;
        }

        public object Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetValue();
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => SetValue(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual object GetValue()
        {
            Debug.Assert(DeclaringInstance != null);
            return PropertyInfo.GetValue(DeclaringInstance);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected object ConvertValue(object value)
        {
            if (value != null && typeConverter != null && !Type.IsInstanceOfType(value))
            {
                value = typeConverter.ConvertFrom(value);
            }
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void SetValue(object value)
        {
            Debug.Assert(DeclaringInstance != null);
            PropertyInfo.SetValue(DeclaringInstance, ConvertValue(value));
        }
    }
}