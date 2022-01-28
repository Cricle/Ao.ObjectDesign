using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    internal class TypePropertyBoxInfo
    {
        public Dictionary<string, PropertyBox> Box;

        public Dictionary<string, PropertyBox> VirtualBox;

        public PropertyBox FirstVirtual;
    }
}
