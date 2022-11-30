using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Channels
{
    public class ChannelDataMessage<T> : DefaultDataMessage<Channel<T>, T, ChannelDataEventRequest<T>>
    {
        public ChannelDataMessage(IDataEventBody<long, Channel<T>, T, ChannelDataEventRequest<T>> body, T message) : base(body, message)
        {
        }
    }
    public class ChannelDataEventBody<T> : DefaultDataEventBody<Channel<T>, T, ChannelDataEventRequest<T>>
    {
        public ChannelDataEventBody(ChannelDataEventRequest<T> request, Channel<T> eventPipline) : base(request, eventPipline)
        {
        }

        private Task raiseEventTask;

        private CancellationTokenSource tokenSource;

        protected override Task<bool> OnStartAsync()
        {
            tokenSource?.Dispose();
            tokenSource = new CancellationTokenSource();
            raiseEventTask = Task.Factory.StartNew(RaisingMessage, tokenSource.Token);
            return Task.FromResult(true);
        }

        protected override Task<bool> OnStopAsync()
        {
            return Task.FromResult(true);
        }

        protected virtual void HandleException(T data, Exception exception)
        {

        }

        protected async void RaisingMessage(object input)
        {
            var pipline = EventPipline;
            var tk = (CancellationToken)input;
            T data = default;
            while (!tk.IsCancellationRequested)
            {
                try
                {
                    if (await pipline.Reader.WaitToReadAsync() && pipline.Reader.TryRead(out data))
                    {
                        RaiseMessageReceived(new ChannelDataMessage<T>(this, data));
                    }
                }
                catch (Exception ex)
                {
                    HandleException(data, ex);
                }
            }
        }

        protected override void OnDisposed()
        {
            EventPipline.Writer.Complete();
            tokenSource?.Cancel();
        }
    }
}
