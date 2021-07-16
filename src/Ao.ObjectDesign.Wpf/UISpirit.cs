using System;

namespace Ao.ObjectDesign.Wpf
{
    public class UISpirit<TView, TContext> : IUISpirit<TView, TContext>
    {
        public UISpirit(TView view, TContext context)
        {
            View = view;
            Context = context;
        }
        public UISpirit(TContext context)
            : this(default, context)
        {
        }

        public TView View { get; }

        public TContext Context { get; }

        public override bool Equals(object obj)
        {
            if (obj is UISpirit<TView, TContext> b)
            {
                return View.Equals(b.View) &&
                    Context.Equals(b.Context);
            }
            return false;
        }
        public override string ToString()
        {
            return $"{{{View}, {Context}}}";
        }
        public override int GetHashCode()
        {
#if NET5_0
            return HashCode.Combine(View, Context);
#else
            return View.GetHashCode() ^ Context.GetHashCode();
#endif
        }
    }
}
