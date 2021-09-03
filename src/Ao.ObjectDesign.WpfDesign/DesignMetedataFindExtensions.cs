using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public static class DesignMetedataFindExtensions
    {
        public static IEnumerable<DependencyObject> GetBrothersWithContainer(this IDesignMetedata metedata)
        {
            return GetBrothersWithContainer(metedata,_ => true);
        }

        public static IEnumerable<DependencyObject> GetBrothersWithContainer(this IDesignMetedata metedata,Predicate<DependencyObject> childFilter)
        {
            if (metedata.Container != null)
            {
                var count = VisualTreeHelper.GetChildrenCount(metedata.Container);
                for (int i = 0; i < count; i++)
                {
                    var val = VisualTreeHelper.GetChild(metedata.Container, i);
                    if (childFilter(val))
                    {
                        yield return val;
                    }
                }
            }
        }
        public static T GetOrAddTransform<T>(this IDesignMetedata metedata)
            where T : Transform, new()
        {
            return DesignMetedata.GetOrAddTransform<T>(metedata.Target);
        }
    }
}
