using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Events;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IDesignSessionInfo<TScene, TSetting> : IDesignSessionWorking, IDesignSessionDesignable<TScene, TSetting>, IDesignSessionFallbackable, IDisposable
           where TScene : IDesignScene<TSetting>
    {
        Guid Id { get; }

        bool IsDisposed { get; }
    }
}
