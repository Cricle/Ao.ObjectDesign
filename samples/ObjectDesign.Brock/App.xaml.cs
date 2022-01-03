using Ao.Lang.Runtime;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using ObjectDesign.Brock.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ObjectDesign.Brock
{
    public partial class App : Application
    {
        private static IServiceProvider provider;

        public static IServiceProvider Provider => provider;

        public App()
        {
            DispatcherUnhandledException += OnAppDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var appEng = new AppEngine(this);
            appEng.Services.AddSingleton<DesignPanel>();
            appEng.ConfigurateService();
            provider = appEng.Build();
        }

        private void ShowError(string msg)
        {
            if (Current.MainWindow is MetroWindow win)
            {
                var lang = provider.GetRequiredService<LanguageManager>();
                var title = lang.Root["Cdev.Dialog.Exception"];
                win.ShowMessageAsync(title, msg);
            }
        }
        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowError(((Exception)e.ExceptionObject).Message);
        }

        private void OnTaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ShowError(e.Exception.Message);
            e.SetObserved();
        }

        private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ShowError(e.Exception.Message);
            e.Handled = true;
        }
    }
}
