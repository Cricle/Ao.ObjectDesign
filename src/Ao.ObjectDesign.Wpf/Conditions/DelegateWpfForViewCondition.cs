using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public class DelegateWpfForViewCondition : WpfForViewCondition
    {
        public DelegateWpfForViewCondition(Func<WpfForViewBuildContext, bool> canBuildDelegate,
            Action<WpfForViewBuildContext, FrameworkElement, Binding> bindDelegate,
            Func<WpfForViewBuildContext, FrameworkElement> createViewDelegate)
        {
            CanBuildDelegate = canBuildDelegate ?? throw new ArgumentNullException(nameof(canBuildDelegate));
            BindDelegate = bindDelegate ?? throw new ArgumentNullException(nameof(bindDelegate));
            CreateViewDelegate = createViewDelegate ?? throw new ArgumentNullException(nameof(createViewDelegate));
        }

        public Func<WpfForViewBuildContext, bool> CanBuildDelegate { get; }

        public Action<WpfForViewBuildContext, FrameworkElement, Binding> BindDelegate { get; }

        public Func<WpfForViewBuildContext, FrameworkElement> CreateViewDelegate { get; }

        public override bool CanBuild(WpfForViewBuildContext context)
        {
            return CanBuildDelegate(context);
        }

        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            BindDelegate(context, e, binding);
        }

        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            return CreateViewDelegate(context);
        }
    }
}
