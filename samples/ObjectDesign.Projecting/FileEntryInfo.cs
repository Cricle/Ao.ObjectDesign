using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;

namespace ObjectDesign.Projecting
{
    public class FileEntryInfo
    {
        public FileEntryInfo(IFileInfo file)
        {
            File = file;
            Nexts = new ObservableCollection<FileEntryInfo>();
        }
        public FileEntryInfo(IDirectoryInfo directory)
        {
            Directory = directory;
            Nexts = new ObservableCollection<FileEntryInfo>();
        }

        public string Name => File is null ? Directory.Name : File.Name;

        public IFileInfo File { get; }

        public IDirectoryInfo Directory { get; }

        public ObservableCollection<FileEntryInfo> Nexts { get; }
    }
    public class FileEntryRoot
    {
        public FileEntryRoot(IDirectoryInfo directory,string filter)
        {
            Filter = filter;
            Directory = directory;
        }

        public IDirectoryInfo Directory { get; }

        public string Filter { get; }

        public FileEntryInfo Root { get; private set; }

        public void Update()
        {
            Root = new FileEntryInfo(Directory);
            UpdateCore(GetFilterName(Directory), Root);
        }

        private IEnumerable<IFileSystemInfo> GetFilterName(IDirectoryInfo dir)
        {
            foreach (var item in dir.GetFiles(Filter))
            {
                yield return item;
            }
            foreach (var item in dir.EnumerateDirectories())
            {
                yield return item;
            }
        }

        private void UpdateCore(IEnumerable<IFileSystemInfo> directories, FileEntryInfo info)
        {
            foreach (var item in directories)
            {
                if (item is IDirectoryInfo dir)
                {
                    var nextInfo = new FileEntryInfo(dir);
                    UpdateCore(GetFilterName(dir), nextInfo);
                    info.Nexts.Add(nextInfo);
                }
                else
                {
                    info.Nexts.Add(new FileEntryInfo((IFileInfo)item));
                }
            }
        }
    }
}
