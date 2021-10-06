using Ao.ObjectDesign.Designing;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP = MessagePack;

namespace Ao.ObjectDesign.Wpf.MessagePack
{
    public class IgnoreFormatterResolver : IFormatterResolver
    {
        public IgnoreFormatterResolver(IFormatterResolver resolver)
            :this(resolver,new HashSet<Type>())
        {
        }

        public IgnoreFormatterResolver(IFormatterResolver resolver, HashSet<Type> ignoreTypes)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            IgnoreTypes = ignoreTypes ?? throw new ArgumentNullException(nameof(ignoreTypes));
            ForceTypeless = new HashSet<Type>();
        }

        public IFormatterResolver Resolver { get; }

        public HashSet<Type> IgnoreTypes { get; }

        public HashSet<Type> ForceTypeless { get; }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            var type = typeof(T);
            if (IgnoreTypes.Contains(type))
            {
                return NullFormatter<T>.formatter;
            }
            if (ForceTypeless.Contains(type))
            {
                return TypelessFormatter<T>.formatter;
            }
            return Resolver.GetFormatter<T>();
        }
        private static class FormatterCache<T>
        {
            public static IMessagePackFormatter<T> Formatter;
        }
        private static class TypelessFormatter<T>
        {
            public static readonly ForceTypelessFormatter<T> formatter = new ForceTypelessFormatter<T>();
        }
        private static class NullFormatter<T>
        {
            public static readonly NullMessagePackFormatter<T> formatter = new NullMessagePackFormatter<T>();
        }
    }
}
