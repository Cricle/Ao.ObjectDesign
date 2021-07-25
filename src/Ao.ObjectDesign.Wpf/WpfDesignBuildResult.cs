using System;

namespace Ao.ObjectDesign.Wpf
{
    internal class WpfDesignBuildResult : IWpfDesignBuildResult
    {
        public IActionSequencer<IModifyDetail> Sequencer { get; set; }

        public IObjectDesigner Designer { get; set; }

        public IDisposable Subjected { get; set; }
    }
}
