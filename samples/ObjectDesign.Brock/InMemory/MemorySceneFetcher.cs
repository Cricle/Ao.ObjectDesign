using Ao.ObjectDesign.Session;
using ObjectDesign.Brock.Level;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace ObjectDesign.Brock.InMemory
{
    public class MemorySceneFetcher : ISceneFetcher<Scene>
    {
        public MemorySceneFetcher()
        {
            
            Datas = new Dictionary<string, Scene>();
        }

        public Dictionary<string, Scene> Datas { get; }

        public Scene Get(IFileInfo file)
        {
            if (Datas.TryGetValue(file.Name, out var scene))
            {
                return scene;
            }
            return null;
        }

    }
}
