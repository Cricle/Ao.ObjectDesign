using Ao.ObjectDesign;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Designing;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Controls;
using System.Windows.Shapes;
using Ao.ObjectDesign.Wpf.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Linq;
using Ao.ObjectDesign.Wpf.Annotations;
using ObjectDesign.Wpf.Views;
using System.Xaml;
using ObjectDesign.Wpf.Controls;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.IO;
using MahApps.Metro.Controls.Dialogs;

namespace ObjectDesign.Wpf
{
    public enum DesignLevels
    {
        UI,
        Setting,
    }
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
        private ForViewBuilder<FrameworkElement, WpfForViewBuildContext> builder;
        private ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> dtbuilder;
        private ForViewDataTemplateSelector forViewDataTemplateSelector;
        private IObjectDesigner objectDesigner;
        private List<DesignMapping> settingTypes;
        private void Init()
        {
            builder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();
            builder.AddWpfCondition();
            builder.Add(new DateTimeBrushForViewCondition());
            objectDesigner = ObjectDesigner.Instance;
            settingTypes = KnowControlTypes.SettingTypes
                .Where(x => !x.ClrType.IsAbstract && !x.UIType.IsAbstract && !ignoreTypes.Contains(x.UIType))
                .ToList();
            settingTypes.Add(DesignMapping.FromMapping(typeof(MoveIdSetting)));
            dtbuilder = new ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>();
            dtbuilder.Add(new CornerDesignCondition());
            dtbuilder.Add(new EnumCondition());
            dtbuilder.Add(new FontSettingCondition());
            dtbuilder.Add(new LocationSizeCondition());
            dtbuilder.Add(new PenBrushCondition());
            dtbuilder.Add(new PrimitiveCondition());

            forViewDataTemplateSelector = new ForViewDataTemplateSelector(dtbuilder, objectDesigner);
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
                else if (e.Key== Key.S)
                {
                    var t = currentObject.GetType();
                    var str = DesignJsonHelper.SerializeObject(currentObject);
                    File.WriteAllText(t.Name + ".json", str);
                    this.ShowMessageAsync(string.Empty, "保存成功");
                }
            }
        }
        private CompiledSequencer sequencer =new CompiledSequencer();
        private INotifyPropertyChangeTo currentObject;
        private IDisposable disposable;
        private Dictionary<Type, INotifyPropertyChangeTo> uics = new Dictionary<Type, INotifyPropertyChangeTo>();
        private DesignLevels level = DesignLevels.Setting;
        private GenerateMode mode = GenerateMode.Direct;
        private SilentObservableCollection<object> designs = new SilentObservableCollection<object>();
        private void Providing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count != 0 && e.AddedItems[0] is DesignMapping item)
            {
                var bg = Stopwatch.GetTimestamp();
                INotifyPropertyChangeTo obj = null;
                designs.Clear();
                disposable?.Dispose();
                disposable = null;
                sequencer.StripAll();
                sequencer.CleanAllRecords();
                if (!uics.TryGetValue(item.ClrType, out obj))
                {
                    var t = item.ClrType;
                    var fn = t.Name + ".json";
                    if (File.Exists(fn))
                    {
                        var str = File.ReadAllText(fn);
                        obj = (INotifyPropertyChangeTo)DesignJsonHelper.DeserializeObject(str, t);
                    }
                    else
                    {
                        obj = (INotifyPropertyChangeTo)Activator.CreateInstance(item.ClrType);
                        if (obj is IDefaulted uis)
                        {
                            uis.SetDefault();
                        }
                    }
                    uics.Add(item.ClrType, obj);
                }
                var ui = (DependencyObject)Activator.CreateInstance(item.UIType);
                var drawing = DesignerManager.CreateBindings(obj, ui, BindingMode.TwoWay);
                if (ui is FrameworkElement fe)
                {
                    fe.SetBindings(drawing);
                }
                else if (ui is FrameworkContentElement fce)
                {
                    fce.SetBindings(drawing);
                }
                if (ui is CheckBox cb)
                {
                    cb.Content = "hewoiuguidasvfbsa";
                }
                currentObject = obj;
                IObjectProxy proxy = null;
                if (level == DesignLevels.Setting)
                {
                    proxy = objectDesigner.CreateProxy(obj, obj.GetType());
                }
                else if (level == DesignLevels.UI)
                {
                    proxy = objectDesigner.CreateProxy(ui, ui.GetType());
                }
                if (mode == GenerateMode.DataTemplate)
                {
                    var ctxs = NotifySubscriber.Lookup(proxy).Select(x => new WpfTemplateForViewBuildContext
                    {
                        Designer = ObjectDesigner.Instance,
                        ForViewBuilder = dtbuilder,
                        PropertyProxy = x,
                        BindingMode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.Default,
                        UseNotifyVisitor = true
                    }).Where(x => dtbuilder.Any(y => y.CanBuild(x))).ToArray();
                    disposable = NotifySubscriber.Subscribe(ctxs, sequencer);
                    if (Design.ItemTemplateSelector is null)
                    {
                        Design.ItemTemplateSelector = forViewDataTemplateSelector;
                    }
                    foreach (var prop in ctxs.Where(x => x.PropertyProxy.DeclaringInstance == proxy.Instance))
                    {
                        designs.Add(prop);
                    }
                }
                else
                {
                    var uig = new UIGenerator(builder);
                    var props = proxy.GetPropertyProxies();
                    var ds = uig.Generate(props);
                    var ctxs = ds.Where(x => x.View != null);
                    disposable = NotifySubscriber.Subscribe(ctxs.Select(x=>x.Context), sequencer);
                    foreach (var ctx in ctxs)
                    {
                        var grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = GridLength.Auto
                        });
                        grid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = new GridLength(1, GridUnitType.Star)
                        });
                        Grid.SetColumn(ctx.View, 1);
                        var tbx = new TextBlock { Text = ctx.Context.PropertyProxy.PropertyInfo.Name };
                        grid.Children.Add(tbx);
                        grid.Children.Add(ctx.View);
                        designs.Add(grid);
                    }
                    //Design.ItemsSource = designs;
                }
                Sv.Child = (UIElement)ui;
                var ed = Stopwatch.GetTimestamp();
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

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count != 0 && e.AddedItems[0] is ComboBoxItem item && item.Tag is DesignLevels level)
            {
                this.level = level;
            }
        }
    }
}
