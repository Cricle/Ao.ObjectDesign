using Ao.Project;
using Ao.Project.NewtonsoftJson;
using System.IO.Abstractions;

namespace ObjectDesign.Projecting
{
    public class JsonProjectManager : ProjectManager
    {
        public JsonProjectManager(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        protected override IProjectInterop GetProjectInterop(IFileInfo fileInfo)
        {
            return JsonProjectInterop.Instance;
        }
    }
}
