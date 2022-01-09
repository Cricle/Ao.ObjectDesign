using Ao.Lang.Runtime;
using Ao.ObjectDesign.Designing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace ObjectDesign.Brock.Models
{
    public class DesignTool : NotifyableObject, IDisposable
    {
        public DesignTool()
        {
            DragCommand = new RelayCommand<DependencyObject>(Drag);
        }

        private string name;
        private DesignToolTypes type;
        private char icon;
        private Type settingType;
        private bool isSelected;

        public string LangKey { get; set; }

        public bool IsSelected
        {
            get => isSelected;
            set => Set(ref isSelected, value);
        }

        public Type SettingType
        {
            get => settingType;
            set => Set(ref settingType, value);
        }

        public char Icon
        {
            get => icon;
            set => Set(ref icon, value);
        }

        public DesignToolTypes Type
        {
            get => type;
            set => Set(ref type, value);
        }

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public RelayCommand<DependencyObject> DragCommand { get; }
        internal MySceneMakerRuntime runtime;

        public void Drag(DependencyObject source)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (runtime.SelectedTool != this)
                {
                    runtime.SelectedTool = this;
                    IsSelected = true;
                }
                else
                {
                    DataObject dt = new DataObject(DropKeys.DropToolKey, this);
                    DragDrop.DoDragDrop(source, dt, DragDropEffects.Move);
                }
            }
        }


        public override string ToString()
        {
            return $"{{{name}}}";
        }

        public void Listen(IServiceProvider provider)
        {
            var mgr = provider.GetRequiredService<LanguageManager>();
            mgr.CultureInfoChanged += OnMgrCultureInfoChanged;
        }

        private void OnMgrCultureInfoChanged(CultureInfo obj)
        {
            UpdateName(App.Provider);
        }
        public void UpdateName(IServiceProvider provider)
        {
            var mgr = provider.GetRequiredService<LanguageManager>();
            var root = mgr.Root;
            if (root != null)
            {

                Name = root[LangKey];
            }
        }

        public void Dispose()
        {
            var mgr = App.Provider.GetRequiredService<LanguageManager>();
            mgr.CultureInfoChanged -= OnMgrCultureInfoChanged;
        }
    }
}
