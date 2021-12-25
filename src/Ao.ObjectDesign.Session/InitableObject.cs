using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public class InitableObject :DependencyObject, IInitableObject, IDisposable
    {
        private bool isInitialized;
        private bool disposedValue;

        public bool IsInitialized => isInitialized;

        public bool IsDisposed => disposedValue;

        public void Initialize()
        {
            if (isInitialized)
            {
                throw new InvalidOperationException("This instance is initialized, please see property IsInitialized");
            }
            OnInitialize();
            isInitialized = true;
        }

        protected virtual void OnInitialize()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        ~InitableObject()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected void ThrowIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
