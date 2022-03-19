using Ao.ObjectDesign.Bindings.Annotations;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock.Components
{
    [MappingFor(typeof(UIElement))]
    [BindingCreator(typeof(UIElement),typeof(UIElementSetting))]
    public class UIElementSetting : NotifyableObject
    {
        private Visibility visibility;
        private bool clipToBounds;
        private bool isEnabled;
        private double opacity;
        //private TransformGroupDesigner renderTransform;
        private PointDesigner renderTransformOrigin;
        private RotateTransformDesigner rotateTransformDesigner;

        public RotateTransformDesigner RotateTransformDesigner
        {
            get => rotateTransformDesigner;
            set
            {
                Set(ref rotateTransformDesigner, value);
            }
        }

        public PointDesigner RenderTransformOrigin
        {
            get => renderTransformOrigin;
            set => Set(ref renderTransformOrigin, value);
        }

        //public TransformGroupDesigner RenderTransform
        //{
        //    get => renderTransform;
        //    set
        //    {
        //        Set(ref renderTransform, value);
        //    }
        //}

        public double Opacity
        {
            get => opacity;
            set
            {
                Set(ref opacity, value);
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                Set(ref isEnabled, value);
            }
        }


        public bool ClipToBounds
        {
            get => clipToBounds;
            set
            {
                Set(ref clipToBounds, value);
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                Set(ref visibility, value);
            }
        }
        public virtual void SetDefault()
        {
            IsEnabled = true;
            ClipToBounds = false;
            Opacity = 1;
            Visibility = Visibility.Visible;
            RenderTransformOrigin = new PointDesigner
            {
                X = 0.5,
                Y = 0.5
            };
            RotateTransformDesigner = new RotateTransformDesigner();
        }
    }
}
