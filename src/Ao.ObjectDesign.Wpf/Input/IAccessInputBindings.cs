using System;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Input
{
    public interface IAccessInputBindings : IDisposable
    {
        IInputElement InputElement { get; }
        IList<IKeyboardInput> KeyboardInputs { get; }
        bool Listening { get; }
        IList<IMouseInput> MouseInputs { get; }
        IList<IPreviewKeyboardInput> PreviewKeyboardInputs { get; }
        IList<IPreviewMouseInput> PreviewMouseInputs { get; }
        IList<IPreviewStylusInput> PreviewStylusInputs { get; }
        IList<IStylusInput> StylusInputs { get; }
        IList<ITextInput> TextInputs { get; }

        void Listen();
        void UnListen();
    }
}