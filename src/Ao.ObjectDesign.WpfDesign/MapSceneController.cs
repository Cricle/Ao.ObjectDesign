using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class MapSceneController<TDesignObject> : DesignSceneController<UIElement, TDesignObject>
    {
        private static readonly IBindingCreator<TDesignObject>[] emptyCreator = new IBindingCreator<TDesignObject>[0];

        protected MapSceneController(IDesignPackage<TDesignObject> designMap)
        {
            DesignPackage = designMap ?? throw new ArgumentNullException(nameof(designMap));
            DesignMap = designMap.UIDesinMap;
            UseUnitDesignAttribute = false;
            IgnoreBinding = false;
            bindingCreatorMap = new Dictionary<IDesignPair<UIElement, TDesignObject>, IEnumerable<IBindingCreator<TDesignObject>>>();
        }

        private readonly Dictionary<IDesignPair<UIElement, TDesignObject>, IEnumerable<IBindingCreator<TDesignObject>>> bindingCreatorMap; 
       
        public UIDesignMap DesignMap { get; }

        public IDesignPackage<TDesignObject> DesignPackage { get; }

        public bool IgnoreBinding { get; set; }

        public bool UseUnitDesignAttribute { get; set; }

        protected override void AddUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            AddToContainer(unit);
            var creators = BindDesignUnit(unit);
            Debug.Assert(!bindingCreatorMap.ContainsKey(unit));
            bindingCreatorMap.Add(unit, creators);
            foreach (var item in creators)
            {
                item.Attack();
            }
        }
        protected abstract void RemoveFromContainer(IDesignPair<UIElement, TDesignObject> unit);
        protected abstract void AddToContainer(IDesignPair<UIElement, TDesignObject> unit);

        protected override void RemoveUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            RemoveFromContainer(unit);
            var val = bindingCreatorMap[unit];
            bindingCreatorMap.Remove(unit);
            foreach (var item in val)
            {
                item.Detack();
            }
        }


        protected virtual IEnumerable<IBindingCreator<TDesignObject>> BindDesignUnit(IDesignPair<UIElement, TDesignObject> unit)
        {
            if (IgnoreBinding)
            {
                return emptyCreator;
            }
            var state = DesignPackage.CreateBindingCreatorState(unit);
            var creators = Compile(unit, state);
            var scopes = creators.SelectMany(x => x.BindingScopes);

            RunBinding(unit, scopes);

            return creators;
        }
        protected virtual void RunBinding(IDesignPair<UIElement, TDesignObject> unit,IEnumerable<IWithSourceBindingScope> scopes)
        {
            foreach (var item in scopes)
            {
                item.Bind(unit.UI);
            }
        }
        protected virtual IEnumerable<IBindingCreator<TDesignObject>> Compile(IDesignPair<UIElement, TDesignObject> unit,IBindingCreatorState state)
        {
            if (UseUnitDesignAttribute && unit.HasCreateAttributes())
            {
                return unit.CreateBindingCreatorFromAttribute();
            }
            return DesignPackage.GetBindingCreatorFactorys(unit, state)
                .SelectMany(x => x.Create(unit,state));
        }
        protected override UIElement CreateUI(TDesignObject designingObject)
        {
            var t = DesignMap.GetUIType(designingObject.GetType());
            if (t is null)
            {
                return null;
            }
            return (UIElement)DesignMap.CreateByFactoryOrEmit(t);
        }
    }
}
