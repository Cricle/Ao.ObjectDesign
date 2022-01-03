using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls.Designing
{
    
    public class ItemsPanelTemplateDesigning : NotifyableObject
    {
        private static readonly string StackPanelOrientationKey = "ObjectDesign.Brock.Controls.Designing.ItemsPanelTemplateDesigning.StackPanelOrientation";
        private ItemsPanelTemplateTypes type= ItemsPanelTemplateTypes.HorizontalStackPanel;

        public ItemsPanelTemplateTypes Type
        {
            get => type;
            set
            {
                Set(ref type, value);
                RaiseItemsPanelTemplateChanged();
            }
        }

        
        public ItemsPanelTemplate ItemsPanelTemplate
        {
            get => CreatePanelTemplate(type);
            set
            {
                if (value is null)
                {
                    Type = ItemsPanelTemplateTypes.Unknow;
                }
                else if (value.VisualTree.Type == typeof(Canvas))
                {
                    Type = ItemsPanelTemplateTypes.Canvas;
                }
                else if (value.VisualTree.Type == typeof(Grid))
                {
                    Type = ItemsPanelTemplateTypes.Grid;
                }
                else if (value.VisualTree.Type == typeof(StackPanel))
                {
                    if (value.Resources.Keys.OfType<string>().Any(x => x == StackPanelOrientationKey) &&
                        value.Resources[StackPanelOrientationKey] is Orientation or)
                    {
                        if (or == Orientation.Horizontal)
                        {
                            Type = ItemsPanelTemplateTypes.HorizontalStackPanel;
                        }
                        else
                        {
                            Type = ItemsPanelTemplateTypes.VerticalStackPanel;
                        }
                    }
                    else
                    {
                        Type = ItemsPanelTemplateTypes.HorizontalStackPanel;
                    }
                }
                else
                {
                    Type = ItemsPanelTemplateTypes.Unknow;
                }
            }
        }
        private static readonly PropertyChangedEventArgs ItemsPanelTemplateEventArgs = new PropertyChangedEventArgs(nameof(ItemsPanelTemplate));
        private void RaiseItemsPanelTemplateChanged()
        {
            RaisePropertyChanged(ItemsPanelTemplateEventArgs);
        }
        public ItemsPanelTemplate CreatePanelTemplate(ItemsPanelTemplateTypes type)
        {
            var ipt = new ItemsPanelTemplate();
            Type t = null;
            switch (type)
            {
                case ItemsPanelTemplateTypes.HorizontalStackPanel:
                case ItemsPanelTemplateTypes.VerticalStackPanel:
                    t = typeof(StackPanel);
                    break;
                default:
                case ItemsPanelTemplateTypes.Unknow:
                case ItemsPanelTemplateTypes.Grid:
                    t = typeof(Grid);
                    break;
                case ItemsPanelTemplateTypes.Canvas:
                    t = typeof(Canvas);
                    break;
            }
            ipt.VisualTree = new FrameworkElementFactory(t);
            switch (type)
            {
                case ItemsPanelTemplateTypes.HorizontalStackPanel:
                    ipt.VisualTree.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                    ipt.Resources.Add(StackPanelOrientationKey, Orientation.Horizontal);
                    break;
                case ItemsPanelTemplateTypes.VerticalStackPanel:
                    ipt.VisualTree.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);
                    ipt.Resources.Add(StackPanelOrientationKey, Orientation.Vertical);
                    break;
                default:
                    break;
            }
            return ipt;
        }

    }
}
