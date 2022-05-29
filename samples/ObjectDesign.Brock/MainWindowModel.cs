using Ao.Lang.Runtime;
using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using ObjectDesign.Brock.Behaviors;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Models;
using ObjectDesign.Brock.Services;
using ObjectDesign.Projecting;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ao.Project;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ObjectDesign.Brock
{
    internal class MainWindowModel : ObservableObject
    {
        public MainWindowModel(SceneEngine<Scene, UIElementSetting> engine,
            SceneMakerStartup<Scene, UIElementSetting> startup,
            MySceneMakerRuntime runtime,
            DesignPanel designPanel,
            LanguageManager langMgr,
            ActionSettingService actionSettingService,
            MoveWayService moveWayService,
            MetroWindow window,
            ProjectManager projectManager)
        {
            this.window = window;
            this.engine = engine;
            this.startup = startup;
            Runtime = runtime;
            this.designPanel = designPanel;
            this.langMgr = langMgr;
            this.actionSettingService = actionSettingService;
            this.moveWayService = moveWayService;
            this.projectManager = projectManager;

            TipsVisibility = Visibility.Visible;
            CopyCommand = new RelayCommand(Copy);
            LoadFromFileCommand = new RelayCommand(LoadFromFile);
            SaveToFileCommand = new AsyncRelayCommand(SaveToFileAsync);
            ResetCommand = new RelayCommand(Reset);
            MoveTopCommand = new RelayCommand(MoveTop);
            MoveUpCommand = new RelayCommand(MoveUp);
            MoveDownCommand = new RelayCommand(MoveDown);
            MoveBottomCommand = new RelayCommand(MoveBottom);
            CopyItemCommand = new RelayCommand<int>(CopyItem);
            DeleteCommand = new RelayCommand(Delete);
            PutInContainerCommand = new RelayCommand(PutInContainer);
            GCCommand = new RelayCommand(() => GC.Collect());

            ToLangZHCommand = new RelayCommand(() => langMgr.SetCulture("zh"));
            ToLangENCommand = new RelayCommand(() => langMgr.SetCulture("en"));
            ToLangSQCommand = new RelayCommand(() => langMgr.SetCulture("sq"));
            ToLangTHCommand = new RelayCommand(() => langMgr.SetCulture("th"));
            ToLangJPCommand = new RelayCommand(() => langMgr.SetCulture("ja"));
        }
        private readonly SceneEngine<Scene, UIElementSetting> engine;
        private readonly SceneMakerStartup<Scene, UIElementSetting> startup;
        private readonly DesignPanel designPanel;
        private readonly LanguageManager langMgr;
        private readonly ActionSettingService actionSettingService;
        private readonly MoveWayService moveWayService;
        private readonly MetroWindow window;
        private readonly ProjectManager projectManager;

        private Visibility tipsVisibility;

        public MySceneMakerRuntime Runtime { get; }

        public ObservableCollection<FileEntryInfo> ProjectSceneDirecotry
        {
            get
            {
                var sc = projectManager.Project.EnsureGetMetadata<FileEntryRoot>(JsonSceneItem.SceneInfoKey);
                return sc.Root.Nexts;
            }
        }

        public ProjectManager ProjectManager => projectManager;

        public DropBox CurrentDropBox
        {
            set
            {
                if (value != null)
                {
                    ProcessDrop(value);
                }
            }
        }
        public UIElementSetting SelectedSetting
        {
            get
            {
                var dps = Runtime.CurrentSession?.Suface.DesigningObjects;
                if (dps != null && dps.Length == 1)
                {
                    var set = new ReadOnlyHashSet<UIElement>(dps);
                    var pair = Runtime.CurrentSession.SceneManager.CurrentSceneController.Find(set)
                        .FirstOrDefault();
                    if (pair != null)
                    {
                        return pair.DesigningObject;
                    }
                }
                return null;
            }
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedSetting)));
                actionSettingService.Select(value);
            }
        }

        public Visibility TipsVisibility
        {
            get => tipsVisibility;
            set => SetProperty(ref tipsVisibility, value);
        }


        public ICommand CopyCommand { get; }
        public ICommand LoadFromFileCommand { get; }
        public ICommand SaveToFileCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand MoveTopCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand MoveBottomCommand { get; }
        public IRelayCommand<int> CopyItemCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand PutInContainerCommand { get; }
        public ICommand GCCommand { get; }

        public ICommand ToLangZHCommand { get; }
        public ICommand ToLangENCommand { get; }
        public ICommand ToLangSQCommand { get; }
        public ICommand ToLangTHCommand { get; }
        public ICommand ToLangJPCommand { get; }

        private async void ProcessDrop(DropBox box)
        {
            var dt = box.Args.Data.GetData(DropKeys.DropToolKey, false) as DesignTool;
            if (dt != null)
            {
                Point pos = box.Args.GetPosition(box.Sender as UIElement);
                actionSettingService.Insert(dt.SettingType, pos, true);
                if (TipsVisibility == Visibility.Visible)
                {
                    TipsVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                var fi = box.Args.Data.GetData(DataFormats.FileDrop) as string[];
                if (fi != null && fi.Length == 1 && fi[0].EndsWith(".hello"))
                {
                    try
                    {
                        CoreLoadFromFile(fi[0]);
                        TipsVisibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        var langRoot = langMgr.Root;
                        var err = langRoot["Cdev.Dialog.Error"];
                        await window.ShowMessageAsync(err, ex.Message);
                    }
                }
                else
                {
                    var langRoot = langMgr.Root;
                    var err = langRoot["Cdev.Dialog.Error"];
                    var msg = langRoot["Cdev.Import.FileNotSupport"];
                    await window.ShowMessageAsync(err, msg);
                }
            }
        }

        public void Copy()
        {
            actionSettingService.CopyToClipboard();
        }
        public void PutInContainer()
        {
            actionSettingService.PutInContainer();
        }
        public void Delete()
        {
            actionSettingService.Delete();
        }
        public void CopyItem(int count)
        {
            actionSettingService.MulityCopy(count);
        }

        public void MoveTop()
        {
            moveWayService.MoveTop();
        }
        public void MoveUp()
        {
            moveWayService.MoveUp();
        }
        public void MoveDown()
        {
            moveWayService.MoveDown();
        }
        public void MoveBottom()
        {
            moveWayService.MoveBottom();
        }

        public void LoadFromFile()
        {
            var of = new OpenFileDialog();
            of.Filter = "hello文件|*.hello";
            if (of.ShowDialog().GetValueOrDefault(false) && of.FileName != null)
            {
                CoreLoadFromFile(of.FileName);
            }
        }
        public async Task SaveToFileAsync()
        {
            var sf = new SaveFileDialog();
            sf.Filter = "hello文件|*.hello";
            if (sf.ShowDialog().GetValueOrDefault(false))
            {
                using (var fs = sf.OpenFile())
                {
                    actionSettingService.Save(fs);
                }
                var root = langMgr.Root;
                var title = root["Cdev.Dialog.Succeed"];
                var msg = root["Cdev.Dialog.SavedToFile", sf.FileName];
                await window.ShowMessageAsync(title, msg);
            }
        }
        public void Reset()
        {
            actionSettingService.Clean();
            var s = startup.NewSession(new Scene());
            actionSettingService.SetSession(s);
            TipsVisibility = Visibility.Visible;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CoreLoadFromFile(string path)
        {
            actionSettingService.Clean();
            using (var fs = File.OpenRead(path))
            {
                actionSettingService.Load(fs);
            }
            GC.Collect();
        }
        public void DoDrag()
        {

        }
    }
}
