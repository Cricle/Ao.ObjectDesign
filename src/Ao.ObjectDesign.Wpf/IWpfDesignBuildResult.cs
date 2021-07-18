using System;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfDesignBuildResult
    {
        INotifyableSequencer Sequencer { get; }

        IObjectDesigner Designer { get; }

        IDisposable Subjected { get; }
    }
}
