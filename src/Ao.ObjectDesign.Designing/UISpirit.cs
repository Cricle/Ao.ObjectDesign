using System;

namespace Ao.ObjectDesign.Designing
{
    public class UISpirit<TView, TContext> : IUISpirit<TView, TContext>,IEquatable<UISpirit<TView, TContext>>
        where TView : class
        where TContext : class
    {
        public UISpirit(TView view, TContext context)
        {
            View = view;
            Context = context;
        }
        public UISpirit(TContext context)
            : this(null, context)
        {
        }

        public TView View { get; }

        public TContext Context { get; }

        public override bool Equals(object obj)
        {
            if (obj is UISpirit<TView, TContext> b)
            {
                return Equals(b);
            }
            return false;
        }
        public override string ToString()
        {
            return $"{{{View}, {Context}}}";
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var h = 17;
                var s = 0;
                if (View != null)
                {
                    s = View.GetHashCode();
                }
                h = h * 31 + s;
                if (Context != null)
                {
                    s = Context.GetHashCode();
                }
                h = h * 31 + s;
                return h;
            }
        }

        public bool Equals(UISpirit<TView, TContext> other)
        {
            if (other is null)
            {
                return false;
            }
            return other.Context == Context &&
                other.View == View;
        }
    }
}
