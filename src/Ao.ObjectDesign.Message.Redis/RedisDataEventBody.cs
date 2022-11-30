using StackExchange.Redis;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Redis
{
    public class RedisDataEventBody : DefaultDataEventBody<RedisChannel,RedisEventPipeline, RedisMessage, RedisDataEventRequest>
    {
        public RedisDataEventBody(RedisDataEventRequest request, RedisEventPipeline eventPipline)
            : base(request, eventPipline)
        {
        }

        protected override Task<bool> OnStartAsync()
        {
            EventPipline.Subscriber.Subscribe(Request.Id, Handle);
            return Task.FromResult(true);
        }

        protected override Task<bool> OnStopAsync()
        {
            EventPipline.Subscriber.Unsubscribe(Request.Id, Handle);
            return Task.FromResult(true);
        }

        private void Handle(RedisChannel channel,RedisValue value)
        {
            RaiseMessageReceived(new RedisDataMessage(this, new RedisMessage(channel, value)));
        }
    }
}
