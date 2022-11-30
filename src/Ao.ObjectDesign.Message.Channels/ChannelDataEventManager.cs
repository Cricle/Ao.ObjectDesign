using System.Threading.Channels;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message.Channels
{
    public class ChannelDataEventManager<T> : DefaultDataEventManager<Channel<T>, T, ChannelDataEventRequest<T>>
    {
        public override async Task<IDataEventBody<long, Channel<T>, T, ChannelDataEventRequest<T>>> CreateAsync(ChannelDataEventRequest<T> request)
        {
            var eventWrapper = await request.CreatePipelineAsync();
            return new ChannelDataEventBody<T>(request, eventWrapper);
        }
    }
}
