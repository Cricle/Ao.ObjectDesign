using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Bindings.BindingCreators;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.AutoBind
{
    public class WpfSharedBindingInfo : BindingInfo<IBindingMaker>
    {
        private static readonly Dictionary<UISpiritType, WpfSharedBindingInfo> sharedBindingInfos = new Dictionary<UISpiritType, WpfSharedBindingInfo>();

        public WpfSharedBindingInfo(BindingScopeCompiler<IBindingMaker> compiler)
            : base(compiler)
        {
        }

        public static WpfSharedBindingInfo GetBindingInfo(Type uiType, Type settingType)
        {
            var key = new UISpiritType(settingType, uiType);
            if (!sharedBindingInfos.TryGetValue(key, out var info))
            {
                info = new WpfSharedBindingInfo(new WpfBindingScopeCompiler(settingType, uiType));
                sharedBindingInfos[key] = info;
            }
            return info;
        }

        private Dictionary<string, IBindingMaker> bindingMakerMap;

        protected override IBindingMaker GetBindingMaker(IPropertyBox box)
        {
            if (bindingMakerMap == null)
            {
                bindingMakerMap = Makers.ToDictionary(x => x.DependencyProperty.Name);
            }
            bindingMakerMap.TryGetValue(box.Name, out var maker);
            return maker;
        }

    }
}
