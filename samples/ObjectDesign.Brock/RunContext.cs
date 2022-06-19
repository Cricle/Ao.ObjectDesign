using Ao.ObjectDesign.Session.Desiging;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Expressions;

namespace ObjectDesign.Brock
{
    public interface IRunContext
    {
        IDesignSession<Scene, UIElementSetting> Session { get; }
    }
    public class RunContext: IRunContext
    {
        public IDesignSession<Scene, UIElementSetting> Session { get; set; }

    }
    public interface ICodeRunner
    {
        object Run(string code,object selft, IRunContext context);
    }
    public class ZCodeRunner : ICodeRunner
    {
        private readonly Dictionary<string, Func<IRunContext,object, object>> runners = new Dictionary<string, Func<IRunContext, object, object>>();

        public static readonly string ContextName = "context";
        public static readonly string SelfName = "self";

        public object Run(string code, object self, IRunContext context)
        {
            if (!runners.TryGetValue(code, out var f))
            {
                f = Eval.Compile<Func<IRunContext, object, object>>(code, ContextName, SelfName);
            }
            return f(context, self);
        }
    }
}
