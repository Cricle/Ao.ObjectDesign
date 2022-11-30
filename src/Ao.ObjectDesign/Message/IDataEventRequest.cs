using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    public interface IDataEventRequest<TId, TEventPipline> : IDisposable
    {
        TId Id { get; }

        Task<TEventPipline> CreatePipelineAsync();
    }
}
