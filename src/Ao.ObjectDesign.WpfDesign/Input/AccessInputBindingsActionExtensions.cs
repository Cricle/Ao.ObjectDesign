namespace Ao.ObjectDesign.WpfDesign.Input
{
    public static class AccessInputBindingsActionExtensions
    {
        public static bool Remove(this IAccessInputBindings inputBindings, ITextInput input)
        {
            return inputBindings.TextInputs.Remove(input);
        }
        public static bool Remove(this IAccessInputBindings inputBindings, IPreviewKeyboardInput input)
        {
            return inputBindings.PreviewKeyboardInputs.Remove(input);
        }
        public static bool Remove(this IAccessInputBindings inputBindings, IKeyboardInput input)
        {
            return inputBindings.KeyboardInputs.Remove(input);
        }
        public static bool Remove(this IAccessInputBindings inputBindings, IPreviewMouseInput input)
        {
            return inputBindings.PreviewMouseInputs.Remove(input);
        }
        public static bool Remove(this IAccessInputBindings inputBindings, IMouseInput input)
        {
            return inputBindings.MouseInputs.Remove(input);
        }
        public static void Add(this IAccessInputBindings inputBindings, ITextInput input)
        {
            inputBindings.TextInputs.Add(input);
        }
        public static void Add(this IAccessInputBindings inputBindings, IPreviewKeyboardInput input)
        {
            inputBindings.PreviewKeyboardInputs.Add(input);
        }
        public static void Add(this IAccessInputBindings inputBindings, IKeyboardInput input)
        {
            inputBindings.KeyboardInputs.Add(input);
        }
        public static void Add(this IAccessInputBindings inputBindings, IPreviewMouseInput input)
        {
            inputBindings.PreviewMouseInputs.Add(input);
        }
        public static void Add(this IAccessInputBindings inputBindings, IMouseInput input)
        {
            inputBindings.MouseInputs.Add(input);
        }

    }
}