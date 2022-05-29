using Ao.Lang;
using Ao.Lang.Lookup;
using Ao.Lang.Runtime;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using Ao.ObjectDesign.Sources;
using Ao.Project;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Configuration.Resources;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Models;
using ObjectDesign.Brock.Services;
using ObjectDesign.Projecting;
using System;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Windows;

namespace ObjectDesign.Brock
{
    internal class AppEngine
    {
        public AppEngine(Application app)
        {
            this.app = app;
            Services = new ServiceCollection();
        }
        private readonly Application app;

        public IServiceCollection Services { get; }

        public virtual void ConfigurateService()
        {
            Services.AddSingleton(app);
            Services.AddSingleton(provider => (MetroWindow)provider.GetRequiredService<Application>().MainWindow);
            Services.AddSceneEngine((p) => new MySceneMakerStartup(p));

            var lang = new LanguageService();
            Services.AddSingleton<ILanguageService>(lang);

            var langMgr = LanguageManager.Instance;
            langMgr.CultureInfo = new CultureInfo("zh");
            LoadLang(langMgr);
            Services.AddSingleton(langMgr);
            Services.AddScoped(provider => provider.GetRequiredService<LanguageManager>().LangService);
            Services.AddScoped(provider => provider.GetRequiredService<LanguageManager>().Root);

            Services.AddSingleton<MySceneMakerRuntime>();

            Services.AddSingleton<MoveWayService>();
            Services.AddSingleton<ActionSettingService>();
            Services.AddSingleton<MainWindowModel>();
            Services.AddSingleton(provider =>
            {
                return new ImageManager(null);
            });
            Services.AddSingleton<ProjectManager, JsonProjectManager>(x =>
            {
                var m= new JsonProjectManager(new FileSystem());
                var proj = new Project();
                proj.PropertyGroups.Add(new JsonDataProvider
                {
                    Name="Base data",
                    Descript="Provider base data",
                    Value=JsonConvert.SerializeObject(new
                    {
                        name="aaa",
                        age=22,
                        a="dsads"
                    })
                });
                proj.ItemGroups.Add(new JsonSceneItem());
                var dir = m.FileSystem.DirectoryInfo
                    .FromDirectoryName(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"test"));
                if (!dir.Exists)
                {
                    dir.Create();
                }
                var fi = m.FileSystem.FileInfo.FromFileName(Path.Combine(dir.FullName, "proj.jsonproject"));
                m.SaveProject(proj.ToSkeleton(), fi);
                m.SetProject(dir, fi);
                return m;
            });
            Services.AddSingleton<DataProviderGroup>();
            Services.AddSingleton<FileEntryRoot>();
            Services.AddSingleton<DataManager>();
        }

        public virtual IServiceProvider Build()
        {
            var provider = Services.BuildServiceProvider();
            Init(provider);
            var projMgr = provider.GetRequiredService<ProjectManager>();
            projMgr.Project.Initialize(provider);
            projMgr.Project.Decorate();
            projMgr.Project.ConductAsync().GetAwaiter().GetResult();
            return provider;
        }

        private void Init(IServiceProvider provider)
        {
            var eng = provider.GetRequiredService<SceneEngine<Scene, UIElementSetting>>();
            var startup = provider.GetRequiredService<SceneMakerStartup<Scene, UIElementSetting>>();
            var runtime = provider.GetRequiredService<MySceneMakerRuntime>();
            var ass = provider.GetRequiredService<ActionSettingService>();

            eng.BindingCreatorStateCreator.AsImpl<Scene, UIElementSetting>()
                .RuntimeType = DesignRuntimeTypes.Designing;

            var session = startup.NewSession(new Scene());

            session.SceneManager.SetLazyBinding(false);
            ass.SetSession(session);
            runtime.AutoSwithDesignPanel = true;

            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.Rectangle",
                Icon = '\xE003',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(RectangleSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.TextBlock",
                Icon = '\xE18E',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(TextBlockSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.TextBox",
                Icon = '\xE185',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(TextBoxSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.Line",
                Icon = '\xE199',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(LineSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.ProgressBar",
                Icon = '\xE0B8',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(ProgressBarSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.Image",
                Icon = '\xE114',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(RelativeFileImageSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.Media",
                Icon = '\xE124',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(MediaElementSetting)
            });
            runtime.DesignTools.Add(new DesignTool
            {
                LangKey = "Cdev.Tools.Canvas",
                Icon = '\xE138',
                Type = DesignToolTypes.Tool,
                SettingType = typeof(CanvasSetting)
            });
            var tools = runtime.DesignTools;
            for (int i = 0; i < tools.Count; i++)
            {
                var tool = tools[i];
                tool.runtime = runtime;
                tool.UpdateName(provider);
                tool.Listen(provider);
            }
        }

        private static void LoadLang(LanguageManager langMgr)
        {
            const int langRevIndex = 2;
            var langSer = langMgr.LangService;
            var assembly = typeof(App).Assembly;
            var resources = assembly.GetManifestResourceNames();
            foreach (var item in resources)
            {
                if (item.EndsWith(".resources"))
                {
                    var sps = item.Split('.');
                    if (sps.Length > 1 && sps.Length >= langRevIndex)
                    {
                        var lang = sps[sps.Length - langRevIndex - 1].Replace('_', '-');
                        if (CultureInfoHelper.IsAvaliableCulture(lang))
                        {
                            var stream = assembly.GetManifestResourceStream(item);
                            var node = langSer.EnsureGetLangNode(lang);
#if NET5_0_OR_GREATER
                            var rs = new ResourceStreamConfigurataionSource { Stream = stream };
#else
                            var rs = new ResourceStreamConfigurataionSource(stream);
#endif
                            node.Add(rs);
                        }
                    }
                }
            }
        }

    }
}
