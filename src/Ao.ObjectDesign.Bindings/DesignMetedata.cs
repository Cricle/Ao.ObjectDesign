using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class DesignMetedata<TUI, TContext> : IDesignMetedata<TUI, TContext>
    {
        public DesignMetedata(IDesignContext<TUI, TContext> designContext, TUI target, TUI parent)
        {
            DesignContext = designContext;
            Target = target;
            Parent = parent;
        }

        public IDesignContext<TUI, TContext> DesignContext { get; }

        public TUI Target { get; }

        public TUI Parent { get; }

        public abstract TUI Container { get; }

        public abstract bool IsContainerCanvas { get; }


        public abstract IVector InCanvasPosition { get; }

        public virtual IVector TargetOffset
        {
            get
            {
                if (IsContainerCanvas)
                {
                    return InCanvasPosition;
                }
                return IgnoreCanvasTargetOffset;
            }
        }

        public virtual IVector IgnoreCanvasTargetOffset => GetPosition(Target);

        public virtual IRect TargetBounds
        {
            get
            {
                var offset = TargetOffset;
                var size = GetSize(Target);
                return new DefaultRect(offset.X, offset.Y, size.X + offset.X, size.Y + offset.Y);
            }
        }
        public virtual IVector ParentOffset => GetPosition(Parent);

        public virtual IRect ParentBounds
        {
            get
            {
                var offset = ParentOffset;
                var size = GetSize(Parent);
                return new DefaultRect(offset.X, offset.Y, size.X + offset.X, size.Y + offset.Y);
            }
        }

        public virtual IVector ContainerOffset => GetPosition(Container);

        public virtual IRect ContainerBounds
        {
            get
            {
                var offset = ContainerOffset;
                var size = GetSize(Container);
                return new DefaultRect(offset.X, offset.Y, size.X + offset.X, size.Y + offset.Y);
            }
        }

        public abstract IVector GetPosition(TUI element);

        public abstract IVector GetSize(TUI element);

        public abstract IVector GetInCanvaPosition(TUI element);
    }
}
