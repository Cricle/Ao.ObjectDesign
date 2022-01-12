using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDesign.Brock.Services
{
    internal class SceneService : IDisposable
    {
        private IFileSystemWatcher watcher;

        public SceneService(IDirectoryInfo directory, string extensions)
        {
            Directory = directory;
            Extensions = extensions;
            Files = new SilentObservableCollection<IFileInfo>();
        }

        public IDirectoryInfo Directory { get; }

        public string Extensions { get; }

        public SilentObservableCollection<IFileInfo> Files { get; }

        public void Dispose()
        {
            if (watcher != null)
            {
                watcher.Created -= OnCreated;
                watcher.Renamed -= OnRenamed;
                watcher.Deleted -= OnDeleted;
                watcher.Dispose();
            }
        }

        public void Watch()
        {
            Dispose();
            watcher = Directory.FileSystem.FileSystemWatcher.CreateNew(Directory.FullName, Extensions);
            watcher.Created += OnCreated;
            watcher.Renamed += OnRenamed;
            watcher.Deleted += OnDeleted;
        }

        private async void OnDeleted(object sender, FileSystemEventArgs e)
        {
            await Task.Delay(500);
            FlushScenes();
        }

        private async void OnRenamed(object sender, RenamedEventArgs e)
        {
            await Task.Delay(500);
            FlushScenes();
        }

        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            await Task.Delay(500);
            FlushScenes();
        }
        public void FlushScenes()
        {
            Files.Clear();
            var fs = Directory.EnumerateFiles("*." + Extensions, SearchOption.TopDirectoryOnly);
            Files.AddRange(fs);
        }
    }
}
