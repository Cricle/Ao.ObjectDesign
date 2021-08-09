using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Ao.ObjectDesign.Data
{
    public abstract class DesignPropertyDataBinding : DesignDataBinding
    {
        private INotifyPropertyChanged targetNotifyer;
        private INotifyPropertyChanged sourceNotifyer;
        private PropertyInfo sourcePropertyInfo;
        private PropertyInfo targetPropertyInfo;

        protected PropertyInfo SourcePropertyInfo => sourcePropertyInfo;
        protected PropertyInfo TargetPropertyInfo => targetPropertyInfo;

        public override void UpdateSource()
        {
            ThrowIfUnBind();
            UpdateSourceCore();
        }
        private void UpdateSourceCore()
        {
            var val = GetValue(Target, targetPropertyInfo);
            if (Converter != null)
            {
                val = Converter.ConvertBack(val, sourcePropertyInfo.PropertyType, ConverterParamter);
            }
            SetValue(Source, sourcePropertyInfo, val);
        }

        public override void UpdateTarger()
        {
            ThrowIfUnBind();
            UpdateTargerCore();
        }
        private void UpdateTargerCore()
        {
            CheckEnter();
            var val = GetValue(Source, sourcePropertyInfo);
            if (Converter != null)
            {
                val = Converter.Convert(val, targetPropertyInfo.PropertyType, ConverterParamter);
            }
            SetValue(Target, targetPropertyInfo, val);
        }

        protected override void OnBind()
        {
            Debug.Assert(targetNotifyer is null);
            Debug.Assert(sourceNotifyer is null);

            Debug.Assert(sourcePropertyInfo is null);
            Debug.Assert(targetPropertyInfo is null);

            if (Source is null || Target is null)
            {
                throw new InvalidOperationException("When Source or Target is null, can't to bind");
            }
            if (string.IsNullOrEmpty(SourcePropertyName) || string.IsNullOrEmpty(TargetPropertyName))
            {
                throw new InvalidOperationException("When SourcePropertyName or TargetPropertyName is null or empty, can't to bind");
            }
            if (Source == Target && TargetPropertyName == SourcePropertyName)
            {
                throw new InvalidOperationException("When source equals target, source property can't equal target property name");
            }
            sourcePropertyInfo = Source.GetType().GetProperty(SourcePropertyName);
            if (sourcePropertyInfo is null)
            {
                throw new InvalidOperationException($"Can't find property {SourcePropertyName} in type {Source.GetType()}");
            }
            targetPropertyInfo = Target.GetType().GetProperty(TargetPropertyName);
            if (targetPropertyInfo is null)
            {
                throw new InvalidOperationException($"Can't find property {TargetPropertyName} in type {Target.GetType()}");
            }
            if (Mode == DesignDataBindingModes.Defualt ||
                Mode == DesignDataBindingModes.TwoWay ||
                Mode == DesignDataBindingModes.OneWay)
            {

                if (!targetPropertyInfo.CanWrite)
                {
                    throw new InvalidOperationException($"When Mode is TwoWay/OneWayToSource, Target property {TargetPropertyName} must can write");
                }

                if (UpdateSourceTrigger != DesignUpdateSourceTrigger.Explicit)
                {
                    sourceNotifyer = Source as INotifyPropertyChanged;
                    if (sourceNotifyer is null)
                    {
                        throw new InvalidOperationException("When Mode is TwoWay/OneWayToSource UpdateSourceTrigger is PropertyChanged/Default, Source must implemnet INotifyPropertyChanged");
                    }
                    CreateSourceResource();
                    UpdateSourceCore();
                    sourceNotifyer.PropertyChanged += OnSourceNotifyerPropertyChanged;
                }
            }
            if (Mode == DesignDataBindingModes.TwoWay ||
                Mode == DesignDataBindingModes.OneWayToSource)
            {
                if (!sourcePropertyInfo.CanWrite)
                {
                    throw new InvalidOperationException($"When Mode is Defualt/OneWay/TwoWay, Source property {SourcePropertyName} must can write");
                }
                if (UpdateSourceTrigger != DesignUpdateSourceTrigger.Explicit)
                {
                    targetNotifyer = Target as INotifyPropertyChanged;
                    if (targetNotifyer is null)
                    {
                        throw new InvalidOperationException("When Mode is Defualt/OneWay/TwoWay UpdateSourceTrigger is PropertyChanged/Default, Target must implemnet INotifyPropertyChanged");
                    }
                    CreateTargetResource();
                    UpdateSourceCore();
                    targetNotifyer.PropertyChanged += OnTargetNotifyerPropertyChanged;
                }
            }
            if (Mode == DesignDataBindingModes.OneTime)
            {
                CreateTargetResource();
                UpdateTargerCore();
            }
        }

        protected virtual void CreateSourceResource()
        {

        }
        protected virtual void CreateTargetResource()
        {

        }

        protected abstract void SetValue(object instance, PropertyInfo sourceProp, object value);

        protected abstract object GetValue(object instance, PropertyInfo sourceProp);

        protected override void OnUnBind()
        {
            if (targetNotifyer != null)
            {
                targetNotifyer.PropertyChanged -= OnTargetNotifyerPropertyChanged;
            }
            if (sourceNotifyer != null)
            {
                sourceNotifyer.PropertyChanged -= OnSourceNotifyerPropertyChanged;
            }
            targetPropertyInfo = null;
            sourcePropertyInfo = null;
        }
        private void OnSourceNotifyerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SourcePropertyName)
            {
                UpdateTarger();
            }
        }


        private void OnTargetNotifyerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TargetPropertyName)
            {
                CheckEnter();
                UpdateSource();
            }
        }
        protected void ThrowIfUnBind()
        {
            if (!IsBind)
            {
                throw new InvalidOperationException($"The binding is no actived, can't do the operator");
            }
        }
        [Conditional("Debug")]
        private void CheckEnter()
        {
            Debug.Assert(targetPropertyInfo != null);
            Debug.Assert(sourcePropertyInfo != null);

            Debug.Assert(Source != null);
            Debug.Assert(Target != null);
        }
    }
}
