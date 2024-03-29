﻿using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Data;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.AutoBind
{
    public partial class AutoBindingCreator<TUI,TSetting, TDesignObject>
    {
        public static WpfSharedBindingInfo BindingInfo { get; } = GetBindingInfo();

        private static WpfSharedBindingInfo GetBindingInfo()
        {
            return WpfSharedBindingInfo.GetBindingInfo(typeof(TUI),typeof(TDesignObject));
        }

        public static readonly IReadOnlyList<IBindingScope> TwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged).ToArray();
        public static readonly IReadOnlyList<IBindingScope> OneWayScopes =
            CreateBindingScope(BindingMode.OneWay, UpdateSourceTrigger.PropertyChanged).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            foreach (var item in BindingInfo.Makers)
            {
                yield return item.AddSetConfig(mode, trigger).Build();
            }
        }

        public void SetSettingToUI(TSetting obj, DependencyObject ui)
        {
            foreach (var item in BindingInfo.BindingPairMap.Values)
            {
                SetValue(obj, ui, item);
            }
        }
        protected virtual void SetValue(TSetting obj, DependencyObject ui, BindingPair<IBindingMaker> pair)
        {
            if (pair.Maker != null && pair.Box.Getter != null)
            {
                if (pair.DesignForBox != null)
                {
                    ui.SetValue(pair.Maker.DependencyProperty, pair.DesignForBox.Getter(pair.Box.Getter(obj)));
                }
                else
                {
                    ui.SetValue(pair.Maker.DependencyProperty, pair.Box.Getter(obj));
                }
            }
            else
            {
                var prop = GetProperty(ui.GetType(), pair.Box.Name);
                if (prop != null)
                {
                    ui.SetValue(pair.Maker.DependencyProperty, pair.Box.Getter(obj));
                }
            }
        }
        protected DependencyProperty GetProperty(Type uiType,string propertyName)
        {
            return DependencyObjectHelper.GetDependencyProperties(uiType)
                .FirstOrDefault(x => x.Name == propertyName);
        }
    }
}
