using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [UnfoldMapping]
    [DesignFor(typeof(Transform))]
    public class TransformDesigner : NotifyableObject
    {
    }
}
