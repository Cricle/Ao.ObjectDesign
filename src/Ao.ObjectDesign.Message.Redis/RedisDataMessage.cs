using StackExchange.Redis;

namespace Ao.ObjectDesign.Message.Redis
{
    public class RedisDataMessage : DefaultDataMessage<RedisChannel, RedisEventPipeline, RedisMessage, RedisDataEventRequest>
    {
        public RedisDataMessage(IDataEventBody<RedisChannel, RedisEventPipeline, RedisMessage, RedisDataEventRequest> body, RedisMessage message)
            : base(body, message)
        {
        }
    }
}
