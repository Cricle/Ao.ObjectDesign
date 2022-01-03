using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session.DesignHelpers;
using Ao.ObjectDesign.Session.Environment;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.InputBindings;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Services;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
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

namespace ObjectDesign.Brock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var dp = App.Provider.GetRequiredService<DesignPanel>();
            Cv.Children.Add(dp);
            DataContext = App.Provider.GetRequiredService<MainWindowModel>();
        }
    }
}
