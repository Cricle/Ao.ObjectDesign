using Ao.ObjectDesign;
using Ao.ObjectDesign.Abstract.Annotations;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using Ao.ObjectDesign.Wpf.Designing;
using MahApps.Metro.Controls;
using ObjectDesign.Wpf.DesignControls;
using ObjectDesign.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using System.Windows.Media.Effects;
using System.IO.Compression;

namespace ObjectDesign.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MyControl c = new MyControl();
        public MainWindow()
        {
            InitializeComponent();
           
            var builder = new ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>();
            builder.Add(new LocationSizeCondition());
            builder.Add(new CornerDesignCondition());
            builder.Add(new PrimitiveCondition());
            builder.Add(new PenBrushCondition());
            builder.Add(new FontSettingCondition());
            builder.Add(new EnumCondition());

            var builderDet = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();
            builderDet.Add(new BooleanForViewCondition());
            builderDet.Add(new EnumForViewCondition());
            builderDet.Add(new ValueTypeForViewCondition());

            var ob = ObjectDesigner.CreateDefaultProxy(this, this.GetType());
            var prov = ob.GetPropertyProxies();
            
            var lv = new ItemsControl();
            var tmp = new ForViewDataTemplateSelector(builder, ObjectDesigner.Instance);
            tmp.UseCompiledVisitor = true;
            lv.ItemsSource = prov;
            lv.ItemTemplateSelector = tmp;
            DesignPlan.Child = lv;
            
            var border = new LightBorder { Width = 100, Height = 100, CornerRadius = new CornerRadius(10, 20, 30, 40) };
            border.SetBinding(Canvas.LeftProperty, new Binding(nameof(LocationSize.Left)) { Source = c.Size, Mode = BindingMode.TwoWay });
            border.SetBinding(Canvas.TopProperty, new Binding(nameof(LocationSize.Top)) { Source = c.Size, Mode = BindingMode.TwoWay });
            border.MyControl = c;
            Design.Children.Add(border);

            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.E)
            {
                var jstr = JsonSerializer.Serialize(c);
                var desc = JsonSerializer.Deserialize<MyControl>(jstr, new JsonSerializerOptions
                {
                    IgnoreReadOnlyProperties = false
                });

                Design.Children.Clear();

                var border = new LightBorder();
                border.SetBinding(Canvas.LeftProperty, new Binding(nameof(LocationSize.Left)) { Source = desc.Size, Mode = BindingMode.OneWay });
                border.SetBinding(Canvas.TopProperty, new Binding(nameof(LocationSize.Top)) { Source = desc.Size, Mode = BindingMode.OneWay });
                border.MyControl = desc;
                Design.Children.Add(border);

            }
        }
    }
}
