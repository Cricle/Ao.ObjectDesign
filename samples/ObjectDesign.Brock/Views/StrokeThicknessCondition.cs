
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Controls;
using System.Collections.Generic;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class StrokeThicknessCondition : TemplateCondition
    {
        public static readonly IReadOnlyCollection<double> GivenFontStrokeThicknesses = GetFontStrokeThicknesses();

        private static double[] GetFontStrokeThicknesses()
        {
            var ds = new double[20];
            ds[0] = 1;
            for (int i = 1; i < ds.Length; i++)
            {
                ds[i] = 2 + i * 2;
            }
            return ds;
        }

        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.PropertyInfo.Name == nameof(ShapeSetting.StrokeThickness) &&
                context.PropertyProxy.DeclaringInstance is ShapeSetting;
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.StrokeThickness";
        }
    }
}
