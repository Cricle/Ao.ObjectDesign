using StackExchange.Redis;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Redis
{
    public class RedisDataEventRequest : DefaultDataEventRequest<RedisChannel, RedisEventPipeline>
    {
        public RedisDataEventRequest(IConnectionMultiplexer connectionMultiplexer, RedisChannel id)
        {
            ConnectionMultiplexer = connectionMultiplexer;
            Id = id;
        }

        public IConnectionMultiplexer ConnectionMultiplexer { get; }

        public override RedisChannel Id { get; }

        public override Task<RedisEventPipeline> CreatePipelineAsync()
        {
            return Task.FromResult(new RedisEventPipeline(ConnectionMultiplexer,ConnectionMultiplexer.GetSubscriber()));
        }
    }
}
