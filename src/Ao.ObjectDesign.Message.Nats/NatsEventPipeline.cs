using NATS.Client;

namespace Ao.ObjectDesign.Message.Nats
{
    public class NatsEventPipeline
    {
        public NatsEventPipeline(string id, IConnection connection)
        {
            Id = id;
            Connection = connection;
        }

        public string Id { get; }

        public IConnection Connection { get; }
    }
}
