using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public class CreateTemplateContextsResult
    {
        public CreateTemplateContextsResult(IReadOnlyList<WpfForViewBuildContextBase> contexts, IDisposable subscriber)
        {
            Contexts = contexts;
            Subscriber = subscriber;
        }

        public IReadOnlyList<WpfForViewBuildContextBase> Contexts { get; }

        public IDisposable Subscriber { get; }
    }
}
