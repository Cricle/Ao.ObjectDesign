using Ao.ObjectDesign.Bindings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf
{
    public static class DesignMetedataFindExtensions
    {
        public static IEnumerable<DependencyObject> GetBrothersWithContainer(this IDesignMetedata<UIElement, IWpfDesignContext> metedata)
        {
            return GetBrothersWithContainer(metedata, null);
        }

        public static IEnumerable<DependencyObject> GetBrothersWithContainer(this IDesignMetedata<UIElement, IWpfDesignContext> metedata, Predicate<DependencyObject> childFilter)
        {
            if (metedata.Container != null)
            {
                var count = VisualTreeHelper.GetChildrenCount(metedata.Container);
                for (int i = 0; i < count; i++)
                {
                    var val = VisualTreeHelper.GetChild(metedata.Container, i);
                    if (childFilter is null)
                    {
                        yield return val;
                    }
                    else if (childFilter(val))
                    {
                        yield return val;
                    }
                }
            }
        }
        public static T GetOrAddTransform<T>(this IDesignMetedata<UIElement, IWpfDesignContext> metedata)
            where T : Transform, new()
        {
            return WpfDesignMetedata.GetOrAddTransform<T>(metedata.Target);
        }
    }
}
