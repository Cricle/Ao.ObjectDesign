using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Expander))]
    public class ExpanderSetting : ControlSetting,IMiddlewareDesigner<Expander>
    {
        private ExpandDirection expandDirection;
        private bool isExpanded;

        public virtual bool IsExpanded
        {
            get => isExpanded;
            set => Set(ref isExpanded, value);
        }

        public virtual ExpandDirection ExpandDirection
        {
            get => expandDirection;
            set => Set(ref expandDirection, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            ExpandDirection = ExpandDirection.Down;
            IsExpanded = false;
        }

        public void Apply(Expander value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                IsExpanded = value.IsExpanded;
                ExpandDirection = value.ExpandDirection;
            }
        }

        public void WriteTo(Expander value)
        {
            if (value !=null)
            {
                WriteTo((Control)value);
                value.IsExpanded = isExpanded;
                value.ExpandDirection = expandDirection;
            }
        }
    }
}
