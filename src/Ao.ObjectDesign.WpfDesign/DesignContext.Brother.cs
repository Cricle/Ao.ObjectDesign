using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public partial class DesignContext
    {
        public IEnumerable<DependencyObject> GetBrothers()
        {
            return GetBrothers(_ => true);
        }
        public virtual IEnumerable<DependencyObject> GetBrothers(Predicate<DependencyObject> childFilter)
        {
            if (Container != null)
            {
                var count = VisualTreeHelper.GetChildrenCount(Container);
                for (int i = 0; i < count; i++)
                {
                    var val = VisualTreeHelper.GetChild(Container, i);
                    if (childFilter(val))
                    {
                        yield return val;
                    }
                }
            }
        }
    }
}
