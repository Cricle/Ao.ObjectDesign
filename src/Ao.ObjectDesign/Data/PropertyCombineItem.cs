using System;

namespace Ao.ObjectDesign.Data
{
    public class PropertyCombineItem : ICombineItem
    {
        public PropertyCombineItem(PropertyProxy proxy)
        {
            Proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public PropertyProxy Proxy { get; }

        public string Name => Proxy.PropertyInfo.Name;

        public object GetValue()
        {
            return Proxy.Instance;
        }

        public void SetValue(object value)
        {
            Proxy.Instance = value;
        }
    }

}
