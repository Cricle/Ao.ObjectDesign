using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.ComponentModel;
using System.Windows;

namespace ObjectDesign.Brock.Components
{
    
    public class FrameworkElementSetting : UIElementSetting
    {
        private bool useLayoutRounding;
        private PositionSize positionSize;
        private HorizontalAlignment horizontalAlignment;
        private VerticalAlignment verticalAlignment;
        private string name;

        
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        
        public VerticalAlignment VerticalAlignment
        {
            get => verticalAlignment;
            set => Set(ref verticalAlignment, value);
        }
        
        public HorizontalAlignment HorizontalAlignment
        {
            get => horizontalAlignment;
            set => Set(ref horizontalAlignment, value);
        }
        
        public bool UseLayoutRounding
        {
            get => useLayoutRounding;
            set
            {
                Set(ref useLayoutRounding, value);
            }
        }
        public PositionSize PositionSize
        {
            get => positionSize;
            set => Set(ref positionSize, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            UseLayoutRounding = false;
            Name = null;
            PositionSize = new PositionSize();
            PositionSize.SetDefault();
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
        }
    }
}
