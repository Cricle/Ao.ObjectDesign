using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Converters
{
    public class TypeEnumConverter : IValueConverter
    {
        public static readonly TypeEnumConverter Instance = new TypeEnumConverter();

        private static readonly Dictionary<Type, WeakReference<Array>> enumCaches = new Dictionary<Type, WeakReference<Array>>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type t)
            {
                Array values;
                if (enumCaches.TryGetValue(t, out WeakReference<Array> enumValuesRef))
                {
                    if (!enumValuesRef.TryGetTarget(out values))
                    {
                        values = Enum.GetValues(t);
                        enumValuesRef.SetTarget(values);
                    }
                }
                else
                {
                    values = Enum.GetValues(t);
                    enumCaches.Add(t, new WeakReference<Array>(values));
                }
                Debug.Assert(values != null);
                return ConvertNames(t, values);
            }
            return Binding.DoNothing;
        }
        protected virtual Array ConvertNames(Type type, Array datas)
        {
            return datas;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && parameter is Type t)
            {
                return Enum.Parse(t, str);
            }
            return Binding.DoNothing;
        }
    }
}
