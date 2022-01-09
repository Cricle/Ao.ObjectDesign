using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using System;
using System.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DesignDependencyInjection
    {
        public static IServiceCollection AddSceneEngine<TScene, TDesignObject>(this IServiceCollection services, SceneMakerStartup<TScene, TDesignObject> startup)
            where TScene : IDesignScene<TDesignObject>
        {
            return AddSceneEngine(services, _ => startup);
        }
        public static IServiceCollection AddSceneEngine<TScene, TDesignObject>(this IServiceCollection services, Func<IServiceProvider, SceneMakerStartup<TScene, TDesignObject>> startupFunc)
            where TScene : IDesignScene<TDesignObject>
        {
            services.AddSingleton<ISceneMakerStartup<TScene, TDesignObject>>(startupFunc);
            services.AddSingleton(startupFunc);
            services.AddSingleton((p) =>
            {
                var s = p.GetRequiredService<SceneMakerStartup<TScene, TDesignObject>>();
                s.Initialize();
                s.Ready();
                return s.Engine;
            });
            AddEngineServices<TScene, TDesignObject>(services);
            return services;
        }
        public static IServiceCollection AddSceneEngine<TScene, TDesignObject>(this IServiceCollection services, Func<IServiceProvider, SceneEngine<TScene, TDesignObject>> engineFunc)
            where TScene : IDesignScene<TDesignObject>
        {
            services.AddSingleton(engineFunc);
            AddEngineServices<TScene, TDesignObject>(services);
            return services;
        }

        private static void AddEngineServices<TScene, TDesignObject>(IServiceCollection services)
            where TScene : IDesignScene<TDesignObject>
        {
            Debug.Assert(services != null);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().BindingCreatorStateDecoraters);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().DataTemplateBuilder);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().ForViewBuilder);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().UIGenerator);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().InstanceDesigner);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().DesignOrderManager);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().TemplateContextsDecoraters);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().BindingCreatorStateCreator);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().DesignPackage);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().ClipboardManager);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().Environment);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().UIDesignMap);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().SessionManager);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().Environment.BindingCreatorStateCreator);
            services.AddSingleton(x => x.GetRequiredService<SceneEngine<TScene, TDesignObject>>().Environment.FileSystem);

        }
    }
}
