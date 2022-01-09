using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [UnfoldMapping]
    [DesignFor(typeof(Transform))]
    public class TransformDesigner : NotifyableObject
    {
        private ScaleTransformDesigner scaleTransform;
        private RotateTransformDesigner rotateTransform;
        private TranslateTransformDesigner translateTransform;
        private SkewTransformDesigner skewTransform;
        private TransformTypes transformType;

        [DefaultValue(TransformTypes.None)]
        public virtual TransformTypes TransformType
        {
            get => transformType;
            set
            {
                Set(ref transformType, value);
                RaiseTransformChanged();
            }
        }

        public virtual SkewTransformDesigner SkewTransform
        {
            get => skewTransform;
            set
            {
                Set(ref skewTransform, value);
                RaiseTransformChanged();
            }
        }

        public virtual TranslateTransformDesigner TranslateTransform
        {
            get => translateTransform;
            set
            {
                Set(ref translateTransform, value);
                RaiseTransformChanged();
            }
        }

        public virtual ScaleTransformDesigner ScaleTransform
        {
            get => scaleTransform;
            set
            {
                Set(ref scaleTransform, value);
                RaiseTransformChanged();
            }
        }
        public virtual RotateTransformDesigner RotateTransform
        {
            get => rotateTransform;
            set
            {
                Set(ref rotateTransform, value);
                RaiseTransformChanged();
            }
        }
        [PlatformTargetGetMethod]
        [return: ProvideMulityValues]
        [return: PropertyProvideValue(nameof(SkewTransform), typeof(SkewTransform))]
        [return: PropertyProvideValue(nameof(ScaleTransform), typeof(ScaleTransform))]
        [return: PropertyProvideValue(nameof(RotateTransform), typeof(RotateTransform))]
        [return: PropertyProvideValue(nameof(TranslateTransform), typeof(TranslateTransform))]
        public virtual Transform GetTransform()
        {
            if (transformType == TransformTypes.None)
            {
                return null;
            }
            TransformGroup transformGroup = new TransformGroup();
            if ((transformType & TransformTypes.Rotate) != 0 &&
                rotateTransform != null)
            {
                transformGroup.Children.Add(rotateTransform.GetRotateTransform());
            }
            if ((transformType & TransformTypes.Translate) != 0 &&
                translateTransform != null)
            {
                transformGroup.Children.Add(translateTransform.GetTranslateTransform());
            }
            if ((transformType & TransformTypes.Skew) != 0 &&
                skewTransform != null)
            {
                transformGroup.Children.Add(skewTransform.GetSkewTransform());
            }
            if ((transformType & TransformTypes.Scale) != 0 &&
               scaleTransform != null)
            {
                transformGroup.Children.Add(scaleTransform.GetScaleTransform());
            }
            return transformGroup;
        }
        [PlatformTargetSetMethod]
        public virtual void SetTransform(Transform value)
        {
            if (value is null)
            {
                TransformType = TransformTypes.None;
            }
            else
            {
                TransformTypes FindSet(Transform item)
                {
                    if (item is TranslateTransform translate)
                    {
                        TranslateTransform = new TranslateTransformDesigner();
                        TranslateTransform.SetTranslateTransform(translate);
                        return TransformTypes.Translate;
                    }
                    else if (item is RotateTransform rotate)
                    {
                        RotateTransform = new RotateTransformDesigner();
                        RotateTransform.SetRotateTransform(rotate);
                        return TransformTypes.Rotate;
                    }
                    else if (item is SkewTransform skew)
                    {
                        SkewTransform = new SkewTransformDesigner();
                        SkewTransform.SetSkewTransform(skew);
                        return TransformTypes.Skew;
                    }
                    else if (item is ScaleTransform scale)
                    {
                        ScaleTransform = new ScaleTransformDesigner();
                        ScaleTransform.SetScaleTransform(scale);
                        return TransformTypes.Scale;
                    }
                    return TransformTypes.None;
                }
                if (value is TransformGroup group)
                {
                    TransformTypes type = TransformTypes.None;
                    foreach (Transform item in group.Children)
                    {
                        type |= FindSet(item);
                    }
                    TransformType = type;
                }
                else
                {
                    TransformType = FindSet(value);
                }
            }
        }
        private static readonly PropertyChangedEventArgs transformEventArgs = new PropertyChangedEventArgs(nameof(Transform));

        protected void RaiseTransformChanged()
        {
            RaisePropertyChanged(transformEventArgs);
        }
    }
}
