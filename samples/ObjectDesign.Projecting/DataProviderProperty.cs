using Ao.ObjectDesign.Sources;
using Ao.Project;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Abstractions;

namespace ObjectDesign.Projecting
{
    public abstract class DataProviderProperty: PropertyGroupItem
    {
        private ProjectManager projectManager;
        private DataProviderGroup dataProviders;

        public string Name { get; set; }

        public string Descript { get; set; }

        public string Value { get; set; }

        public string FileName { get; set; }

        public override void Decorate(IProject project)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                var path = FileName;
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(projectManager.ProjectFolder.FullName, path);
                }
                var fi = projectManager.FileSystem.FileInfo.FromFileName(path);
                if (fi != null && fi.Exists)
                {
                    DoWithFile(fi);
                }
            }
            else
            {
                DoWithString(Value);
            }
        }

        protected virtual void DoWithFile(IFileInfo file)
        {
            using (var s = file.OpenRead())
            using (var sr = new StreamReader(s))
            {
                var str = sr.ReadToEnd();
                DoWithString(str);
            }
        }
        protected abstract void DoWithString(string value);

        public ProjectManager GetProjectManager()
        {
            return projectManager;
        }
        public DataProviderGroup GetDataProviderGroup()
        {
            return dataProviders;
        }

        public override void Initialize(IServiceProvider provider)
        {
            projectManager = provider.GetRequiredService<ProjectManager>();
            dataProviders = (DataProviderGroup)provider.GetService(typeof(DataProviderGroup));

            base.Initialize(provider);
        }
    }
}
