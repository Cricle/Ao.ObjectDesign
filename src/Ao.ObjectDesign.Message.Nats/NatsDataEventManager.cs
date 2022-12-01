using NATS.Client;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Nats
{
    public class NatsDataEventManager : DefaultDataEventManager<string, NatsEventPipeline, Msg, NatsDataEventRequest>
    {
        public override async Task<IDataEventBody<string, NatsEventPipeline, Msg, NatsDataEventRequest>> CreateAsync(NatsDataEventRequest request)
        {
            var eventPipeline = await request.CreatePipelineAsync();
            return new NatsDataEventBody(request, eventPipeline);
        }
    }
}
