using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Wpf
{
    public static class NotifyObjectManagerExtensions
    {
        public static void StripAll(this INotifyObjectManager mgr)
        {
            var s = mgr.Listenings.ToList();
            foreach (var item in s)
            {
                mgr.Strip(item);
            }
        }
        public static IReadOnlyList<INotifyPropertyChangeTo> DeepAttack(this INotifyObjectManager mgr, INotifyPropertyChangeTo instance)
        {
            var props = instance.GetType().GetProperties();
            mgr.Attack(instance);
            var attacks = new List<INotifyPropertyChangeTo> { instance };
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsClass && prop.PropertyType.GetInterface(typeof(INotifyPropertyChangeTo).FullName) != null)
                {
                    var val = prop.GetValue(instance);
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
