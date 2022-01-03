using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ObjectDesign.Brock
{
    public partial class SceneMakerRuntime<TDesignTool>:NotifyableObject
    {
        private static readonly DependencyPropertyDescriptor DesigningObjectPropertyDescript
            = DependencyPropertyDescriptor.FromProperty(DesignSuface.DesigningObjectProperty, typeof(DesignSuface));

        private IDesignSession<Scene,UIElementSetting> currentSession;
        private ForViewDataTemplateSelector forViewDataTemplateSelector;
        private TDesignTool selectedTool;

        public TDesignTool SelectedTool
        {
            get => selectedTool;
            set => Set(ref selectedTool, value);
        }

        public ForViewDataTemplateSelector ForViewDataTemplateSelector
        {
            get => forViewDataTemplateSelector;
            set
            {
                Set(ref forViewDataTemplateSelector,value);
            }
        }

        public IDesignSession<Scene, UIElementSetting> CurrentSession
        {
            get => currentSession;
            set
            {
                var old = currentSession;
                if (old != value)
                {
                    currentSession = value;
                    OnCurrentSessionChanged(old, value);
                    CurrentSessionChanged?.Invoke(this, new ValueChangedEventArgs<IDesignSession<Scene, UIElementSetting>>(old, value));
                }
            }
        }

        public UIElement[] DesigningObjects
        {
            get
            {
                ThrowIfNoSession();

                return CurrentSession.Suface.DesigningObjects;
            }
            set
            {
                ThrowIfNoSession();
                CurrentSession.Suface.DesigningObjects = value;
            }
        }

        public bool AutoSwithDesignPanel { get; set; }

        public bool HasSession => currentSession != null;

        public event EventHandler<ValueChangedEventArgs<IDesignSession<Scene, UIElementSetting>>> CurrentSessionChanged;

        public SilentObservableCollection<UIElementSetting> Outline { get; }
        public SilentObservableCollection<TDesignTool> DesignTools { get; }

        private readonly EventHandler sessionChangeHandler;

        public SceneMakerRuntime()
        {
            sessionChangeHandler = EventHandler;
            Outline = new SilentObservableCollection<UIElementSetting>();
            DesignTools = new SilentObservableCollection<TDesignTool>();
        }

        protected virtual void OnCurrentSessionChanged(IDesignSession<Scene, UIElementSetting> old, IDesignSession<Scene, UIElementSetting> @new)
        {
            Outline.Clear();
            if (old != null)
            {
                DesigningObjectPropertyDescript.RemoveValueChanged(old.Suface, sessionChangeHandler);
            }
            if (@new != null)
            {
                DesigningObjectPropertyDescript.AddValueChanged(@new.Suface, sessionChangeHandler);
                Outline.Add(@new.SceneManager.CurrentScene);
            }
        }
        private void EventHandler(object sender, EventArgs e)
        {
            var s = (DesignSuface)sender;
            var value = s.DesigningObjects;
            if (AutoSwithDesignPanel && value != null && value.Length == 1)
            {
                SwithDesigningContexts(value[0]);
            }
            else
            {
                DesigningContexts.Clear();
            }
        }

        protected virtual void ThrowIfNoSession()
        {
            if (!HasSession)
            {
                throw new InvalidOperationException("Now is no session");
            }
        }
    }
}
