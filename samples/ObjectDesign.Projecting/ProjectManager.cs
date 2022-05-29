using Ao.Project;
using Ao.Project.NewtonsoftJson;
using System;
using System.IO;
using System.IO.Abstractions;

namespace ObjectDesign.Projecting
{
    public abstract class ProjectManager : IDisposable
    {
        private IDirectoryInfo projectInfo;
        private IProject project;
        private IFileInfo projectFile;
        private IFileSystem fileSystem;
        private IFileSystemWatcher watcher;
        private FileEntryRoot root;

        protected ProjectManager(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public FileEntryRoot Root => root;

        public IDirectoryInfo ProjectFolder => projectInfo;

        public IProject Project => project;

        public IFileInfo ProjectFile => projectFile;

        public IFileSystemWatcher Watcher => watcher;

        public IFileSystem FileSystem => fileSystem;

        public void SetFileSystem(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }
        public IFileSystemWatcher CreateWatcher(string path, string filter = "*")
        {
            return fileSystem.FileSystemWatcher.CreateNew(path, filter);
        }

        public void SaveProject(IProjectSkeleton skeleton, IFileInfo file)
        {
            using (var s = file.Open(FileMode.OpenOrCreate))
            {
                s.SetLength(0);
                GetProjectInterop(file).Save(skeleton, s);
            }
        }
        public void SetProject(IDirectoryInfo root, IFileInfo projectFile)
        {
            this.projectInfo = root ?? throw new ArgumentNullException(nameof(root));
            this.projectFile = projectFile ?? throw new ArgumentNullException(nameof(projectFile));
            this.project = CreateProject(projectFile);

            Close();
            this.root = new FileEntryRoot(root, JsonSceneItem.JsonSceneFilter);
            watcher = fileSystem.FileSystemWatcher.CreateNew(root.FullName);
            watcher.EnableRaisingEvents = true;
        }
        public virtual void Close()
        {
            watcher?.Dispose();
        }
        public virtual void Dispose()
        {
            Close();
        }

        protected abstract IProjectInterop GetProjectInterop(IFileInfo fileInfo);

        protected virtual IProject CreateProject()
        {
            return new Project();
        }

        protected virtual IProject CreateProject(IFileInfo fileInfo)
        {
            using (var stream = fileInfo.OpenRead())
            {
                var project = CreateProject();
                var interop = GetProjectInterop(fileInfo);
                JsonProjectInterop.Instance.Load(project, stream);
                return project;
            }
        }
    }
}
