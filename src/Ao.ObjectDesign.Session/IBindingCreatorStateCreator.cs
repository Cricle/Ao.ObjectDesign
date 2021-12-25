using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public interface IBindingCreatorStateCreator<TSetting>
    {
        IBindingCreatorState GetBindingCreatorState(IDesignPair<UIElement, TSetting> unit);
    }
}
