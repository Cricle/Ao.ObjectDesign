using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    public delegate void PropertyChangeToEventHandler(object sender, PropertyChangeToEventArgs e);
    [Serializable]
    public class NotifyableObject : DynamicObject, INotifyPropertyChanged, INotifyPropertyChanging, INotifyPropertyChangeTo
    {
        private IReadOnlyDictionary<string, PropertyBox> propertyTable;
        private string[] propertyNames;

        internal string[] PropertyNames
        {
            get
            {
                if (propertyNames==null)
                {
                    propertyNames = DynamicTypePropertyHelper.GetPropertyNames(GetType());
                }
                return propertyNames;
            }
        }

        internal IReadOnlyDictionary<string, PropertyBox> PropertyTable
        {
            get
            {
                if (propertyTable == null)
                {
                    propertyTable = DynamicTypePropertyHelper.GetPropertyMap(GetType());
                }
                return propertyTable;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangeToEventHandler PropertyChangeTo;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(name));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanging([CallerMemberName] string name = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChangeTo(object from, object to, [CallerMemberName] string name = null)
        {
            PropertyChangeTo?.Invoke(this, new PropertyChangeToEventArgs(name, from, to));
        }
        protected void Set<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                T origin = prop;
                RaisePropertyChanging(name);
                prop = value;
                RaisePropertyChanged(name);
                RaisePropertyChangeTo(origin, value, name);
            }
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return PropertyNames;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var ok = base.TryGetMember(binder, out result);
            if (!ok)
            {
                if (PropertyTable.TryGetValue(binder.Name, out var box))
                {
                    result = box.GetValue(this);
                    ok = true;
                }
            }
            return ok;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var ok = base.TrySetMember(binder, value);
            if (!ok)
            {
                if (PropertyTable.TryGetValue(binder.Name, out var box))
                {
                    box.SetValue(this, value);
                    ok = true;
                }
            }
            return ok;
        }
    }
}
