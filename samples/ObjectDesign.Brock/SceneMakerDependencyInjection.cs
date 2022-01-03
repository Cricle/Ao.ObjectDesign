using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Components;
using System;
using System.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SceneMakerDependencyInjection
    {
        public static IServiceCollection AddSceneEngine(this IServiceCollection services, SceneMakerStartup<Scene, UIElementSetting> startup)
        {
            return AddSceneEngine(services, _ => startup);
        }
        public static IServiceCollection AddSceneEngine(this IServiceCollection services, Func<IServiceProvider, SceneMakerStartup<Scene, UIElementSetting>> startupFunc)
        {
            services.AddSingleton<ISceneMakerStartup<Scene, UIElementSetting>>(startupFunc);
            services.AddSingleton(startupFunc);
            services.AddSingleton((p) =>
            {
                var s = p.GetRequiredService<SceneMakerStartup<Scene, UIElementSetting>>();
                s.Initialize();
                s.Ready();
                return s.Engine;
            });
            AddEngineServices(services);
            return services;
        }
        public static IServiceCollection AddSceneEngine(this IServiceCollection services, Func<IServiceProvider, SceneEngine<Scene, UIElementSetting>> engineFunc)
        {
            services.AddSingleton(engineFunc);
            AddEngineServices(services);
            return services;
        }

        private static void AddEngineServices(IServiceCollection services)
        {
            Debug.Assert(services != null);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().BindingCreatorStateDecoraters);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().DataTemplateBuilder);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().ForViewBuilder);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().UIGenerator);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().InstanceDesigner);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().DesignOrderManager);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().TemplateContextsDecoraters);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().BindingCreatorStateCreator);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().DesignPackage);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().ClipboardManager);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().EngineEnvironment);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().EngineConfiguration);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().UIDesignMap);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().SessionManager);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().EngineConfiguration.EngineEnvironment);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().EngineConfiguration.EngineEnvironment.BindingCreatorStateCreator);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<Scene, UIElementSetting>>().EngineConfiguration.EngineEnvironment.FileSystem);

        }
    }
}
