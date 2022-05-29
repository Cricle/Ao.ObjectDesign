using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Projecting;

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
