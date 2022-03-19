using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign;
using Microsoft.Toolkit.Mvvm.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObjectDesign.Brock.Views
{
    public class RotateSection
    {
        public static ICommand SelectFileCommand { get; } = BuildUpdateDesign();

        private static RelayCommand<WpfTemplateForViewBuildContext> BuildUpdateDesign()
        {
            return new RelayCommand<WpfTemplateForViewBuildContext>(UpdateDesign);
        }

        private static void UpdateDesign(WpfTemplateForViewBuildContext paramter)
        {
            var state = DesigningDataHelper<Scene, UIElementSetting>.GetPropertyPanel(paramter);
            if (state!=null)
            {
                state.Session.Suface.UpdateInRender();
            }
        }
    }
}
