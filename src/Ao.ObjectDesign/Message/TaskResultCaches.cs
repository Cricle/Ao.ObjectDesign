using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    internal static class TaskResultCaches
    {
        public static readonly Task<bool> trueTask = Task.FromResult(true);

        public static readonly Task<bool> falseTask = Task.FromResult(false);
    }
}
