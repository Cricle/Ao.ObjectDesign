using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public static class BindingCreatorStateFetchExtensions
    {
        public static T GetFeature<T>(this IBindingCreatorState state,object key)
        {
            var feature = state.Features;
            if (feature is null)
            {
                return default;
            }
            if (feature.Contains(key))
            {
                var val = feature[key];
                if (val is T t)
                {
                    return t;
                }
                throw new InvalidCastException($"Key {key} value {val} can't case to {typeof(T)}");
            }
            return default;
        }

    }
}
