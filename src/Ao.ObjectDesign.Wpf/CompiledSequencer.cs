using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public class CompiledSequencer : Sequencer
    {
        protected override void OnReset(IModifyDetail detail)
        {
            Type instanceType = detail.Instance.GetType();
            PropertyIdentity identity = new PropertyIdentity(instanceType, detail.PropertyName);
            PropertySetter setter = CompiledPropertyInfo.GetSetter(identity);
            setter(detail.Instance, detail.From);
        }
    }
}
