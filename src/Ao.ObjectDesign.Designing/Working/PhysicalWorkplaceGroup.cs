using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Working
{
    public abstract class PhysicalWorkplaceGroup<TKey, TResource> : WorkplaceGroup<TKey, TResource>
    {
        protected PhysicalWorkplaceGroup(DirectoryInfo folder)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            AcceptAllFile = true;
        }

        protected PhysicalWorkplaceGroup(TKey key, DirectoryInfo folder)
            : base(key)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            AcceptAllFile = true;
        }

        protected PhysicalWorkplaceGroup(DirectoryInfo folder, IReadOnlyHashSet<string> acceptExtensions)
            : this(folder)
        {
            AcceptExtensions = acceptExtensions ?? throw new ArgumentNullException(nameof(acceptExtensions));
        }
        protected PhysicalWorkplaceGroup(TKey key, DirectoryInfo folder, IReadOnlyHashSet<string> acceptExtensions)
            : this(key, folder)
        {
            AcceptExtensions = acceptExtensions ?? throw new ArgumentNullException(nameof(acceptExtensions));
        }

        public DirectoryInfo Folder { get; }

        public bool AcceptAllFile { get; }

        public IReadOnlyHashSet<string> AcceptExtensions { get; }

        public override IEnumerable<TKey> Resources
        {
            get
            {
                return Directory.EnumerateDirectories(Folder.FullName)
                    .Select(x => ToKey(x));
            }
        }

        public override void Clear()
        {
            foreach (var item in Folder.EnumerateDirectories())
            {
                item.Delete(true);
            }
            RaiseClean();
        }

        protected abstract string ToRelativePath(TKey key);
        protected abstract TKey ToKey(string folderPath);

        protected string GetAbsolutePath(TKey key)
        {
            Debug.Assert(Folder != null);
            var relativePath = ToRelativePath(key);
            var path = Path.Combine(Folder.FullName, relativePath);
            return path;
        }

        public override void Copy(TKey sourceKey, TKey destkey)
        {
            var sourcePath = GetAbsolutePath(sourceKey);
            var destPath = GetAbsolutePath(destkey);

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (var item in Directory.EnumerateFiles(sourcePath))
            {
                var fn = Path.GetFileName(item);
                var targetPath = Path.Combine(destPath, fn);

                File.Copy(item, targetPath, true);
            }
            RaiseCopiedGroup(new CopyResourceResultEventArgs<TKey>(sourceKey, destkey));
        }

        public override bool Has(TKey key)
        {
            var path = GetAbsolutePath(key);
            return Directory.Exists(path);
        }

        public override bool Remove(TKey key)
        {
            var path = GetAbsolutePath(key);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                RaiseRemovedGroup(new ActionResouceResultEventArgs<TKey>(key, ResourceActions.Removed));
                return true;
            }
            return false;
        }

        public override void Rename(TKey oldKey, TKey newKey)
        {
            var oldPath = GetAbsolutePath(oldKey);
            var newPath = GetAbsolutePath(newKey);

            Directory.Move(oldPath, newPath);
            RaiseRenamedGroup(new CopyResourceResultEventArgs<TKey>(oldKey, newKey));
        }
        public override IWorkplaceGroup<TKey, TResource> Group(TKey key)
        {
            var relativePath = ToRelativePath(key);
            var path = Path.Combine(Folder.FullName, relativePath);
            var k = CombineKey(relativePath);
            var folder = new DirectoryInfo(path);
            return CreateGroup(k, folder);
        }

        public override IWithGroupWorkplace<TKey, TResource> Create(TKey key)
        {
            var k = ToRelativePath(key);
            var path = Path.Combine(Folder.FullName, k);
            Directory.CreateDirectory(path);
            return Get(key);

        }
        public override IWithGroupWorkplace<TKey, TResource> Get(TKey key)
        {
            var relativePath = ToRelativePath(key);
            var path = GetAbsolutePath(key);
            var k = CombineKey(relativePath);
            var folder = new DirectoryInfo(path);
            if (!folder.Exists)
            {
                folder.Create();
            }
            return CreateWorkplace(k, folder);

        }
        protected virtual string CombineKey(string key)
        {
            var k = ToRelativePath(Key);
            if (string.IsNullOrEmpty(k))
            {
                k = key;
            }
            else
            {
                k = Path.Combine(k, key);
            }
            return k;
        }
        protected abstract IWithGroupWorkplace<TKey, TResource> CreateWorkplace(string key, DirectoryInfo folder);
        protected abstract IWorkplaceGroup<TKey, TResource> CreateGroup(string key, DirectoryInfo folder);

    }
}
