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

namespace ObjectDesign.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        class DesignableItem
        {
            public Type ClrType { get; set; }

            public Type UIType { get; set; }
        }
        private ForViewBuilder<FrameworkElement, WpfForViewBuildContext> builder;
        private ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> dtbuilder;
        private ForViewDataTemplateSelector forViewDataTemplateSelector;
        private IObjectDesigner objectDesigner;
        private DesignableItem[] settingTypes;
        private void Init()
        {
            builder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();
            builder.AddWpfCondition();
            builder.Add(new DateTimeBrushForViewCondition());
            objectDesigner = ObjectDesigner.Instance;
            settingTypes = typeof(BorderSetting).Assembly.GetExportedTypes()
                .Where(x => typeof(NotifyableObject).IsAssignableFrom(x))
                .Select(x => new DesignableItem
                {
                    ClrType = x,
                    UIType = x.GetCustomAttribute<MappingForAttribute>()?.Type
                })
                .Where(x => x.UIType != null && !x.UIType.IsAbstract)
                .ToArray();

            dtbuilder = new ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>();
            dtbuilder.Add(new CornerDesignCondition());
            dtbuilder.Add(new EnumCondition());
            dtbuilder.Add(new FontSettingCondition());
            dtbuilder.Add(new LocationSizeCondition());
            dtbuilder.Add(new PenBrushCondition());
            dtbuilder.Add(new PrimitiveCondition());

            forViewDataTemplateSelector = new ForViewDataTemplateSelector(dtbuilder, objectDesigner)
            {
                BindingMode = BindingMode.TwoWay,
                UseCompiledVisitor = true
            };
        }
        public MainWindow()
        {
            InitializeComponent();

            Init();
            Providing.ItemsSource = settingTypes;

            //DesignPlan.Child = items;
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.E)
            {
                var t = currentObject.GetType();
                var str = DesignJsonHelper.SerializeObject(currentObject);
                var obj = DesignJsonHelper.DeserializeObject(str, t);
            }
        }
        private object currentObject;
        private void Providing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count != 0 && e.AddedItems[0] is DesignableItem item)
            {
                var obj = Activator.CreateInstance(item.ClrType);
                var ui = (DependencyObject)Activator.CreateInstance(item.UIType);
                if (obj is FrameworkElementSetting fes)
                {
                    fes.SetDefault();
                }
                var drawing = DesignerManager.CreateBindings(obj, ui, BindingMode.TwoWay);
                if (ui is FrameworkElement fe)
                {
                    fe.SetBindings(drawing);
                }
                else if (ui is FrameworkContentElement fce)
                {
                    fce.SetBindings(drawing);
                }
                currentObject = obj;
                var designs = new List<FrameworkElement>();
                var proxy = objectDesigner.CreateProxy(obj, obj.GetType());
                //var props = proxy.GetPropertyProxies()
                //    .Where(x => dtbuilder.Any(y => y.CanBuild(forViewDataTemplateSelector.CreateContext(x))));
                //if (Design.ItemTemplateSelector is null)
                //{
                //    Design.ItemTemplateSelector = forViewDataTemplateSelector;
                //}
                //Design.ItemsSource = props;

                var props = proxy.GetPropertyProxies();
                foreach (var prop in props)
                {
                    var uix = builder.Build(new WpfForViewBuildContext
                    {
                        BindingMode = BindingMode.TwoWay,
                        Designer = ObjectDesigner.Instance,
                        ForViewBuilder = builder,
                        UseCompiledVisitor = true,
                        PropertyProxy = prop
                    });
                    if (uix != null)
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
                        Grid.SetColumn(uix, 1);
                        var tbx = new TextBlock { Text = prop.PropertyInfo.Name };
                        grid.Children.Add(tbx);
                        grid.Children.Add(uix);
                        designs.Add(grid);
                    }
                }
                Design.ItemsSource = designs;
                Sv.Content = (UIElement)ui;
            }
        }
    }
}
