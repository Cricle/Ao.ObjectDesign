using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf.MessagePack
{
    public class TypelessFormatterResolver : IFormatterResolver
    {
        public TypelessFormatterResolver(IFormatterResolver resolver)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            ForceTypeless = new HashSet<Type>();
        }

        public IFormatterResolver Resolver { get; }

        public HashSet<Type> ForceTypeless { get; }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            if (ForceTypeless.Contains(typeof(T)))
            {
                return TypelessFormatter<T>.formatter;
            }
            return Resolver.GetFormatter<T>();
        }
        private static class TypelessFormatter<T>
        {
            public static readonly ForceTypelessFormatter<T> formatter = new ForceTypelessFormatter<T>();
        }
    }
}
