using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.AutoBind
{
    public class SharedBindingInfo
    {
        private static readonly Dictionary<Type, SharedBindingInfo> sharedBindingInfos = new Dictionary<Type, SharedBindingInfo>();

        public static SharedBindingInfo GetBindingInfo(Type settingType, Type uiType)
        {
            if (!sharedBindingInfos.TryGetValue(settingType, out var info))
            {
                info=new SharedBindingInfo(settingType,uiType);
                sharedBindingInfos[settingType] = info;
            }
            return info;
        }

        private SharedBindingInfo(Type settingType,Type uiType)
        {
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
            UIType = uiType ?? throw new ArgumentNullException(nameof(uiType));

            Makers = new AttributeBindingScopeCompiler(settingType,uiType).Analysis().ToArray();
            PropertyBoxes = DynamicTypePropertyHelper.EnumerablePropertyBoxs(settingType).ToArray();

            var makerMap = Makers.ToDictionary(x => x.DependencyProperty.Name);
            BindingPairMap = PropertyBoxes.ToDictionary(x => x.Name, x =>
            {
                makerMap.TryGetValue(x.Name, out var maker);
                return new BindingPair(maker, x);
            });
        }

        public Type SettingType { get; }

        public Type UIType { get; }

        public IReadOnlyList<IBindingMaker> Makers { get; }

        public IReadOnlyList<IPropertyBox> PropertyBoxes { get; }

        public IReadOnlyDictionary<string,BindingPair> BindingPairMap { get; }
    }
}
