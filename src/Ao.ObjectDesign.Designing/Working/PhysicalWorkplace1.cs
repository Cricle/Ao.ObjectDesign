using System.IO;

namespace Ao.ObjectDesign.Designing.Working
{
    public class PhysicalWorkplace : WithGroupPhysicalWorkplace<string, Stream>
    {
        public PhysicalWorkplace(string groupKey, IWorkplaceGroup<string, Stream> group, DirectoryInfo folder) : base(groupKey, group, folder)
        {
        }

        protected override string ToKey(string relativePath)
        {
            return relativePath;
        }

        protected override string ToRelativePath(string key)
        {
            return key;
        }

        protected override Stream ToResource(string key, string path)
        {
            return File.Open(path, FileMode.Open);
        }

        protected override void WriteResource(string key, Stream stream, Stream resource)
        {
            stream.CopyTo(resource);
        }
    }
}
