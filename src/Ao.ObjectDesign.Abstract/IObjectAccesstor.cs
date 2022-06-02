using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public interface IObjectAccesstor
    {
        object Instance { get; }

        IEnumerable<string> Names { get; }

        bool HasName(string name);

        object GetValue(string name);

        void SetValue(string name, object value);
    }
}
