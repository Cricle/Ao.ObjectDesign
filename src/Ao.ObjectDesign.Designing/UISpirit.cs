using System;
using System.Windows;

namespace Ao.ObjectDesign.Designing
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
                var h1 = GetHashCode();
                var h2 = b.GetHashCode();
                return h1 == h2;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{{{View}, {Context}}}";
        }
        public override int GetHashCode()
        {
            var h = 0;
            if (View!=null)
            {
                h = View.GetHashCode();
            }
            if (Context!=null)
            {
                h ^= Context.GetHashCode();
            }
            return h;
        }
    }
}
