using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using System;

namespace Ao.ObjectDesign.Session.Wpf
{
    public static class BindingCreatorStateCreatorAsExtensions
    {
        public static BindingCreatorStateCreator<TScene, TDeisgnObject> AsImpl<TScene, TDeisgnObject>(this IBindingCreatorStateCreator<TDeisgnObject> creator)
            where TScene : IDesignScene<TDeisgnObject>
        {
            var res = creator as BindingCreatorStateCreator<TScene, TDeisgnObject>;
            if (res is null)
            {
                throw new InvalidCastException($"Can't case{creator} to {typeof(BindingCreatorStateCreator<TScene, TDeisgnObject>)}");
            }
            return res;
        }
    }
}
