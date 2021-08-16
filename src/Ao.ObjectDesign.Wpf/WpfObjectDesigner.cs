using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfObjectDesigner : IWpfObjectDesignerSettings, IWpfObjectDesigner
    {
        private static readonly Type ignoreDesignAttributeType = typeof(IgnoreDesignAttribute);
        private static readonly Func<IPropertyProxy,bool> defaultFilter=
            p => p.PropertyInfo.GetCustomAttribute(ignoreDesignAttributeType) == null;

        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> DataTemplateBuilder { get; }

        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> UIBuilder { get; }

        public IObjectDesigner Designer { get; }

        public IActionSequencer<IModifyDetail> Sequencer { get; }

        public IWpfUIGenerator UIGenerator { get; }

        public WpfObjectDesigner(WpfObjectDesignerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            settings.Check();

            DataTemplateBuilder = settings.DataTemplateBuilder;
            UIBuilder = settings.UIBuilder;
            Designer = settings.Designer;
            Sequencer = settings.Sequencer;
            UIGenerator = settings.UIGenerator;
        }
        public WpfObjectDesigner(bool useCompiledSequencer)
        {
            DataTemplateBuilder = new ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>();
            UIBuilder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();
            Designer = ObjectDesigner.Instance;
            UIGenerator = new UIGenerator(UIBuilder);
            if (useCompiledSequencer)
            {
                Sequencer = new CompiledPropertySequencer();
            }
            else
            {
                Sequencer = new PropertySequencer();
            }
        }
        protected virtual object CreateInstance(Type type)
        {
            return ReflectionHelper.Create(type);
        }
        protected object CreateFromMapping(DesignMapping mapping, 
            DesignLevels designLevel)
        {
            if (designLevel == DesignLevels.Setting)
            {
                return CreateInstance(mapping.ClrType);
            }
            else if (designLevel == DesignLevels.UI)
            {
                return CreateInstance(mapping.UIType);
            }
            throw new NotSupportedException(designLevel.ToString());
        }
        public IWpfDesignBuildUIResult BuildUI(DesignMapping mapping,
            DesignLevels designLevel)
        {
            return BuildUI(mapping, designLevel, null);
        }
        public IWpfDesignBuildUIResult BuildUI(DesignMapping mapping,
            DesignLevels designLevel,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            return BuildUI(mapping, designLevel, AttackModes.NativeAndDeclared,proxiesFilter);
        }
        public IWpfDesignBuildUIResult BuildUI(DesignMapping mapping,
            DesignLevels designLevel,
            AttackModes attackMode)
        {
            return BuildUI(mapping, designLevel, attackMode, null);
        }
        public IWpfDesignBuildUIResult BuildUI(DesignMapping mapping,
            DesignLevels designLevel,
            AttackModes attackMode,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            var instance = CreateFromMapping(mapping, designLevel);
            return BuildUI(instance, attackMode,proxiesFilter);
        }
        public IWpfDesignBuildUIResult BuildUI(object instance)
        {
            return BuildUI(instance, null);
        }
        public IWpfDesignBuildUIResult BuildUI(object instance, Func<IPropertyProxy, bool> proxiesFilter)
        {
            return BuildUI(instance, AttackModes.NativeAndDeclared, proxiesFilter);
        }
        public IWpfDesignBuildUIResult BuildUI(object instance,
            AttackModes attackMode)
        {
            return BuildUI(instance, attackMode, null);
        }
        public IWpfDesignBuildUIResult BuildUI(object instance,
            AttackModes attackMode,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var proxy = Designer.CreateProxy(instance, instance.GetType());
            IEnumerable<IPropertyProxy> props = proxy.GetPropertyProxies()
                .Where(proxiesFilter ?? defaultFilter);
            IEnumerable<IWpfUISpirit> ds = UIGenerator.Generate(props);
            IEnumerable<IWpfUISpirit> ctxs = ds.Where(x => x.View != null);
            IDisposable disposable = null;
            if (attackMode != AttackModes.None)
            {
                disposable = NotifySubscriber.Subscribe(ctxs.Select(x => x.Context), Sequencer, attackMode);
            }
            var result = new WpfDesignBuildUIResult
            {
                Contexts = ctxs,
                Designer = Designer,
                Sequencer = Sequencer,
                Subjected = disposable,
                Builder = UIBuilder
            };
            return result;
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(DesignMapping mapping,
            DesignLevels designLevel)
        {
            return BuildDataTemplate(mapping, designLevel, null);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(DesignMapping mapping,
            DesignLevels designLevel,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            return BuildDataTemplate(mapping, designLevel, AttackModes.VisitorAndDeclared, proxiesFilter);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(DesignMapping mapping,
            DesignLevels designLevel,
            AttackModes attackMode)
        {
            return BuildDataTemplate(mapping, designLevel, attackMode, null);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(DesignMapping mapping,
            DesignLevels designLevel,
            AttackModes attackMode,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            var instance = CreateFromMapping(mapping, designLevel);
            return BuildDataTemplate(instance, attackMode, proxiesFilter);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(object instance)
        {
            return BuildDataTemplate(instance, null);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(object instance,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            return BuildDataTemplate(instance, AttackModes.VisitorAndDeclared, proxiesFilter);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(object instance,
            AttackModes attackMode)
        {
            return BuildDataTemplate(instance, attackMode, null);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(object instance,
            AttackModes attackMode,
            Func<IPropertyProxy, bool> proxiesFilter)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var proxy = Designer.CreateProxy(instance, instance.GetType());
            var proxiesProperty = NotifySubscriber.Lookup(proxy)
                .Where(proxiesFilter ?? defaultFilter);

            var ctxs = proxiesProperty.Select(x => new WpfTemplateForViewBuildContext
            {
                Designer = ObjectDesigner.Instance,
                ForViewBuilder = DataTemplateBuilder,
                PropertyProxy = x,
                BindingMode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                UseNotifyVisitor = true
            }).Where(x => DataTemplateBuilder.Any(y => y.CanBuild(x))).ToArray();
            IDisposable disposable = null;
            if (attackMode != AttackModes.None)
            {
                disposable = NotifySubscriber.Subscribe(ctxs, Sequencer, attackMode);
            }
            var result = new WpfDesignDataTemplateBuildResult
            {
                Contexts = ctxs,
                Designer = Designer,
                Sequencer = Sequencer,
                Subjected = disposable,
                Builder = DataTemplateBuilder
            };
            return result;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(IForViewCondition<FrameworkElement, WpfForViewBuildContext> condition)
        {
            Debug.Assert(UIBuilder != null);
            UIBuilder.Add(condition);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(IEnumerable<IForViewCondition<FrameworkElement, WpfForViewBuildContext>> condition)
        {
            Debug.Assert(UIBuilder != null);
            UIBuilder.AddRange(condition);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext> condition)
        {
            Debug.Assert(DataTemplateBuilder != null);
            DataTemplateBuilder.Add(condition);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(IEnumerable<IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>> condition)
        {
            Debug.Assert(DataTemplateBuilder != null);
            DataTemplateBuilder.AddRange(condition);
        }
    }
}
