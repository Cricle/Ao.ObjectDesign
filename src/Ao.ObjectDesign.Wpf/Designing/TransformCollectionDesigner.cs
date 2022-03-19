using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(TransformCollection))]
    public class TransformCollectionDesigner : DynamicSilentObservableCollection<TransformDesigner>
    {
        private static readonly PropertyChangedEventArgs TransformCollectionChangedEventArgs = new PropertyChangedEventArgs(nameof(TransformGroup));
        public TransformCollectionDesigner()
        {
            CollectionChanged += OnDoubleCollectionSettingCollectionChanged;
        }

        private void OnDoubleCollectionSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(TransformCollectionChangedEventArgs);
        }
        [PlatformTargetGetMethod]
        public virtual TransformCollection GetTransformCollection()
        {
            return new TransformCollection(ToArray());
        }
        protected virtual Transform[] ToArray()
        {
            var t = new Transform[Count];
            for (int i = 0; i < Count; i++)
            {
                var item = this[i];
                if (item is TranslateTransformDesigner tf)
                {
                    t[i] = tf.GetTranslateTransform();
                }
                else if (item is RotateTransformDesigner rt)
                {
                    t[i] = rt.GetRotateTransform();
                }
                else if (item is SkewTransformDesigner st)
                {
                    t[i] = st.GetSkewTransform();
                }
                else if (item is ScaleTransformDesigner sct)
                {
                    t[i] = sct.GetScaleTransform();
                }
                else
                {
                    throw new NotSupportedException($"Not support {item.GetType()}");
                }
            }
            return t;
        }
        protected virtual TransformDesigner[] ToDesigner(IList<Transform> transforms)
        {
            var design = new TransformDesigner[transforms.Count];
            for (int i = 0; i < transforms.Count; i++)
            {
                var item = transforms[i];
                if (item is TranslateTransform tf)
                {
                    var v = new TranslateTransformDesigner();
                    v.SetTranslateTransform(tf);
                    design[i] = v;
                }
                else if (item is RotateTransform rf)
                {
                    var v = new RotateTransformDesigner();
                    v.SetRotateTransform(rf);
                    design[i] = v;
                }
                else if (item is SkewTransform st)
                {
                    var v = new SkewTransformDesigner();
                    v.SetSkewTransform(st);
                    design[i] = v;
                }
                else if (item is ScaleTransform sct)
                {
                    var v = new ScaleTransformDesigner();
                    v.SetScaleTransform(sct);
                    design[i] = v;
                }
                else
                {
                    throw new NotSupportedException($"No support {item.GetType()}");
                }
            }
            return design;
        }
        [PlatformTargetSetMethod]
        public virtual void SetTransformCollection(TransformCollection value)
        {
            Clear();
            if (value != null)
            {
                AddRange(ToDesigner(value));
            }
        }

    }
}
