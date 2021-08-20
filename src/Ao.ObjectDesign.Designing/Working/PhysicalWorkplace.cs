using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Working
{
    public abstract class PhysicalWorkplace<TKey, TResource> : Workplace<TKey, TResource>
    {
        protected PhysicalWorkplace(DirectoryInfo folder)
            : this(folder, ReadOnlyHashSet<string>.Empty)
        {
            AcceptAllFile = true;
        }
        protected PhysicalWorkplace(DirectoryInfo folder, IReadOnlyHashSet<string> acceptExtensions)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            AcceptExtensions = acceptExtensions;
        }

        public bool AcceptAllFile { get; }

        public IReadOnlyHashSet<string> AcceptExtensions { get; }

        public DirectoryInfo Folder { get; }

        public override IEnumerable<TKey> Resources
        {
            get
            {
                Debug.Assert(AcceptExtensions != null);
                var files = Directory.EnumerateFiles(Folder.FullName);
                if (!AcceptAllFile)
                {
                    var accepts = AcceptExtensions;
                    files = files.Where(x => accepts.Contains(Path.GetExtension(x)));
                }
                return files.Select(x => ToKey(Path.GetFileName(x)));
            }
        }

        protected abstract string ToRelativePath(TKey key);
        protected abstract TKey ToKey(string relativePath);

        protected abstract TResource ToResource(TKey key, string path);

        protected abstract void WriteResource(TKey key, Stream stream, TResource resource);

        protected string GetAbsolutePath(TKey key)
        {
            Debug.Assert(Folder != null);
            var relativePath = ToRelativePath(key);
            var path = Path.Combine(Folder.FullName, relativePath);
            return path;
        }

        public override void Clear()
        {
            foreach (var item in Folder.EnumerateFiles())
            {
                item.Delete();
            }
            RaiseClean();
        }

        public override void Copy(TKey sourceKey, TKey destKey)
        {
            var sourcePath = GetAbsolutePath(sourceKey);
            var destPath = GetAbsolutePath(destKey);

            File.Copy(sourcePath, destPath);
            RaiseCopiedResource(new CopyResourceResultEventArgs<TKey>(sourceKey, destKey));
        }

        public override TResource Get(TKey key)
        {
            var path = GetAbsolutePath(key);
            return ToResource(key, path);
        }

        public override bool Has(TKey key)
        {
            var path = GetAbsolutePath(key);
            return File.Exists(path);
        }

        public override bool Remove(TKey key)
        {
            var path = GetAbsolutePath(key);
            if (File.Exists(path))
            {
                File.Delete(path);
                RaiseRemovedResource(new ActionResouceResultEventArgs<TKey>(key, ResourceActions.Removed));
                return true;
            }
            return false;
        }

        public override void Rename(TKey oldKey, TKey newKey)
        {
            var oldPath = GetAbsolutePath(oldKey);
            var newPath = GetAbsolutePath(newKey);

            File.Move(oldPath, newPath);
            RaiseRenamedResource(new CopyResourceResultEventArgs<TKey>(oldKey, newKey));
        }

        public override void Store(TKey key, TResource resource)
        {
            var path = GetAbsolutePath(key);
            using (var fs = File.Open(path, FileMode.Create))
            {
                WriteResource(key, fs, resource);
            }
            RaiseStoredResource(new StoredResourceEventArgs<TKey, TResource>(key, resource));
        }
    }
}
