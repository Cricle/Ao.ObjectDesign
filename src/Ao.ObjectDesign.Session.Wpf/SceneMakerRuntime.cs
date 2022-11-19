using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;

using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf
{
    public partial class SceneMakerRuntime<TDesignTool, TScene, TDesignObject> : NotifyableObject
        where TScene : IDesignScene<TDesignObject>
    {
        private static readonly DependencyPropertyDescriptor DesigningObjectPropertyDescript
               = DependencyPropertyDescriptor.FromProperty(DesignSuface.DesigningObjectProperty, typeof(DesignSuface));

        private IDesignSession<TScene, TDesignObject> currentSession;
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
                Set(ref forViewDataTemplateSelector, value);
            }
        }

        public IDesignSession<TScene, TDesignObject> CurrentSession
        {
            get => currentSession;
            set
            {
                var old = currentSession;
                if (old != value)
                {
                    currentSession = value;
                    OnCurrentSessionChanged(old, value);
                    CurrentSessionChanged?.Invoke(this, new ValueChangedEventArgs<IDesignSession<TScene, TDesignObject>>(old, value));
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

        public event EventHandler<ValueChangedEventArgs<IDesignSession<TScene, TDesignObject>>> CurrentSessionChanged;

        public SilentObservableCollection<TScene> Outline { get; }
        public SilentObservableCollection<TDesignTool> DesignTools { get; }

        private readonly EventHandler sessionChangeHandler;

        public SceneMakerRuntime()
        {
            sessionChangeHandler = EventHandler;
            Outline = new SilentObservableCollection<TScene>();
            DesignTools = new SilentObservableCollection<TDesignTool>();
        }

        protected virtual void OnCurrentSessionChanged(IDesignSession<TScene, TDesignObject> old, IDesignSession<TScene, TDesignObject> @new)
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
                throw new InvalidOperationException("There is no session");
            }
        }
    }
}
