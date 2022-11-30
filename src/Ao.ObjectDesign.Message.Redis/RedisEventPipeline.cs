using StackExchange.Redis;

namespace Ao.ObjectDesign.Message.Redis
{
    public class RedisEventPipeline
    {
        public RedisEventPipeline(IConnectionMultiplexer connectionMultiplexer, ISubscriber subscriber)
        {
            ConnectionMultiplexer = connectionMultiplexer;
            Subscriber = subscriber;
        }

        public IConnectionMultiplexer ConnectionMultiplexer { get; }

        public ISubscriber Subscriber { get; }
    }
}
