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

namespace ObjectDesign.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private CheckBoxSetting setting;
        public MainWindow()
        {
            InitializeComponent();
            
            var builder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();

            var tb = new CheckBox { Content = "dsads" };
            var brushDes = new BrushDesigner
            {
                SolidColorBrushDesigner = new SolidColorBrushDesigner
                {
                    Color = new ColorDesigner
                    {
                        Color = Colors.Blue
                    }
                }
            };
            setting = new CheckBoxSetting
            {
                Background = brushDes,
                Foreground=brushDes,
                Width = 400,
                Height = 200,
            };
            var str = DesignJsonHelper.SerializeObject(setting);
            setting = DesignJsonHelper.DeserializeObject<CheckBoxSetting>(str);
            var drawing = DesignerManager.CreateBindings(setting, tb, BindingMode.TwoWay);
            tb.SetBindings(drawing);


            Design.Child = tb;
            builder.AddWpfCondition();
            var items = new ItemsControl();
            var proxy = ObjectDesigner.CreateDefaultProxy(setting);
            var props = proxy.GetPropertyProxies().ToArray();
            foreach (var item in props)
            {
                var fe = builder.Build(new WpfForViewBuildContext
                {
                    BindingMode = BindingMode.TwoWay,
                    Designer = ObjectDesigner.Instance,
                    ForViewBuilder = builder,
                    UseCompiledVisitor = true,
                    PropertyProxy = item
                });
                if (fe != null)
                {
                    var grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                         Width=GridLength.Auto
                    });
                    grid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    });
                    Grid.SetColumn(fe,1);
                    var tbx = new TextBlock { Text = item.PropertyInfo.Name};
                    grid.Children.Add(tbx);
                    grid.Children.Add(fe);
                    items.Items.Add(grid);
                }
            }
            DesignPlan.Child = items;
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.E)
            {

            }
        }
    }
}
