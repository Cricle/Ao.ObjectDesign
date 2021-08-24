using Ao.ObjectDesign.Designing.Working;
using System.IO;

namespace Ao.ObjectDesign.Designing.Test.Working
{
    internal class ValuePhysicalWorkplace : WithGroupPhysicalWorkplace<string, string>
    {
        public ValuePhysicalWorkplace(string groupKey, IWorkplaceGroup<string, string> group, DirectoryInfo folder) : base(groupKey, group, folder)
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

        protected override string ToResource(string key, string path)
        {
            return File.ReadAllText(path);
        }

        protected override void WriteResource(string key, Stream stream, string resource)
        {
            using (var sw=new StreamWriter(stream))
            {
                sw.Write(resource);
            }
        }
    }
}
