using Ao.ObjectDesign.Bindings.BindingCreators;
using Ao.ObjectDesign.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class BindingInfo<TBindingMaker>
    {
        protected BindingInfo(BindingScopeCompiler<TBindingMaker> compiler)
        {
            Compiler = compiler;
            SettingType = compiler.SettingType;
            UIType = compiler.UIType;

            Makers = compiler.Analysis().ToArray();
            PropertyBoxes = DynamicTypePropertyHelper.EnumerablePropertyBoxs(SettingType).ToArray();

            BindingPairMap = PropertyBoxes.ToDictionary(x => x.Name, x =>
            {
                var maker = GetBindingMaker(x);
                return new BindingPair<TBindingMaker>(maker, x);
            });
        }
        
        public BindingScopeCompiler<TBindingMaker> Compiler { get; }

        public Type SettingType { get; }

        public Type UIType { get; }

        public IReadOnlyList<TBindingMaker> Makers { get; }

        public IReadOnlyList<IPropertyBox> PropertyBoxes { get; }

        public IReadOnlyDictionary<string, BindingPair<TBindingMaker>> BindingPairMap { get; }

        protected abstract TBindingMaker GetBindingMaker(IPropertyBox box);
    }
}
