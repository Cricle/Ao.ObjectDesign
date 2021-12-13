using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.Wpf.Store;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Wpf.Json
{
    internal static class Json
    {
        public static readonly JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
    }
}
