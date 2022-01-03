using System.Windows;

namespace ObjectDesign.Brock.Behaviors
{
    public class DropBox
    {
        public DropBox(object sender, DragEventArgs args)
        {
            Sender = sender;
            Args = args;
        }

        public object Sender { get; }

        public DragEventArgs Args { get; }
    }
}
