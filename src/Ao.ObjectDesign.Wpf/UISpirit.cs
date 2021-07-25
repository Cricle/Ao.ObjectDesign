using System;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class UISpirit<TView, TContext> : IUISpirit<TView, TContext>
        where TView : class
        where TContext : class
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
                if (View == null && b.View == null &&
                    Context == null && b.Context == null)
                {
                    return true;
                }
                if (View == null && b.View != null ||
                    View != null && b.View == null)
                {
                    return false;
                }
                if (Context == null && b.Context != null ||
                    Context != null && b.Context == null)
                {
                    return false;
                }
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
            return View.GetHashCode() ^ Context.GetHashCode();
        }
    }
}
