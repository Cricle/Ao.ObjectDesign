using Ao.Lang.Runtime;
using Ao.ObjectDesign.Designing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public abstract class EnumStringConverter<T> : IValueConverter, IDisposable
    {
        public static readonly IReadOnlyList<T> Values = GetValues();

        public IReadOnlyList<LangBox> LangBoxs { get; }

        private static LanguageManager langMgr;

        protected static LanguageManager LangMgr
        {
            get
            {
                if (langMgr == null)
                {
                    langMgr = App.Provider.GetService<LanguageManager>();
                }
                return langMgr;
            }
        }
        public EnumStringConverter()
        {
            LangBoxs = GetEnumValues();
        }

        private static IReadOnlyList<T> GetValues()
        {
            var arr = Enum.GetValues(typeof(T));
            var ret = new T[arr.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = (T)arr.GetValue(i);
            }
            return ret;
        }

        private LangBox[] GetEnumValues()
        {
            var arr = Enum.GetValues(typeof(T));
            var ret = new LangBox[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                var val = (T)arr.GetValue(i);
                var key = GetLangKey(val);
                var box = new LangBox(key, val, LangMgr);
                box.UpdateValue();
                ret[i] = box;
            }
            return ret;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T kt)
            {
                var box = LangBoxs.FirstOrDefault(x => x.Value.Equals(kt));
                return box;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        protected abstract string GetLangKey(T type);

        public void Dispose()
        {
        }

        public class LangBox : NotifyableObject, IDisposable
        {
            private static readonly PropertyChangedEventArgs textChangedEventArgs = new PropertyChangedEventArgs(nameof(Text));
            private string text;

            public LangBox(string key, T value, LanguageManager languageManager)
            {
                Key = key;
                Value = value;
                LanguageManager = languageManager;
                languageManager.CultureInfoChanged += LanguageManager_CultureInfoChanged;
            }

            private void LanguageManager_CultureInfoChanged(CultureInfo obj)
            {
                UpdateValue();
            }

            public LanguageManager LanguageManager { get; }

            public string Key { get; }

            public T Value { get; }

            public string Text
            {
                get => text;
                private set
                {
                    text = value;
                    RaisePropertyChanged(textChangedEventArgs);
                }
            }
            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is T t)
                {
                    return t.Equals(Value);
                }
                if (obj is LangBox lb)
                {
                    return lb.Value.Equals(Value);
                }
                return false;
            }
            public void UpdateValue()
            {
                if (Key == null)
                {
                    Text = null;
                }
                else
                {
                    Text = LanguageManager.Root[Key];
                }
            }
            ~LangBox()
            {
                LanguageManager.CultureInfoChanged -= LanguageManager_CultureInfoChanged;
            }
            public void Dispose()
            {
                LanguageManager.CultureInfoChanged -= LanguageManager_CultureInfoChanged;
                GC.SuppressFinalize(this);
            }
        }

    }
}
