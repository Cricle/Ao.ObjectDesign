using System;

namespace Ao.ObjectDesign.Data
{
    public interface IDesignValueConverter
    {
        object Convert(object value, Type targetType, object paramter);

        object ConvertBack(object value, Type targetType, object paramter);
    }
}
