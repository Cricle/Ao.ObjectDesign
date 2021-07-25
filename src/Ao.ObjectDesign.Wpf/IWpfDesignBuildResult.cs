using System;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfDesignBuildResult
    {
        IActionSequencer<IModifyDetail> Sequencer { get; }

        IObjectDesigner Designer { get; }

        IDisposable Subjected { get; }
    }
}
