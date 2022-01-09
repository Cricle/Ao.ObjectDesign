using System;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace Ao.ObjectDesign.WpfDesign.Test
{
    internal static class ThreadRunner
    {
        public static Thread STA(Action action)
        {
            Exception ex = null;
            var thread = new Thread(_ =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    ex = e;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (ex != null)
            {
                ThrowExceptionPreservingStack(ex);
                throw ex;
            }
            return thread;
        }
        [ReflectionPermission(SecurityAction.Demand)]
        private static void ThrowExceptionPreservingStack(Exception exception)
        {
            FieldInfo remoteStackTraceString = typeof(Exception).GetField(
              "_remoteStackTraceString",
              BindingFlags.Instance | BindingFlags.NonPublic);
            remoteStackTraceString.SetValue(exception, exception.StackTrace + Environment.NewLine);
            throw exception;
        }
    }
}
