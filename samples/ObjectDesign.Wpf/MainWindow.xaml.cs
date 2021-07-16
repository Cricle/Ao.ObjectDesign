using Ao.ObjectDesign;
using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.Wpf.Json;
using Ao.ObjectDesign.Wpf.Xml;
using Ao.ObjectDesign.Wpf.Yaml;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using ObjectDesign.Wpf.Controls;
using ObjectDesign.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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
            dtbuilder = new ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>
            {
                new CornerDesignCondition(),
                new EnumCondition(),
                new FontSettingCondition(),
                new LocationSizeCondition(),
                new PenBrushCondition(),
                new PrimitiveCondition(),
                new FontFamilyCondition()
            };

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
                else if (e.Key == Key.S)
                {
                    Type t = currentObject.GetType();
                    string str = DesignJsonHelper.SerializeObject(currentObject);
                    File.WriteAllText(t.Name + ".json", str);
                    this.ShowMessageAsync(string.Empty, "保存为json成功");
                }
                else if (e.Key == Key.T)
                {
                    Type t = currentObject.GetType();
                    using (FileStream fs = File.Open(t.Name + ".bson", FileMode.Create))
                    using (BsonDataWriter writer = new BsonDataWriter(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        JsonSerializerSettings setting = DesignJsonHelper.CreateSerializeSettings();
                        serializer.ContractResolver = setting.ContractResolver;
                        serializer.Serialize(writer, currentObject);
                    }
                    using (FileStream fs = File.Open(t.Name + ".bson", FileMode.Open))
                    using (BsonDataReader reader = new BsonDataReader(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        JsonSerializerSettings setting = DesignJsonHelper.CreateSerializeSettings();
                        serializer.ContractResolver = setting.ContractResolver;
                        object obj = serializer.Deserialize(reader, t);
                    }
                    this.ShowMessageAsync(string.Empty, "保存为bson成功");
                }
                else if (e.Key == Key.P)
                {
                    if (currentObject is ButtonSetting bs)
                    {
                        bs.BorderBrush.RadialGradientBrushDesigner.PenGradientStops.Add(new GradientStopDesigner());
                    }
                    Type t = currentObject.GetType();
                    string str = DeisgnYamlSerializer.Serialize(currentObject);
                    File.WriteAllText(t.Name + ".yaml", str);

                    object xa = DeisgnYamlSerializer.Deserializer(str, t);
                    this.ShowMessageAsync(string.Empty, "保存为yaml成功");
                }
                else if (e.Key == Key.K)
                {
                    Type t = currentObject.GetType();
                    string s = DeisgnXmlSerializer.Serialize(currentObject);
                    File.WriteAllText(t.Name + ".xml", s);
                    this.ShowMessageAsync(string.Empty, "保存为xml成功");
                }
            }
        }
        private CompiledSequencer sequencer = new CompiledSequencer();
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
                long bg = Stopwatch.GetTimestamp();
                INotifyPropertyChangeTo obj = null;
                designs.Clear();
                disposable?.Dispose();
                disposable = null;
                sequencer.StripAll();
                sequencer.CleanAllRecords();
                if (!uics.TryGetValue(item.ClrType, out obj))
                {
                    Type t = item.ClrType;
                    string fn = t.Name + ".json";
                    if (File.Exists(fn))
                    {
                        string str = File.ReadAllText(fn);
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
                DependencyObject ui = (DependencyObject)Activator.CreateInstance(item.UIType);
                IEnumerable<BindingUnit> drawing = DesignerManager.CreateBindings(obj, ui, BindingMode.TwoWay, UpdateSourceTrigger.Default);
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
                    WpfTemplateForViewBuildContext[] ctxs = NotifySubscriber.Lookup(proxy).Select(x => new WpfTemplateForViewBuildContext
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
                    foreach (WpfTemplateForViewBuildContext prop in ctxs.Where(x => x.PropertyProxy.DeclaringInstance == proxy.Instance))
                    {
                        designs.Add(prop);
                    }
                }
                else
                {
                    UIGenerator uig = new UIGenerator(builder);
                    IEnumerable<IPropertyProxy> props = proxy.GetPropertyProxies();
                    IEnumerable<IUISpirit<FrameworkElement, WpfForViewBuildContext>> ds = uig.Generate(props);
                    IEnumerable<IUISpirit<FrameworkElement, WpfForViewBuildContext>> ctxs = ds.Where(x => x.View != null);
                    disposable = NotifySubscriber.Subscribe(ctxs.Select(x => x.Context), sequencer);
                    foreach (IUISpirit<FrameworkElement, WpfForViewBuildContext> ctx in ctxs)
                    {
                        Grid grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = GridLength.Auto
                        });
                        grid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = new GridLength(1, GridUnitType.Star)
                        });
                        Grid.SetColumn(ctx.View, 1);
                        TextBlock tbx = new TextBlock { Text = ctx.Context.PropertyProxy.PropertyInfo.Name };
                        grid.Children.Add(tbx);
                        grid.Children.Add(ctx.View);
                        designs.Add(grid);
                    }
                    //Design.ItemsSource = designs;
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

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count != 0 && e.AddedItems[0] is ComboBoxItem item && item.Tag is DesignLevels level)
            {
                this.level = level;
            }
        }
    }
}
