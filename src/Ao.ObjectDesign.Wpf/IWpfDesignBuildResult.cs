using Ao.ObjectDesign.Designing;
using System;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfDesignBuildResult
    {
        IActionSequencer<IFallbackable> Sequencer { get; }

        IObjectDesigner Designer { get; }

        IDisposable Subjected { get; }
    }
}
