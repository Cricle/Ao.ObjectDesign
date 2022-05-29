using Ao.ObjectDesign.Data;
using Ao.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDesign.Projecting
{
    public class JsonSceneItem : ItemGroupPart
    {
        public static readonly string SceneInfoKey = "ObjectDesign.Projecting.JsonSceneItem.SceneInfo";
        public static readonly string JsonSceneFilter = "*.jsonscene";
        private ProjectManager projectManager;
        private IFileSystemWatcher watcher;

        public override Task ConductAsync(IProject project)
        {
            project.Metadatas.AddOrUpdate(SceneInfoKey, projectManager.Root, (_, __) => projectManager.Root);
            watcher?.Dispose();
            watcher = projectManager.CreateWatcher(projectManager.ProjectFolder.FullName, JsonSceneFilter);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += OnFileChanged;
            watcher.Created += OnFileCreated;
            watcher.Deleted += OnFileDeleted;
            watcher.Renamed += OnFileRenamed;
            projectManager.Root.Update();
            return Task.CompletedTask;
        }
        private void Update()
        {
            projectManager.Root.Update();
        }
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            Update();
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            Update();
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Update();
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Update();
        }

        public override void Dispose()
        {
            watcher?.Dispose();

            base.Dispose();
        }
        public override void Initialize(IServiceProvider provider)
        {
            projectManager = (ProjectManager)provider.GetService(typeof(ProjectManager));
        }
    }
}
