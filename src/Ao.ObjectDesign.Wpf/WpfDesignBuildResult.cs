using Ao.ObjectDesign.Designing;
using System;

namespace Ao.ObjectDesign
{
    internal class WpfDesignBuildResult : IWpfDesignBuildResult
    {
        public IActionSequencer<IFallbackable> Sequencer { get; set; }

        public IObjectDesigner Designer { get; set; }

        public IDisposable Subjected { get; set; }
    }
}
