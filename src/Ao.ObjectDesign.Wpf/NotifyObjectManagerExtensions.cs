using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Wpf
{
    public static class NotifyObjectManagerExtensions
    {
        private static readonly string INotifyPropertyChangeToTypeName = typeof(INotifyPropertyChangeTo).FullName;

        public static void StripAll(this INotifyObjectManager mgr)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            List<INotifyPropertyChangeTo> s = mgr.Listenings.ToList();
            foreach (INotifyPropertyChangeTo item in s)
            {
                mgr.Strip(item);
            }
        }

        public static IReadOnlyList<INotifyPropertyChangeTo> AttackDeep(this INotifyObjectManager mgr, INotifyPropertyChangeTo instance)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            System.Reflection.PropertyInfo[] props = instance.GetType().GetProperties();
            mgr.Attack(instance);
            List<INotifyPropertyChangeTo> attacks = new List<INotifyPropertyChangeTo> { instance };
            foreach (System.Reflection.PropertyInfo prop in props)
            {
                if (prop.PropertyType.IsClass &&
                    prop.PropertyType.GetInterface(INotifyPropertyChangeToTypeName) != null)
                {
                    object val = prop.GetValue(instance);
                    if (val is INotifyPropertyChangeTo notifyer)
                    {
                        mgr.Attack(notifyer);
                        attacks.Add(notifyer);
                    }
                }
            }
            return attacks;
        }
    }
}
