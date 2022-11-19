using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;

using Ao.ObjectDesign.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public class DataTemplatePropertyPanel<TScene, TSetting> : InitableObject, IPropertyPanel<TScene, TSetting>, IPropertyContextCreator<WpfTemplateForViewBuildContext, TSetting>, IPropertyContextCreator<TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public DataTemplatePropertyPanel(IDesignSession<TScene, TSetting> session)
            : this(session, new TemplateContextsDecoraterCollection<TScene, TSetting>())
        {
        }
        public DataTemplatePropertyPanel(IDesignSession<TScene, TSetting> session,
            TemplateContextsDecoraterCollection<TScene, TSetting> decoraters)
        {
            Session = session ?? throw new ArgumentNullException(nameof(session));
            Decoraters = decoraters ?? throw new ArgumentNullException(nameof(decoraters));
            AttackMode = AttackModes.None;
        }

        private ForViewDataTemplateSelector dataTemplateSelector;

        public ForViewDataTemplateSelector DataTemplateSelector => dataTemplateSelector;

        public AttackModes AttackMode { get; set; }

        public TemplateContextsDecoraterCollection<TScene, TSetting> Decoraters { get; }

        public IDesignSession<TScene, TSetting> Session { get; }

        protected override void OnInitialize()
        {
            dataTemplateSelector = CreateDataTemplateSelector();
        }

        protected virtual ForViewDataTemplateSelector CreateDataTemplateSelector()
        {
            Debug.Assert(Session != null);
            return Session.ObjectDesigner.CreateTemplateSelector();
        }

        public IEnumerable<WpfTemplateForViewBuildContext> CreateContexts(IDesignPair<UIElement, TSetting> pair)
        {
            Debug.Assert(Session != null);

            var ctxs = Session
                .ObjectDesigner
                .BuildDataTemplate(pair.DesigningObject, AttackMode)
                .GetEqualsInstanceContexts(pair.DesigningObject);//TODO: order by
            if (CanStepInThrowIfTag())
            {
                ctxs = ThrowIfTag(ctxs);
            }
            ctxs = Decoraters.Decorate(this, ctxs);
            return ctxs;
        }
        protected virtual bool CanStepInThrowIfTag()
        {
            return true;
        }
        private static IEnumerable<WpfTemplateForViewBuildContext> ThrowIfTag(IEnumerable<WpfTemplateForViewBuildContext> contexts)
        {
            foreach (var item in contexts)
            {
                if (item.PropertyProxy.PropertyInfo.GetCustomAttribute<IgnoreDesignAttribute>() == null)
                {
                    yield return item;
                }
            }
        }

        IEnumerable IPropertyContextCreator<TSetting>.CreateContexts(IDesignPair<UIElement, TSetting> pair)
        {
            return CreateContexts(pair);
        }
    }
}
