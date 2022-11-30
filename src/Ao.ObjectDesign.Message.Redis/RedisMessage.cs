using StackExchange.Redis;

namespace Ao.ObjectDesign.Message.Redis
{
    public readonly struct RedisMessage
    {
        public readonly RedisChannel Channel;

        public readonly RedisValue Value;

        public RedisMessage(RedisChannel channel, RedisValue value)
        {
            Channel = channel;
            Value = value;
        }
    }
}
