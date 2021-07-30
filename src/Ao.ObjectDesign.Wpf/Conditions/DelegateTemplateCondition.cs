using System;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public class DelegateTemplateCondition<T> : TemplateCondition<T>
    {
        public DelegateTemplateCondition(string resourceKey)
        {
            if (string.IsNullOrEmpty(resourceKey))
            {
                throw new ArgumentException($"“{nameof(resourceKey)}”不能为 null 或空。", nameof(resourceKey));
            }

            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; }

        protected override string GetResourceKey()
        {
            return ResourceKey;
        }
    }
    public class DelegateTemplateCondition : TemplateCondition
    {
        public DelegateTemplateCondition(Func<WpfTemplateForViewBuildContext, bool> canBuildDelegate,
            string resourceKey)
        {
            if (string.IsNullOrEmpty(resourceKey))
            {
                throw new ArgumentException($"“{nameof(resourceKey)}”不能为 null 或空。", nameof(resourceKey));
            }

            CanBuildDelegate = canBuildDelegate ?? throw new ArgumentNullException(nameof(canBuildDelegate));
            ResourceKey = resourceKey;
        }

        public Func<WpfTemplateForViewBuildContext, bool> CanBuildDelegate { get; }

        public string ResourceKey { get; }

        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return CanBuildDelegate(context);
        }

        protected override string GetResourceKey()
        {
            return ResourceKey;
        }
    }

}
