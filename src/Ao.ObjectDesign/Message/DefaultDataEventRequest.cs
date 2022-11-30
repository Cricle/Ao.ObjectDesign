using System;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    public abstract class DefaultDataEventRequest<TEventPipline> : DefaultDataEventRequest<long, TEventPipline>
    {
        public override long Id => GuidToLongHelper.Generate();
    }
    public abstract class DefaultDataEventRequest<TId, TEventPipline> : IDataEventRequest<TId, TEventPipline>
    {
        public abstract TId Id { get; }

        public abstract Task<TEventPipline> CreatePipelineAsync();

        public void Dispose()
        {
            OnDisposed();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDisposed()
        {

        }
    }
}
