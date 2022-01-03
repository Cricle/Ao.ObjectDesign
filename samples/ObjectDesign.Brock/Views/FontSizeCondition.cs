using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Controls;
using System.Collections.Generic;

namespace ObjectDesign.Brock.Views
{
    public class FontSizeCondition : TemplateCondition
    {
        public static readonly IReadOnlyCollection<double> GivenFontSizes = GetFontSizes();

        private static double[] GetFontSizes()
        {
            var ds = new double[101];
            ds[0] = 1;
            for (int i = 1; i < ds.Length; i++)
            {
                ds[i] = i;
            }
            return ds;
        }
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return (context.PropertyProxy.DeclaringInstance is ControlSetting&&
                context.PropertyProxy.PropertyInfo.Name==nameof(ControlSetting.FontSize))||
                (context.PropertyProxy.DeclaringInstance is TextBlockSetting &&
                context.PropertyProxy.PropertyInfo.Name == nameof(TextBlockSetting.FontSize));
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.FontSize";
        }
    }
}
