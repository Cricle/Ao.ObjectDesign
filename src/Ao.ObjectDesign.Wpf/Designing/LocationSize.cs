using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class LocationSize : NotifyableObject
    {
        private double left;
        private double top;
        private double width = 50;
        private double height = 50;

        public virtual double Left
        {
            get => left;
            set => Set(ref left, value);
        }
        public virtual double Top
        {
            get => top;
            set => Set(ref top, value);
        }
        public virtual double Height
        {
            get => height;
            set => Set(ref height, value);
        }
        public virtual double Width
        {
            get => width;
            set => Set(ref width, value);
        }
    }
}
