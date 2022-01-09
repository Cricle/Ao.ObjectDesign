using System;

namespace Ao.ObjectDesign.Data
{
    public abstract class DesignDataBinding
    {
        private object source;
        private object target;
        private string sourcePropertyName;
        private string targetPropertyName;
        private bool isBind;
        private object converterParamter;
        private IDesignValueConverter converter;
        private DesignDataBindingModes mode;
        private DesignUpdateSourceTrigger updateSourceTrigger;

        public object Source
        {
            get => source;
            set
            {
                ThrowIfBind();
                source = value;
            }
        }
        public object Target
        {
            get => target;
            set
            {
                ThrowIfBind();
                target = value;
            }
        }

        public string SourcePropertyName
        {
            get => sourcePropertyName;
            set
            {
                ThrowIfBind();
                sourcePropertyName = value;
            }
        }

        public string TargetPropertyName
        {
            get => targetPropertyName;
            set
            {
                ThrowIfBind();
                targetPropertyName = value;
            }
        }
        public object ConverterParamter
        {
            get => converterParamter;
            set
            {
                ThrowIfBind();
                converterParamter = value;
            }
        }

        public DesignDataBindingModes Mode
        {
            get => mode;
            set
            {
                ThrowIfBind();
                mode = value;
            }
        }

        public DesignUpdateSourceTrigger UpdateSourceTrigger
        {
            get => updateSourceTrigger;
            set
            {
                ThrowIfBind();
                updateSourceTrigger = value;
            }
        }
        public IDesignValueConverter Converter
        {
            get => converter;
            set
            {
                ThrowIfBind();
                converter = value;
            }
        }

        public bool IsBind => isBind;

        public void Bind()
        {
            if (isBind)
            {
                return;
            }
            OnBind();
            isBind = true;
            OnBindEnd();
        }
        protected virtual void OnBindEnd()
        {

        }

        public void UnBind()
        {
            if (!isBind)
            {
                return;
            }
            OnUnBind();
            isBind = false;
            OnUnBindEnd();
        }
        protected virtual void OnUnBindEnd()
        {

        }
        protected abstract void OnBind();
        protected abstract void OnUnBind();

        public abstract void UpdateSource();
        public abstract void UpdateTarger();

        protected void ThrowIfBind()
        {
            if (isBind)
            {
                throw new InvalidOperationException("When IsBind is true, can't change property value");
            }
        }
    }
}
