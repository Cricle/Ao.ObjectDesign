using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class PhysicalWorkplace<TScene, TDesignObject> : Workplace<TScene, TDesignObject>
        where TScene : IDeisgnScene<TDesignObject>
    {
        protected PhysicalWorkplace(DirectoryInfo folder, string extensions)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            Extensions = extensions ?? throw new ArgumentNullException(nameof(extensions));
        }

        public DirectoryInfo Folder { get; }

        public string Extensions { get; }

        public override IReadOnlyList<string> SceneNames => GetSceneFiles().Select(x => GetSceneName(x)).ToList();

        protected override void CoreAddScene(TScene scene, string name)
        {
            var path = GetScenePath(name);
            using (var stream = ToStream(scene))
            using (var fs = File.Create(path))
            {
                stream.CopyTo(fs);
            }
        }
        protected override bool CoreRemoveScene(string name)
        {
            var path = GetScenePath(name);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        protected override void CoreStore(TScene scene)
        {
            var sceneName = GetSceneName(scene);
            if (string.IsNullOrEmpty(sceneName))
            {
                throw new InvalidOperationException("No name scene can't store");
            }
            var path = GetScenePath(sceneName);
            using (var ss = ToStream(scene))
            using (var fs = File.Open(path, FileMode.Create))
            {
                ss.CopyTo(fs);
            }
        }
        protected override TScene CoreCopyScene(string sourceName, string destName)
        {
            ThrowIfHasScene(destName);
            if (!HasScene(sourceName))
            {
                throw new InvalidOperationException($"Can't find scene {sourceName}");
            }
            var sourcePath = GetScenePath(sourceName);
            var destPath = GetScenePath(destName);
            File.Copy(sourcePath, destPath);
            return default;
        }
        protected void ThrowIfHasScene(string name)
        {
            if (HasScene(name))
            {
                throw new InvalidOperationException($"Scene name {name} exists, please use HasScene check!");
            }
        }

        public override TScene GetScene(string name)
        {
            var path = GetScenePath(name);
            if (File.Exists(path))
            {
                using (var stream = File.OpenRead(path))
                {
                    return FromStream(stream);
                }
            }
            return default;
        }

        public override bool HasScene(string name)
        {
            var path = GetScenePath(name);
            return File.Exists(path);
        }
        public virtual IEnumerable<FileInfo> GetSceneFiles()
        {
            return Folder.EnumerateFiles($"*.{Extensions}", SearchOption.TopDirectoryOnly);
        }
        protected virtual string GetSceneFileName(string name)
        {
            return string.Concat(name, ".", Extensions);
        }
        protected virtual string GetSceneName(FileInfo fileInfo)
        {
            return Path.GetFileNameWithoutExtension(fileInfo.Name);
        }
        protected string GetScenePath(string name)
        {
            var sceneName = GetSceneFileName(name);
            return Path.Combine(Folder.FullName, sceneName);
        }

        protected abstract Stream ToStream(TScene scene);

        protected abstract TScene FromStream(Stream stream);
    }
}
