using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class BindingCreatorStateCreator<TScene,TSetting> : IBindingCreatorStateCreator<TSetting>
        where TScene:IDesignScene<TSetting>
    {
        public BindingCreatorStateCreator(BindingCreatorStateDecoraterCollection<TSetting> decoraters, IServiceProvider serviceProvider)
        {
            Decoraters = decoraters ?? throw new ArgumentNullException(nameof(decoraters));
            ServiceProvider = serviceProvider;
        }

        public BindingCreatorStateDecoraterCollection<TSetting> Decoraters { get; }

        public IServiceProvider ServiceProvider { get; }

        public DesignRuntimeTypes RuntimeType { get; set; }

        public IBindingCreatorState GetBindingCreatorState(IDesignPair<UIElement, TSetting> unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            var state = CreateState();
            Decoraters.Decorate(state, unit);
            return state;
        }
        protected virtual IBindingCreatorState CreateState()
        {
            var features = CreateFeatures();
            return new BindingCreatorState(ServiceProvider, features, RuntimeType);
        }

        protected virtual IDictionary CreateFeatures()
        {
            return new Dictionary<object, object>(0);
        }
    }
}
