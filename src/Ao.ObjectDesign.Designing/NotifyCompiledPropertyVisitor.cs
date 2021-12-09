using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    public class NotifyCompiledPropertyVisitor : ExpressionPropertyVisitor, INotifyPropertyChangeTo
    {
        public NotifyCompiledPropertyVisitor(object declaringInstance, PropertyInfo propertyInfo)
            : base(declaringInstance, propertyInfo)
        {

        }

        public event PropertyChangeToEventHandler PropertyChangeTo;

        protected virtual void RaisePropertyChangeTo(object sender, object from, object to, [CallerMemberName] string name = null)
        {
            PropertyChangeTo?.Invoke(sender, new PropertyChangeToEventArgs(name, from, to));
        }
        public override void SetValue(object value)
        {
            object origin = GetValue();
            base.SetValue(value);
            if (origin != value)
            {
                RaisePropertyChangeTo(this, origin, value, nameof(Value));
            }
        }
    }
}
