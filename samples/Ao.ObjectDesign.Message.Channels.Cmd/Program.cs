using System.Threading.Channels;

namespace Ao.ObjectDesign.Message.Channels.Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }
        private static async Task Run()
        {
            var mgr = new ChannelDataEventManager<int>();
            var body = await mgr.CreateAsync(ChannelDataEventRequest<int>.CreateUnbound());
            await body.StartAsync();
            body.MessageReceived += OnMessageReceived;

            for (int i = 0; i < 1_000_000; i++)
            {
                body.EventPipline.Writer.TryWrite(i);
            }
            while (body.EventPipline.Reader.Count != 0)
            {
                await Task.Yield();
            }

            await body.StopAsync();
            body.Dispose();
            mgr.Dispose();

        }
        private static void OnMessageReceived(object? sender, IDataMessage<long, Channel<int>, int, ChannelDataEventRequest<int>> e)
        {
            //Console.WriteLine($"Received {e.Message.Raw} !");
        }

    }
}