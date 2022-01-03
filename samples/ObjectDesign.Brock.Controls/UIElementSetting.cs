﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ObjectDesign.Brock.Components
{
    
    public class UIElementSetting : NotifyableObject
    {
        private Visibility visibility;
        private bool clipToBounds;
        private bool isEnabled;
        private double opacity;
        private RotateTransformDesigner rotateTransform;
        private PointDesigner renderTransformOrigin;

        public PointDesigner RenderTransformOrigin
        {
            get => renderTransformOrigin;
            set => Set(ref renderTransformOrigin, value);
        }

        public RotateTransformDesigner RotateTransform
        {
            get => rotateTransform;
            set => Set(ref rotateTransform, value);
        }
        
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
            RotateTransform = new RotateTransformDesigner();
        }
    }
}