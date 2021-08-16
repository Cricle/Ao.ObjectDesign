using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign.Working
{
    public abstract class PhysicalWorkplaceGroup<TKey,TResource> : WorkplaceGroup<TKey, TResource>
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
            :this(folder)
        {
            AcceptExtensions = acceptExtensions ?? throw new ArgumentNullException(nameof(acceptExtensions));
        }
        protected PhysicalWorkplaceGroup(TKey key, DirectoryInfo folder, IReadOnlyHashSet<string> acceptExtensions)
            : this(key,folder)
        {
            AcceptExtensions = acceptExtensions ?? throw new ArgumentNullException(nameof(acceptExtensions));
        }

        public DirectoryInfo Folder { get; }

        public bool AcceptAllFile { get; }

        public IReadOnlyHashSet<string> AcceptExtensions { get; }

        public override void Clear()
        {
            foreach (var item in Folder.EnumerateDirectories())
            {
                item.Delete(true);
            }
        }

        protected abstract string ToRelativePath(TKey key);

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
                return true;
            }
            return false;
        }

        public override void Rename(TKey oldKey, TKey newKey)
        {
            var oldPath = GetAbsolutePath(oldKey);
            var newPath = GetAbsolutePath(newKey);

            Directory.Move(oldPath, newPath);
        }
    }
}
