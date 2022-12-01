using NATS.Client;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Nats
{
    public class NatsDataEventBody : DefaultDataEventBody<string, NatsEventPipeline, Msg, NatsDataEventRequest>
    {
        private IAsyncSubscription sub;

        public NatsDataEventBody(NatsDataEventRequest request, NatsEventPipeline eventPipline)
            : base(request, eventPipline)
        {
        }

        protected override Task<bool> OnStartAsync()
        {
            sub = EventPipline.Connection.SubscribeAsync(Request.Id);
            sub.MessageHandler += OnMessageHandler;
            sub.Start();
            return Task.FromResult(true);
        }

        private void OnMessageHandler(object sender, MsgHandlerEventArgs e)
        {
            RaiseMessageReceived(new NatsDataMessage(this, e.Message));
        }

        protected override Task<bool> OnStopAsync()
        {
            sub.MessageHandler -= OnMessageHandler;
            return Task.FromResult(true);
        }
        protected override void OnDisposed()
        {
            sub?.Dispose();
            base.OnDisposed();
        }
    }
}
