using Ao.Lang.Runtime;
using Ao.Lang.Wpf;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Wpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Level;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ObjectDesign.Brock.Views
{
    public static class ResourceSelection
    {
        public static ICommand SelectFileCommand { get; } = BuildRunSelectFileCommand();
        public static AsyncRelayCommand<WpfTemplateForViewBuildContext> SelectUriCommand { get; } = BuildRunSelectUriCommand();

        public static ICommand SelectMediaCommandCommand { get; } = BuildRunSelectMediaCommand();

        private static RelayCommand<WpfTemplateForViewBuildContext> BuildRunSelectFileCommand()
        {
            return new RelayCommand<WpfTemplateForViewBuildContext>(RunSelectFile);
        }
        private static AsyncRelayCommand<WpfTemplateForViewBuildContext> BuildRunSelectUriCommand()
        {
            return new AsyncRelayCommand<WpfTemplateForViewBuildContext>(RunSelectUri);
        }
        private static RelayCommand<WpfTemplateForViewBuildContext> BuildRunSelectMediaCommand()
        {
            return new RelayCommand<WpfTemplateForViewBuildContext>(RunSelectMedia);
        }
        private static async Task RunSelectUri(WpfTemplateForViewBuildContext paramter)
        {
            var state = DesigningDataHelper<Scene, UIElementSetting>.GetPropertyPanel(paramter);
            var engine = DesigningDataHelper<Scene, UIElementSetting>.GetEngine(paramter);
            if (state != null && engine != null && engine.ServiceProvider != null)
            {
                var value = paramter.PropertyVisitor.Value as ResourceIdentity;
                if (value is null)
                {
                    return;
                }

                var app = engine.ServiceProvider.GetRequiredService<Application>();
                if (app.MainWindow is MetroWindow win)
                {
                    var langMgr = engine.ServiceProvider.GetRequiredService<LanguageManager>();
                    var inputMsg = langMgr.Root["Cdev.Dialog.InputImageUri"];
                    var str = await win.ShowInputAsync(string.Empty, inputMsg);
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (Uri.TryCreate(str, UriKind.Absolute, out _))
                        {
                            value.ResourceName = null;
                            value.Type = ResourceTypes.Uri;
                            value.ResourceGroupName = null;
                            value.ResourceName = str;
                        }
                        else
                        {
                            var title = langMgr.Root["Cdev.Dialog.Error"];
                            var msg = langMgr.Root["Cdev.Uri.InputNotUri"];
                            await win.ShowMessageAsync(title, msg);
                        }
                    }
                }
            }
        }
        private static void RunSelectMedia(WpfTemplateForViewBuildContext paramter)
        {
            var state = DesigningDataHelper<Scene, UIElementSetting>.GetPropertyPanel(paramter);
            var engine = DesigningDataHelper<Scene, UIElementSetting>.GetEngine(paramter);
            if (state != null && engine != null && engine.ServiceProvider != null)
            {
                var openfile = new OpenFileDialog();
                var langMgr = engine.ServiceProvider.GetRequiredService<LanguageManager>();
                var type = langMgr.Root["Cdev.Dialog.PickImage.Filter.Media"];
                openfile.Title = langMgr.Root["Cdev.Dialog.PickImage.Title.Media"];
                openfile.Filter = $"({type})|*.mp4;";
                openfile.CheckFileExists = true;
                openfile.Multiselect = false;
                var res = openfile.ShowDialog();
                if (res.GetValueOrDefault(false))
                {
                    var selected = openfile.FileName;

                    paramter.PropertyVisitor.Value = selected;//TODO:
                }
            }
        }
        private static void RunSelectFile(WpfTemplateForViewBuildContext paramter)
        {
            var state = DesigningDataHelper<Scene,UIElementSetting>.GetPropertyPanel(paramter);
            var engine = DesigningDataHelper<Scene, UIElementSetting>.GetEngine(paramter);
            if (state != null && engine != null&& engine.ServiceProvider!=null)
            {
                var value = paramter.PropertyVisitor.Value as ResourceIdentity;
                if (value is null)
                {
                    return;
                }
                var openfile = new OpenFileDialog();
                var langMgr = engine.ServiceProvider.GetRequiredService<LanguageManager>();
                var type = langMgr.Root["Cdev.Dialog.PickImage.Filter.Image"];
                openfile.Title = langMgr.Root["Cdev.Dialog.PickImage.Title.Image"];
                openfile.Filter = $"({type})|*.png;*.jpg;*.jpeg;*.gif";
                openfile.CheckFileExists = true;
                openfile.Multiselect = false;
                var res = openfile.ShowDialog();
                if (res.GetValueOrDefault(false))
                {
                    var selected = openfile.FileName;

                    var dir = Path.GetDirectoryName(selected);
                    var name = Path.GetFileName(selected);
                    value.Type = ResourceTypes.Path;
                    value.ResourceGroupName = dir;
                    value.ResourceName = name;
                }
            }
        }

    }
}