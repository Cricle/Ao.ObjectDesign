using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfObjectDesigner : IWpfObjectDesignerSettings, IWpfObjectDesigner
    {
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
        protected object CreateFromMapping(DesignMapping mapping, DesignLevels designLevel)
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
            DesignLevels designLevel,
            AttackModes attackMode)
        {
            var instance = CreateFromMapping(mapping, designLevel);
            return BuildUI(instance, attackMode);
        }
        public IWpfDesignBuildUIResult BuildUI(object instance,
            AttackModes attackMode)
        {
            var proxy = Designer.CreateProxy(instance, instance.GetType());
            IEnumerable<IPropertyProxy> props = proxy.GetPropertyProxies();
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
            DesignLevels designLevel,
            AttackModes attackMode)
        {
            var instance = CreateFromMapping(mapping, designLevel);
            return BuildDataTemplate(instance, attackMode);
        }
        public IWpfDesignDataTemplateBuildResult BuildDataTemplate(object instance,
            AttackModes attackMode)
        {
            var proxy = Designer.CreateProxy(instance, instance.GetType());
            var ctxs = NotifySubscriber.Lookup(proxy).Select(x => new WpfTemplateForViewBuildContext
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
    }
}
