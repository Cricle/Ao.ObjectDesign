using NATS.Client;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Nats
{
    public class NatsDataEventRequest : DefaultDataEventRequest<string, NatsEventPipeline>
    {
        public NatsDataEventRequest(string id, IConnection connection)
        {
            Id = id;
            Connection = connection;
        }

        public override string Id { get; }

        public IConnection Connection { get; }

        public override Task<NatsEventPipeline> CreatePipelineAsync()
        {
            return Task.FromResult(new NatsEventPipeline(Id,Connection));
        }
    }
}
