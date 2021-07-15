using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public class CompiledSequencer : Sequencer
    {
        protected override void OnReset(IModifyDetail detail)
        {
            var instanceType = detail.Instance.GetType();
            var identity = new PropertyIdentity(instanceType, detail.PropertyName);
            var setter = CompiledPropertyInfo.GetSetter(identity);
            setter(detail.Instance, detail.From);
        }
    }
}
