using StackExchange.Redis;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Redis
{
    public class RedisDataEventManager : DefaultDataEventManager<RedisChannel, RedisEventPipeline, RedisMessage, RedisDataEventRequest>
    {
        public RedisDataEventManager(IConnectionMultiplexer connectionMultiplexer)
        {
            ConnectionMultiplexer = connectionMultiplexer;
        }

        public IConnectionMultiplexer ConnectionMultiplexer { get; }
        
        public override async Task<IDataEventBody<RedisChannel, RedisEventPipeline, RedisMessage, RedisDataEventRequest>> CreateAsync(RedisDataEventRequest request)
        {
            var pipline = await request.CreatePipelineAsync();
            return new RedisDataEventBody(request, pipline);
        }
    }
}
