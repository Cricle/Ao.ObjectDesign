using Ao.ObjectDesign.Designing;
using System;

namespace Ao.ObjectDesign
{
    public interface IWpfDesignBuildResult
    {
        IActionSequencer<IFallbackable> Sequencer { get; }

        IObjectDesigner Designer { get; }

        IDisposable Subjected { get; }
    }
}
