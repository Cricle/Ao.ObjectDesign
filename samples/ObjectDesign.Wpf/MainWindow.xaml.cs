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
using ObjectDesign.Wpf.Controls;
using ObjectDesign.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
    public class Objx
    {
        public string Name { get; set; }

        public string Name2 { get; set; }

        public int Age { get; set; }
    }
    public class MyXamlObjectWriter : XamlObjectWriter
    {
        public MyXamlObjectWriter(XamlSchemaContext schemaContext) : base(schemaContext)
        {
        }

        public MyXamlObjectWriter(XamlSchemaContext schemaContext, XamlObjectWriterSettings settings) : base(schemaContext, settings)
        {
        }
        protected override bool OnSetValue(object eventSender, XamlMember member, object value)
        {
            if (member.Type.UnderlyingType==typeof(int))
            {
                return false;
            }
            return base.OnSetValue(eventSender, member, value);
        }
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
            wpfObjectDesigner.UIBuilder.Add(new DateTimeBrushForViewCondition());
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


            wpfObjectDesigner.DataTemplateBuilder.AddRange(coditions);

            forViewDataTemplateSelector = wpfObjectDesigner.CreateTemplateSelector();
            sequencer = (Sequencer)wpfObjectDesigner.Sequencer;
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
                    using (FileStream fs = File.Open(t.Name + ".bson", FileMode.Open))
                    {
                        var obj=DesignBsonHelper.Deserialize(fs, t);
                    }
                    this.ShowMessageAsync(string.Empty, "保存为bson成功");
                }
                else if (e.Key == Key.Y)
                {
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
                //DesignerSerializationVisibilityAttribute
            }
        }
        private Sequencer sequencer;
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
                INotifyPropertyChangeTo obj = null;
                designs.Clear();
                disposable?.Dispose();
                disposable = null;
                sequencer.StripAll();
                sequencer.CleanAllRecords();
                if (!uics.TryGetValue(item.ClrType, out obj))
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
                if (ui is FrameworkElement fe)
                {
                    fe.SetBindings(drawing);
                }
                else if (ui is FrameworkContentElement fce)
                {
                    fce.SetBindings(drawing);
                }
                currentObject = obj;
                if (mode == GenerateMode.DataTemplate)
                {
                    var res = wpfObjectDesigner.BuildDataTemplate(obj, AttackModes.VisitorAndDeclared);
                    disposable = res.Subjected;
                    if (Design.ItemTemplateSelector is null)
                    {
                        Design.ItemTemplateSelector = forViewDataTemplateSelector;
                    }
                    foreach (WpfTemplateForViewBuildContext prop in res.GetEqualsInstanceContexts(obj))
                    {
                        designs.Add(prop);
                    }
                }
                else
                {
                    var res = wpfObjectDesigner.BuildUI(obj, AttackModes.NativeAndDeclared);
                    disposable = res.Subjected;
                    foreach (IWpfUISpirit ctx in res.Contexts)
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
    }
}
