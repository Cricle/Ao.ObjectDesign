using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(TransformGroup))]
    public class TransformGroupDesigner:NotifyableObject
    {
        private TransformCollectionDesigner children = new TransformCollectionDesigner();

        public TransformCollectionDesigner Children
        {
            get => children;
            set
            {
                Set(ref children, value);
                RaiseChildrenChanged();
            }
        }

        private static readonly PropertyChangedEventArgs childrenChangedEventArgs = new PropertyChangedEventArgs(nameof(Children));

        public void RaiseChildrenChanged()
        {
            RaisePropertyChanged(childrenChangedEventArgs);
        }

        [PlatformTargetGetMethod]
        public virtual TransformGroup GetTransformGroup()
        {
            if (children == null || children.Count == 0)
            {
                return null;
            }
            return new TransformGroup { Children = children.GetTransformCollection() };
        }
        [PlatformTargetSetMethod]
        public virtual void SetTransformGroup(TransformGroup group)
        {
            if (group?.Children==null)
            {
                children = null;
            }
            else
            {
                var g = new TransformCollectionDesigner();
                g.SetTransformCollection(group.Children);
                Children = g;
            }
        }
    }
}
