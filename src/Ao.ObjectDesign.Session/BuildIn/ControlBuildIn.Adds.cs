using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign.Input;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public partial class ControlBuildIn<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public ControlBuildIn<TScene, TSetting> AddTemplateForViewCondition(IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext> condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            engine.DataTemplateBuilder.Add(condition);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddTemplateForViewCondition(IEnumerable<IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>> conditions)
        {
            if (conditions is null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            engine.DataTemplateBuilder.AddRange(conditions);
            return this;
        }

        public ControlBuildIn<TScene, TSetting> AddMap(Type uiType, Type designType)
        {
            if (uiType is null)
            {
                throw new ArgumentNullException(nameof(uiType));
            }

            if (designType is null)
            {
                throw new ArgumentNullException(nameof(designType));
            }

            engine.DesignPackage.UIDesinMap.RegistDesignType(uiType, designType);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddMap<TUI, TDesigner>()
        {
            return AddMap(typeof(TUI), typeof(TDesigner));
        }
        public ControlBuildIn<TScene, TSetting> AddInstacnceFactory(IInstanceFactory factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            engine.DesignPackage.UIDesinMap.RegistInstanceFactory(factory);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddBindingStateDecorate(IBindingCreatorStateDecorater<TSetting> bindingCreatorStateDecorater)
        {
            if (bindingCreatorStateDecorater is null)
            {
                throw new ArgumentNullException(nameof(bindingCreatorStateDecorater));
            }

            engine.BindingCreatorStateDecoraters.Add(bindingCreatorStateDecorater);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddBindingStateDecorate(IEnumerable<IBindingCreatorStateDecorater<TSetting>> bindingCreatorStateDecoraters)
        {
            if (bindingCreatorStateDecoraters is null)
            {
                throw new ArgumentNullException(nameof(bindingCreatorStateDecoraters));
            }

            engine.BindingCreatorStateDecoraters.AddRange(bindingCreatorStateDecoraters);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddTemplateContextDecorate(IPropertyContextsDecorater<WpfTemplateForViewBuildContext, TScene, TSetting> templatePropertyContextsDecorater)
        {
            if (templatePropertyContextsDecorater is null)
            {
                throw new ArgumentNullException(nameof(templatePropertyContextsDecorater));
            }

            engine.TemplateContextsDecoraters.Add(templatePropertyContextsDecorater);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddTemplateContextDecorate(IEnumerable<IPropertyContextsDecorater<WpfTemplateForViewBuildContext, TScene, TSetting>> templatePropertyContextsDecoraters)
        {
            if (templatePropertyContextsDecoraters is null)
            {
                throw new ArgumentNullException(nameof(templatePropertyContextsDecoraters));
            }

            engine.TemplateContextsDecoraters.AddRange(templatePropertyContextsDecoraters);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddDesignOrder(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            engine.DesignOrderManager.AddWithType(type);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddDesignOrder(PropertyInfo propertyInfo, int order)
        {
            if (propertyInfo is null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            var identity = new PropertyIdentity(propertyInfo.DeclaringType, propertyInfo.Name);
            engine.DesignOrderManager[identity] = order;
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddDesignOrder<T>(Expression<Func<T, object>> propertyMap, int order)
        {
            if (propertyMap is null)
            {
                throw new ArgumentNullException(nameof(propertyMap));
            }
            engine.DesignOrderManager.Add(propertyMap, order);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddBindingCreatorFactory(IBindingCreatorFactory<UIElement, TSetting, IWithSourceBindingScope> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            engine.DesignPackage.BindingCreators.Add(factory);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddBindingCreatorFactory(IEnumerable<IBindingCreatorFactory<UIElement, TSetting, IWithSourceBindingScope>> factories)
        {
            if (factories is null)
            {
                throw new ArgumentNullException(nameof(factories));
            }

            engine.DesignPackage.BindingCreators.AddRange(factories);
            return this;
        }
        public ControlBuildIn<TScene, TSetting> AddBindingCreatorFactoryByAttribute(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            engine.DesignPackage.BindingCreators.AddFromAttributeBindingCreator(type);
            return this;
        }
    }
}
