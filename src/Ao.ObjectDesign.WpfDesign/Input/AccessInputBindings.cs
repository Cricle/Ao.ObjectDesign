using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public class AccessInputBindings : IDisposable, IAccessInputBindings
    {
        private bool listening;

        public bool Listening => listening;

        public IList<ITextInput> TextInputs { get; }

        public IList<IPreviewKeyboardInput> PreviewKeyboardInputs { get; }

        public IList<IKeyboardInput> KeyboardInputs { get; }

        public IList<IPreviewMouseInput> PreviewMouseInputs { get; }

        public IList<IMouseInput> MouseInputs { get; }

        public IList<IStylusInput> StylusInputs { get; }

        public IList<IPreviewStylusInput> PreviewStylusInputs { get; }

        public IInputElement InputElement { get; }

        public AccessInputBindings(IInputElement inputElement)
        {
            InputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));

            TextInputs = new List<ITextInput>();
            PreviewKeyboardInputs = new List<IPreviewKeyboardInput>();
            KeyboardInputs = new List<IKeyboardInput>();
            PreviewMouseInputs = new List<IPreviewMouseInput>();
            MouseInputs = new List<IMouseInput>();
            StylusInputs = new List<IStylusInput>();
            PreviewStylusInputs = new List<IPreviewStylusInput>();
        }
        public void Listen()
        {
            if (listening)
            {
                return;
            }
            listening = true;
            InputElement.MouseEnter += OnMouseEnter;
            InputElement.MouseMove += OnMouseMove;
            InputElement.MouseWheel += OnMouseWheel;
            InputElement.MouseLeave += OnMouseLeave;
            InputElement.MouseLeftButtonUp += OnMouseLeftButtonUp;
            InputElement.MouseLeftButtonDown += OnMouseLeftButtonDown;
            InputElement.MouseRightButtonDown += OnMouseRightButtonDown;
            InputElement.MouseRightButtonUp += OnMouseRightButtonUp;
            InputElement.GotMouseCapture += OnGotMouseCapture;
            InputElement.LostMouseCapture += OnLostMouseCapture;

            InputElement.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            InputElement.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
            InputElement.PreviewMouseMove += OnPreviewMouseMove;
            InputElement.PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
            InputElement.PreviewMouseRightButtonUp += OnPreviewMouseRightButtonUp;
            InputElement.PreviewMouseWheel += OnPreviewMouseWheel;

            InputElement.PreviewTextInput += OnPreviewTextInput;
            InputElement.TextInput += OnTextInput;

            InputElement.PreviewKeyDown += OnPreviewKeyDown;
            InputElement.PreviewKeyUp += OnPreviewKeyUp;
            InputElement.PreviewGotKeyboardFocus += OnPreviewGotKeyboardFocus;
            InputElement.PreviewLostKeyboardFocus += OnPreviewLostKeyboardFocus;

            InputElement.KeyDown += OnKeyDown;
            InputElement.KeyUp += OnKeyUp;
            InputElement.GotKeyboardFocus += OnGotKeyboardFocus;
            InputElement.LostKeyboardFocus += OnLostKeyboardFocus;

            InputElement.StylusButtonDown += OnStylusButtonDown;
            InputElement.StylusButtonUp += OnStylusButtonUp;
            InputElement.StylusDown += OnStylusDown;
            InputElement.StylusEnter += OnStylusEnter;
            InputElement.StylusInAirMove += OnStylusInAirMove;
            InputElement.StylusInRange += OnStylusInRange;
            InputElement.StylusLeave += OnStylusLeave;
            InputElement.StylusMove += OnStylusMove;
            InputElement.StylusOutOfRange += OnStylusOutOfRange;
            InputElement.StylusSystemGesture += OnStylusSystemGesture;
            InputElement.StylusUp += OnStylusUp;
            InputElement.GotStylusCapture += OnGotStylusCapture;
            InputElement.LostStylusCapture += OnLostStylusCapture;

            InputElement.PreviewStylusButtonDown += OnPreviewStylusButtonDown;
            InputElement.PreviewStylusButtonUp += OnPreviewStylusButtonUp;
            InputElement.PreviewStylusDown += OnPreviewStylusDown;
            InputElement.PreviewStylusInAirMove += OnPreviewStylusInAirMove;
            InputElement.PreviewStylusInRange += OnPreviewStylusInRange;
            InputElement.PreviewStylusMove += OnPreviewStylusMove;
            InputElement.PreviewStylusOutOfRange += OnPreviewStylusOutOfRange;
            InputElement.PreviewStylusSystemGesture += OnPreviewStylusSystemGesture;
            InputElement.PreviewStylusUp += OnPreviewStylusUp;
        }

        public void UnListen()
        {
            if (!listening)
            {
                return;
            }
            listening = false;
            InputElement.MouseEnter -= OnMouseEnter;
            InputElement.MouseMove -= OnMouseMove;
            InputElement.MouseWheel -= OnMouseWheel;
            InputElement.MouseLeave -= OnMouseLeave;
            InputElement.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            InputElement.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            InputElement.MouseRightButtonDown -= OnMouseRightButtonDown;
            InputElement.MouseRightButtonUp -= OnMouseRightButtonUp;
            InputElement.GotMouseCapture -= OnGotMouseCapture;
            InputElement.LostMouseCapture -= OnLostMouseCapture;

            InputElement.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            InputElement.PreviewMouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;
            InputElement.PreviewMouseMove -= OnPreviewMouseMove;
            InputElement.PreviewMouseRightButtonDown -= OnPreviewMouseRightButtonDown;
            InputElement.PreviewMouseRightButtonUp -= OnPreviewMouseRightButtonUp;
            InputElement.PreviewMouseWheel -= OnPreviewMouseWheel;

            InputElement.PreviewTextInput -= OnPreviewTextInput;
            InputElement.TextInput -= OnTextInput;

            InputElement.PreviewKeyDown -= OnPreviewKeyDown;
            InputElement.PreviewKeyUp -= OnPreviewKeyUp;
            InputElement.PreviewGotKeyboardFocus -= OnPreviewGotKeyboardFocus;
            InputElement.PreviewLostKeyboardFocus -= OnPreviewLostKeyboardFocus;

            InputElement.KeyDown -= OnKeyDown;
            InputElement.KeyUp -= OnKeyUp;
            InputElement.GotKeyboardFocus -= OnGotKeyboardFocus;
            InputElement.LostKeyboardFocus -= OnLostKeyboardFocus;

            InputElement.StylusButtonDown -= OnStylusButtonDown;
            InputElement.StylusButtonUp -= OnStylusButtonUp;
            InputElement.StylusDown -= OnStylusDown;
            InputElement.StylusEnter -= OnStylusEnter;
            InputElement.StylusInAirMove -= OnStylusInAirMove;
            InputElement.StylusInRange -= OnStylusInRange;
            InputElement.StylusLeave -= OnStylusLeave;
            InputElement.StylusMove -= OnStylusMove;
            InputElement.StylusOutOfRange -= OnStylusOutOfRange;
            InputElement.StylusSystemGesture -= OnStylusSystemGesture;
            InputElement.StylusUp -= OnStylusUp;
            InputElement.GotStylusCapture -= OnGotStylusCapture;
            InputElement.LostStylusCapture -= OnLostStylusCapture;

            InputElement.PreviewStylusButtonDown -= OnPreviewStylusButtonDown;
            InputElement.PreviewStylusButtonUp -= OnPreviewStylusButtonUp;
            InputElement.PreviewStylusDown -= OnPreviewStylusDown;
            InputElement.PreviewStylusInAirMove -= OnPreviewStylusInAirMove;
            InputElement.PreviewStylusInRange -= OnPreviewStylusInRange;
            InputElement.PreviewStylusMove -= OnPreviewStylusMove;
            InputElement.PreviewStylusOutOfRange -= OnPreviewStylusOutOfRange;
            InputElement.PreviewStylusSystemGesture -= OnPreviewStylusSystemGesture;
            InputElement.PreviewStylusUp -= OnPreviewStylusUp;
        }

        private void OnPreviewStylusUp(object sender, StylusEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusSystemGesture(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusOutOfRange(object sender, StylusEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusOutOfRange(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusMove(object sender, StylusEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusMove(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusInRange(object sender, StylusEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusInRange(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusInAirMove(object sender, StylusEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusInAirMove(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusButtonUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewStylusButtonDown(object sender, StylusButtonEventArgs e)
        {
            foreach (var item in PreviewStylusInputs)
            {
                item.OnPreviewStylusButtonDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            foreach (var item in PreviewKeyboardInputs)
            {
                item.OnPreviewLostKeyboardFocus(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            foreach (var item in KeyboardInputs)
            {
                item.OnGotKeyboardFocus(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            foreach (var item in KeyboardInputs)
            {
                item.OnGotKeyboardFocus(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            foreach (var item in PreviewKeyboardInputs)
            {
                item.OnPreviewGotKeyboardFocus(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnLostStylusCapture(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnLostStylusCapture(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnGotStylusCapture(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnGotStylusCapture(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusUp(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusSystemGesture(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusOutOfRange(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusOutOfRange(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusMove(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusMove(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusLeave(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusLeave(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusInRange(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusInRange(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusInAirMove(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusInAirMove(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusEnter(object sender, StylusEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusEnter(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusDown(object sender, StylusDownEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusButtonUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnStylusButtonDown(object sender, StylusButtonEventArgs e)
        {
            foreach (var item in StylusInputs)
            {
                item.OnStylusButtonDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnLostMouseCapture(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnGotMouseCapture(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var item in TextInputs)
            {
                item.OnPreviewTextInput(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var item in TextInputs)
            {
                item.OnTextInput(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            foreach (var item in PreviewMouseInputs)
            {
                item.OnPreviewMouseWheel(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in PreviewMouseInputs)
            {
                item.OnPreviewMouseRightButtonUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in PreviewMouseInputs)
            {
                item.OnPreviewMouseRightButtonDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in PreviewMouseInputs)
            {
                item.OnPreviewMouseMove(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in PreviewMouseInputs)
            {
                item.OnPreviewMouseLeftButtonUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in PreviewMouseInputs)
            {
                item.OnPreviewMouseLeftButtonDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var item in KeyboardInputs)
            {
                item.OnKeyUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var item in KeyboardInputs)
            {
                item.OnKeyDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var item in PreviewKeyboardInputs)
            {
                item.OnPreviewKeyUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var item in PreviewKeyboardInputs)
            {
                item.OnPreviewKeyDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseRightButtonUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseRightButtonDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseLeftButtonDown(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseLeftButtonUp(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseLeave(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseWheel(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseMove(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            foreach (var item in MouseInputs)
            {
                item.OnMouseEnter(sender, e);
                if (e.Handled)
                {
                    break;
                }
            }

        }

        public virtual void Dispose()
        {
            UnListen();
        }
    }
}
