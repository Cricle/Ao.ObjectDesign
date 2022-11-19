using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Designing;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(Panel))]
    public abstract class PanelSetting : FrameworkElementSetting, IObservableDesignScene<UIElementSetting>
    {
        public PanelSetting()
        {
        }
        private BrushDesigner background;

        public BrushDesigner Background
        {
            get => background;
            set
            {
                Set(ref background, value);
            }
        }
        public override void SetDefault()
        {
            base.SetDefault();
            Background = new BrushDesigner { Type = PenBrushTypes.None };
        }
        private SilentObservableCollection<UIElementSetting> designingObjects;

        public SilentObservableCollection<UIElementSetting> DesigningObjects
        {
            get
            {
                if (designingObjects == null)
                {
                    designingObjects = new SilentObservableCollection<UIElementSetting>();
                }
                return designingObjects;
            }
            set
            {
                if (designingObjects != null)
                {
                    throw new InvalidOperationException("Property DesigningObjects is once set property");
                }
                designingObjects = value;
            }

        }

        IList<UIElementSetting> IDesignScene<UIElementSetting>.DesigningObjects => DesigningObjects;
    }
}
