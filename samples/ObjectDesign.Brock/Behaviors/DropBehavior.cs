using System.Windows;
using System.Windows.Interactivity;

namespace ObjectDesign.Brock.Behaviors
{
    public class DropBehavior : Behavior<UIElement>
    {
        public DropBox DropBox
        {
            get { return (DropBox)GetValue(DropBoxProperty); }
            set { SetValue(DropBoxProperty, value); }
        }

        public static readonly DependencyProperty DropBoxProperty =
            DependencyProperty.Register("DropBox", typeof(DropBox), typeof(DropBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject!=null)
            {
                AssociatedObject.Drop += OnAssociatedObjectDrop;
            }
        }

        private void OnAssociatedObjectDrop(object sender, DragEventArgs e)
        {
            DropBox = new DropBox(sender, e);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.Drop -= OnAssociatedObjectDrop;
            }
        }

    }
}
