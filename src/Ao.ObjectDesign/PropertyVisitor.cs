using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign
{
    public class PropertyVisitor : ObjectProxy, IPropertyVisitor, INotifyPropertyChanged
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> typeConverterMap = new ConcurrentDictionary<Type, TypeConverter>();
        private static readonly PropertyChangedEventArgs valueHandledEventArgs = new PropertyChangedEventArgs(nameof(Value));

        private static TypeConverter CreateConvert(Type type)
        {
            return (TypeConverter)Activator.CreateInstance(type);
        }

        public PropertyVisitor(object declaringInstance, PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            DeclaringInstance = declaringInstance ?? throw new ArgumentNullException(nameof(declaringInstance));

            Type = PropertyInfo.PropertyType;

            if (!PropertyInfo.DeclaringType.IsInstanceOfType(declaringInstance))
            {
                throw new ArgumentException($"declaringInstance is not equals or base on {PropertyInfo.DeclaringType.FullName}");
            }
            TypeConverterAttribute typeDescAttr = PropertyInfo.GetCustomAttribute<TypeConverterAttribute>();
            if (typeDescAttr != null)
            {
                Type type = Type.GetType(typeDescAttr.ConverterTypeName, true);
                typeConverter = typeConverterMap.GetOrAdd(type, CreateConvert);
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(object instance, [CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(instance, new PropertyChangedEventArgs(name));
        }

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
            RaiseValueChanged();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaiseValueChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, valueHandledEventArgs);
            }
        }
    }
}