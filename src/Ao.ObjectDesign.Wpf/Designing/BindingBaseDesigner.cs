using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.Generic;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(BindingBase))]
    public abstract class BindingBaseDesigner : NotifyableObject
    {
        protected static readonly IReadOnlyHashSet<string> IncludePropertyNames = new ReadOnlyHashSet<string>(new string[]
        {
            nameof(StringFormat),
            nameof(BindingGroupName),
            nameof(Delay)
        });

        private string stringFormat;
        private string bindingGroupName;
        private int delay;

        public virtual int Delay
        {
            get => delay;
            set
            {
                Set(ref delay, value);
            }
        }

        public virtual string BindingGroupName
        {
            get => bindingGroupName;
            set
            {
                Set(ref bindingGroupName, value);
            }
        }

        public virtual string StringFormat
        {
            get => stringFormat;
            set
            {
                Set(ref stringFormat, value);
            }
        }
    }
}
