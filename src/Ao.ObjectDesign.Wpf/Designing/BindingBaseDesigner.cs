using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Data;

namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(BindingBase))]
    public abstract class BindingBaseDesigner : NotifyableObject
    {
        protected static readonly IReadOnlyHashSet<string> IncludePropertyNames = new ReadOnlyHashSet<string>(new[]
        {
            nameof(StringFormat),
            nameof(BindingGroupName),
            nameof(Delay)
        });

        private string stringFormat;
        private string bindingGroupName;
        private int delay;

        [DefaultValue(0)]
        public virtual int Delay
        {
            get => delay;
            set
            {
                Set(ref delay, value);
            }
        }

        [DefaultValue("")]
        public virtual string BindingGroupName
        {
            get => bindingGroupName;
            set
            {
                Set(ref bindingGroupName, value);
            }
        }

        [DefaultValue(null)]
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
