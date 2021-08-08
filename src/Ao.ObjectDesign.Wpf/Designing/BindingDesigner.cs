using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Binding))]
    public class BindingDesigner : BindingBaseDesigner, IDefaulted
    {
        private UpdateSourceTrigger updateSourceTrigger;
        private bool notifyOnSourceUpdated;
        private bool notifyOnTargetUpdated;
        private bool notifyOnValidationError;
        private CultureInfoDesigner converterCulture;
        private string elementName;
        private bool isAsync;
        private BindingMode mode;
        private string xPath;
        private bool validatesOnDataErrors;
        private bool validatesOnNotifyDataErrors;
        private bool bindsDirectlyToSource;
        private bool validatesOnExceptions;
        private PropertyPathDesigner path;
        private RelativeSourceDesigner relativeSource;

        public virtual RelativeSourceDesigner RelativeSource
        {
            get => relativeSource;
            set
            {
                if (relativeSource != null)
                {
                    relativeSource.PropertyChanged -= OnRelativeSourcePropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnRelativeSourcePropertyChanged;
                }
                Set(ref relativeSource, value);
            }
        }


        public virtual PropertyPathDesigner Path
        {
            get => path;
            set
            {
                if (path != null)
                {
                    path.PropertyChanged -= OnPropertyPathPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged -= OnPropertyPathPropertyChanged;
                }
                Set(ref path, value);
                RaiseBindingChanged();
            }
        }


        [DefaultValue(false)]
        public virtual bool ValidatesOnExceptions
        {
            get => validatesOnExceptions;
            set
            {
                Set(ref validatesOnExceptions, value);
                RaiseBindingChanged();
            }
        }
        [DefaultValue(false)]
        public virtual bool BindsDirectlyToSource
        {
            get => bindsDirectlyToSource;
            set
            {
                Set(ref bindsDirectlyToSource, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(true)]
        public virtual bool ValidatesOnNotifyDataErrors
        {
            get => validatesOnNotifyDataErrors;
            set
            {
                Set(ref validatesOnNotifyDataErrors, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool ValidatesOnDataErrors
        {
            get => validatesOnDataErrors;
            set
            {
                Set(ref validatesOnDataErrors, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(null)]
        public virtual string XPath
        {
            get => xPath;
            set
            {
                Set(ref xPath, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(BindingMode.Default)]
        public virtual BindingMode Mode
        {
            get => mode;
            set
            {
                Set(ref mode, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool IsAsync
        {
            get => isAsync;
            set
            {
                Set(ref isAsync, value);
                RaiseBindingChanged();
            }
        }

        public virtual string ElementName
        {
            get => elementName;
            set
            {
                Set(ref elementName, value);
                RaiseBindingChanged();
            }
        }

        public virtual CultureInfoDesigner ConverterCulture
        {
            get => converterCulture;
            set
            {
                if (converterCulture != null)
                {
                    converterCulture.PropertyChanged += OnCultureInfoPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged -= OnCultureInfoPropertyChanged;
                }
                Set(ref converterCulture, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool NotifyOnValidationError
        {
            get => notifyOnValidationError;
            set
            {
                Set(ref notifyOnValidationError, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool NotifyOnTargetUpdated
        {
            get => notifyOnTargetUpdated;
            set
            {
                Set(ref notifyOnTargetUpdated, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool NotifyOnSourceUpdated
        {
            get => notifyOnSourceUpdated;
            set
            {
                Set(ref notifyOnSourceUpdated, value);
                RaiseBindingChanged();
            }
        }

        [DefaultValue(UpdateSourceTrigger.Default)]
        public virtual UpdateSourceTrigger UpdateSourceTrigger
        {
            get => updateSourceTrigger;
            set
            {
                Set(ref updateSourceTrigger, value);
                RaiseBindingChanged();
            }
        }

        [PlatformTargetProperty]
        public virtual Binding Binding
        {
            get
            {
                var bd = new Binding
                {
                    BindingGroupName = BindingGroupName,
                    BindsDirectlyToSource = bindsDirectlyToSource,
                    ConverterCulture = converterCulture?.CultureInfo,
                    Delay = Delay,
                    IsAsync = isAsync,
                    Mode = mode,
                    NotifyOnSourceUpdated = notifyOnSourceUpdated,
                    NotifyOnTargetUpdated = notifyOnTargetUpdated,
                    NotifyOnValidationError = notifyOnValidationError,
                    Path = path?.PropertyPath,
                    StringFormat = StringFormat,
                    ValidatesOnDataErrors = validatesOnDataErrors,
                    ValidatesOnExceptions = validatesOnExceptions,
                    ValidatesOnNotifyDataErrors = validatesOnNotifyDataErrors,
                    XPath = xPath,
                    UpdateSourceTrigger = updateSourceTrigger
                };
                if (!string.IsNullOrEmpty(elementName))
                {
                    bd.ElementName = elementName;
                }
                return bd;
            }
            set
            {
                if (value is null)
                {
                    SetDefault();
                }
                else
                {
                    UpdateSourceTrigger = value.UpdateSourceTrigger;
                    NotifyOnSourceUpdated = value.NotifyOnSourceUpdated;
                    NotifyOnTargetUpdated = value.NotifyOnTargetUpdated;
                    NotifyOnValidationError = value.NotifyOnValidationError;
                    ConverterCulture = new CultureInfoDesigner { CultureInfo = value.ConverterCulture };
                    ElementName = value.ElementName;
                    IsAsync = value.IsAsync;
                    Mode = value.Mode;
                    XPath = value.XPath;
                    ValidatesOnDataErrors = value.ValidatesOnDataErrors;
                    ValidatesOnNotifyDataErrors = value.ValidatesOnNotifyDataErrors;
                    BindsDirectlyToSource = value.BindsDirectlyToSource;
                    ValidatesOnExceptions = value.ValidatesOnExceptions;
                    Path = new PropertyPathDesigner { PropertyPath = value.Path };
                }
            }
        }

        public virtual void SetDefault()
        {
            UpdateSourceTrigger = UpdateSourceTrigger.Default;
            NotifyOnSourceUpdated = false;
            NotifyOnTargetUpdated = false;
            NotifyOnValidationError = false;
            ConverterCulture = new CultureInfoDesigner();
            ElementName = null;
            IsAsync = false;
            Mode = BindingMode.Default;
            XPath = null;
            ValidatesOnNotifyDataErrors = true;
            ValidatesOnDataErrors = BindsDirectlyToSource = ValidatesOnExceptions = false;
        }


        protected void RaiseBindingChanged()
        {
            RaisePropertyChanged(nameof(Binding));
        }

        private void OnCultureInfoPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CultureInfoDesigner.CultureInfo))
            {
                RaiseBindingChanged();
            }
        }

        private void OnRelativeSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RelativeSourceDesigner.RelativeSource))
            {
                RaiseBindingChanged();
            }
        }
        private void OnPropertyPathPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PropertyPathDesigner.PropertyPath))
            {
                RaiseBindingChanged();
            }
        }
    }
}
