using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Benchmark
{
    internal static class SerializeHelper
    {
        public static List<NotifyableObject> MakeDatas(int count)
        {
            var brushes = new List<NotifyableObject>();
            for (int i = 0; i < count; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        var bs = new BrushDesigner
                        {
                            ImageBrushDesigner = new ImageBrushDesigner(),
                            LinearGradientBrushDesigner = new LinearGradientBrushDesigner(),
                            RadialGradientBrushDesigner = new RadialGradientBrushDesigner(),
                            SolidColorBrushDesigner = new SolidColorBrushDesigner()
                        };
                        brushes.Add(bs);
                        break;
                    case 1:
                        brushes.Add(new BindingDesigner());
                        break;
                    case 2:
                        brushes.Add(new ColorDesigner());
                        break;
                    case 3:
                        brushes.Add(new GradientStopDesigner());
                        break;
                    default:
                        break;
                }
            }
            return brushes;
        }
    }
}
