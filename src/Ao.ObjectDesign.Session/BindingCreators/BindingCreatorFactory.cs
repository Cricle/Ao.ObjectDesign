using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Session.BindingCreators
{
    public abstract class BindingCreatorFactory<TSetting> : IBindingCreatorFactory<TSetting>
    {
        public virtual int Order { get; set; }

        public abstract IEnumerable<IBindingCreator<TSetting>> Create(IDesignPair<UIElement, TSetting> unit, IBindingCreatorState state);

        public abstract bool IsAccept(IDesignPair<UIElement, TSetting> unit, IBindingCreatorState state);
    }

}
