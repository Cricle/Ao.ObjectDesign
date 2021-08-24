using Ao.ObjectDesign.Designing.Working;
using System.IO;

namespace Ao.ObjectDesign.Designing.Test.Working
{
    internal class ValuePhysicalWorkplaceGroup : PhysicalWorkplaceGroup<string, string>
    {
        public ValuePhysicalWorkplaceGroup(DirectoryInfo folder) : base(folder)
        {
        }

        public ValuePhysicalWorkplaceGroup(string key, DirectoryInfo folder) : base(key, folder)
        {
        }

        public ValuePhysicalWorkplaceGroup(DirectoryInfo folder, IReadOnlyHashSet<string> acceptExtensions) : base(folder, acceptExtensions)
        {
        }

        public ValuePhysicalWorkplaceGroup(string key, DirectoryInfo folder, IReadOnlyHashSet<string> acceptExtensions) : base(key, folder, acceptExtensions)
        {
        }

        protected override IWorkplaceGroup<string, string> CreateGroup(string key, DirectoryInfo folder)
        {
            return new ValuePhysicalWorkplaceGroup(key,folder);
        }

        protected override IWithGroupWorkplace<string, string> CreateWorkplace(string key, DirectoryInfo folder)
        {
            return new ValuePhysicalWorkplace(Key, this, folder);
        }

        protected override string ToKey(string folderPath)
        {
            return Path.GetFileName(folderPath);
        }

        protected override string ToRelativePath(string key)
        {
            return key;
        }
    }
}
