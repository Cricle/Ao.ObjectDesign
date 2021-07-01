using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class DesigningObject : NotifyableObject
    {
        public DesigningObject()
        {
            locationSize = new LocationSize();
        }
        private string name;
        private string tag;
        private LocationSize locationSize;

        public LocationSize LocationSize
        {
            get => locationSize;
            set => Set(ref locationSize, value);
        }
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Tag
        {
            get => tag;
            set => Set(ref tag, value);
        }

    }
}
