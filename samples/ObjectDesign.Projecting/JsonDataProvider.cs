using Ao.ObjectDesign.Sources;
using Ao.Project;
using Newtonsoft.Json.Linq;
using System;

namespace ObjectDesign.Projecting
{
    public class JsonDataProvider : DataProviderProperty
    {
        protected override void DoWithString(string value)
        {
            GetDataProviderGroup()
                .Add(new InMemoryDataProvider { Value = JObject.Parse(value), Name = Name });
        }
    }
}
