using Ao.ObjectDesign.Wpf.Conditions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfPropertyVisitor : PropertyVisitor, INotifyPropertyChanged, IDisposable,INotifyPropertyChangeTo
    {
        public WpfPropertyVisitor(DependencyObject declaringInstance, PropertyInfo propertyInfo)
            : base(declaringInstance, propertyInfo)
        {
            if (DependencyObjectHelper.GetDependencyPropertyDescriptors(declaringInstance.GetType())
                .TryGetValue(propertyInfo.Name, out var dpd))
            {
                selectedPropertyDescriptor = dpd;
            }
            DeclaringInstance = declaringInstance;
            if (selectedPropertyDescriptor != null)
            {
                selectedPropertyDescriptor.AddValueChanged(DeclaringInstance, OnPropertyChanged);
            }
        }
        private DependencyPropertyDescriptor selectedPropertyDescriptor;

        public DependencyPropertyDescriptor SelectedPropertyDescriptor => selectedPropertyDescriptor;

        public new DependencyObject DeclaringInstance { get; }

        public event PropertyChangeToEventHandler PropertyChangeTo;

        protected void RaisePropertyChangeTo(object from,object to,[CallerMemberName]string name=null)
        {
            PropertyChangeTo?.Invoke(this, new PropertyChangeToEventArgs(name, from, to));
        }
        private void OnPropertyChanged(object d, EventArgs e)
        {
            if (d == DeclaringInstance)
            {
                RaisePropertyChanged(DeclaringInstance,selectedPropertyDescriptor.Name);
            }
        }

        public override object GetValue()
        {
            if (selectedPropertyDescriptor is null)
            {
                return base.GetValue();
            }
            return DeclaringInstance.GetValue(selectedPropertyDescriptor.DependencyProperty);
        }
        public override void SetValue(object value)
        {
            if (selectedPropertyDescriptor is null)
            {
                base.SetValue(value);
            }
            else
            {
                var origin = GetValue();
                value = ConvertValue(value);
                DeclaringInstance.SetValue(selectedPropertyDescriptor.DependencyProperty, value);
                RaiseValueChanged();
                RaisePropertyChangeTo(origin, value, nameof(Value));
            }
        }
        ~WpfPropertyVisitor()
        {
            OnDispose();
        }
        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }
        protected virtual void OnDispose()
        {
            if (selectedPropertyDescriptor != null)
            {
                selectedPropertyDescriptor.RemoveValueChanged(DeclaringInstance, OnPropertyChanged);
            }
            selectedPropertyDescriptor = null;
        }
    }
}
