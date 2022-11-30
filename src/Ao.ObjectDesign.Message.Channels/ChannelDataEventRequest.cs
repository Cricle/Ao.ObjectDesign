using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Channels
{
    public class ChannelDataEventRequest<T> : DefaultDataEventRequest<Channel<T>>
    {
        public static ChannelDataEventRequest<T> CreateBound(int capacity)
        {
            return new ChannelDataEventRequest<T>(true, new BoundedChannelOptions(capacity));
        }
        public static ChannelDataEventRequest<T> CreateBound(BoundedChannelOptions boundedOptions)
        {
            return new ChannelDataEventRequest<T>(true, boundedOptions);
        }
        public static ChannelDataEventRequest<T> CreateUnbound(UnboundedChannelOptions unboundedOptions, Action<T> itemDone)
        {
            return new ChannelDataEventRequest<T>(unboundedOptions, itemDone);
        }
        public static ChannelDataEventRequest<T> CreateUnbound(UnboundedChannelOptions unboundedOptions)
        {
            return new ChannelDataEventRequest<T>(unboundedOptions, _ => { });
        }
        public static ChannelDataEventRequest<T> CreateUnbound()
        {
            return new ChannelDataEventRequest<T>(new UnboundedChannelOptions(), _ => { });
        }

        private ChannelDataEventRequest(bool bounds, BoundedChannelOptions boundedOptions)
        {
            Bounds = bounds;
            BoundedOptions = boundedOptions;
        }

        private ChannelDataEventRequest(UnboundedChannelOptions unboundedOptions, Action<T> itemDone)
        {
            UnboundedOptions = unboundedOptions;
            ItemDone = itemDone;
        }


        public bool Bounds { get; }

        public BoundedChannelOptions BoundedOptions { get; }

        public UnboundedChannelOptions UnboundedOptions { get; }

        public Action<T> ItemDone { get; }

        public override Task<Channel<T>> CreatePipelineAsync()
        {
            if (Bounds)
            {
                return Task.FromResult(Channel.CreateBounded(BoundedOptions, ItemDone));
            }
            return Task.FromResult(Channel.CreateUnbounded<T>(UnboundedOptions));
        }
    }
}
