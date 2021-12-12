using Ao.ObjectDesign;
using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.Wpf.Json;
using Ao.ObjectDesign.Wpf.Xaml;
using Ao.ObjectDesign.Wpf.Yaml;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ObjectDesign.Wpf.Controls;
using ObjectDesign.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;
using System.Xml;

namespace ObjectDesign.Wpf
{
    public enum GenerateMode
    {
        Direct,
        DataTemplate
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static readonly HashSet<Type> ignoreTypes = new HashSet<Type>
        {
            typeof(Window),typeof(ToolTip)
        };
        private WpfObjectDesigner wpfObjectDesigner;
        private List<DesignMapping> settingTypes;
        private ForViewDataTemplateSelector forViewDataTemplateSelector;
        private void Init()
        {
            wpfObjectDesigner = new WpfObjectDesigner(true);
            wpfObjectDesigner.UIBuilder.AddWpfCondition();
            wpfObjectDesigner.Add(new DateTimeBrushForViewCondition());
            settingTypes = KnowControlTypes.SettingTypes
                .Where(x => !x.ClrType.IsAbstract && !x.UIType.IsAbstract && !ignoreTypes.Contains(x.UIType))
                .ToList();
            settingTypes.Add(DesignMapping.FromMapping(typeof(MoveIdSetting)));
            var coditions = new IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>[]
            {
                new CornerDesignCondition(),
                new EnumCondition(),
                new FontSettingCondition(),
                new LocationSizeCondition(),
                new PenBrushCondition(),
                new PrimitiveCondition(),
                new FontFamilyCondition()
            };

            wpfObjectDesigner.AddRange(coditions);

            forViewDataTemplateSelector = wpfObjectDesigner.CreateTemplateSelector();
            sequencer = (PropertySequencer)wpfObjectDesigner.Sequencer;
            Design.ItemTemplateSelector = forViewDataTemplateSelector;
        }
        public MainWindow()
        {
            InitializeComponent();

            Init();
            Providing.ItemsSource = settingTypes;
            Design.ItemsSource = designs;
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                if (e.Key == Key.Z)
                {
                    sequencer.Undo();
                }
                else if (e.Key == Key.V)
                {
                    sequencer.Redo();
                }
                else if (e.Key == Key.J)
                {
                    Type t = currentObject.GetType();
                    string str = DesignJsonHelper.Serialize(currentObject);
                    File.WriteAllText(t.Name + ".json", str);
                    this.ShowMessageAsync(string.Empty, "保存为json成功");
                }
                else if (e.Key == Key.B)
                {
                    Type t = currentObject.GetType();
                    using (FileStream fs = File.Open(t.Name + ".bson", FileMode.Create))
                    {
                        DesignBsonHelper.Serialize(fs, currentObject, t);
                    }
                    this.ShowMessageAsync(string.Empty, "保存为bson成功");
                }
                else if (e.Key == Key.Y)
                {
                    Type t = currentObject.GetType();
                    string str = DeisgnYamlSerializer.Serialize(currentObject);
                    File.WriteAllText(t.Name + ".yaml", str);
                    this.ShowMessageAsync(string.Empty, "保存为yaml成功");
                }
                else if (e.Key == Key.X)
                {
                    Type t = currentObject.GetType();
                    string s = DeisgnXamlSerializer.Serialize(currentObject);
                    File.WriteAllText(t.Name + ".xaml", s);
                    this.ShowMessageAsync(string.Empty, "保存为xaml成功");
                }
                //DesignerSerializationVisibilityAttribute
            }
        }
        private PropertySequencer sequencer;
        private INotifyPropertyChangeTo currentObject;
        private IDisposable disposable;
        private Dictionary<Type, INotifyPropertyChangeTo> uics = new Dictionary<Type, INotifyPropertyChangeTo>();
        private GenerateMode mode = GenerateMode.Direct;
        private SilentObservableCollection<object> designs = new SilentObservableCollection<object>();
        private void Providing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count != 0 && e.AddedItems[0] is DesignMapping item)
            {
                long bg = Stopwatch.GetTimestamp();
                designs.Clear();
                disposable?.Dispose();
                disposable = null;
                sequencer.StripAll();
                sequencer.CleanAllRecords();
                if (!uics.TryGetValue(item.ClrType, out INotifyPropertyChangeTo obj))
                {
                    string fn = item.ClrType.Name + ".json";
                    if (File.Exists(fn))
                    {
                        string str = File.ReadAllText(fn);
                        obj = (INotifyPropertyChangeTo)DesignJsonHelper.Deserialize(str, item.ClrType);
                    }
                    else
                    {
                        obj = (INotifyPropertyChangeTo)ReflectionHelper.Create(item.ClrType);
                        if (obj is IDefaulted uis)
                        {
                            uis.SetDefault();
                        }
                    }
                    uics.Add(item.ClrType, obj);
                }
                DependencyObject ui = (DependencyObject)ReflectionHelper.Create(item.UIType);
                IEnumerable<BindingUnit> drawing = DesignerManager.CreateBindings(obj, ui, BindingMode.TwoWay, UpdateSourceTrigger.Default);
                ui.SetBindings(drawing);
                currentObject = obj;
                if (mode == GenerateMode.DataTemplate)
                {
                    var res = wpfObjectDesigner.BuildDataTemplate(obj);
                    disposable = res.Subjected;
                    var ctxs = res.GetEqualsInstanceContexts(obj);
                    designs.AddRangeNotifyReset(ctxs);
                }
                else
                {
                    var res = wpfObjectDesigner.BuildUI(obj);
                    disposable = res.Subjected;
                    var gids = new List<Grid>();
                    foreach (IWpfUISpirit ctx in res.Contexts.Where(x=>x.View!=null))
                    {
                        Grid grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        Grid.SetColumn(ctx.View, 1);
                        grid.Children.Add(new TextBlock { Text = ctx.Context.PropertyProxy.PropertyInfo.Name });
                        grid.Children.Add(ctx.View);
                        gids.Add(grid);
                    }
                    designs.AddRangeNotifyReset(gids);
                }
                Sv.Child = (UIElement)ui;
                long ed = Stopwatch.GetTimestamp();
                Title = $"Generate ui use {new TimeSpan(ed - bg)}";
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count != 0 && e.AddedItems[0] is ComboBoxItem item && item.Tag is GenerateMode mode)
            {
                this.mode = mode;
            }
        }
    }
}
