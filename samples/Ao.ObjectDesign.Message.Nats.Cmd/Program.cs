using NATS.Client;
using System;
using System.Text;
using System.Threading.Channels;

namespace Ao.ObjectDesign.Message.Nats.Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }
        private static async Task Run()
        {
            var nat = new ConnectionFactory();
            var conn = nat.CreateConnection("127.0.0.1:4222");
            var mgr = new NatsDataEventManager();
            var body = await mgr.CreateAsync(new NatsDataEventRequest("aaa", conn));
            await body.StartAsync();
            body.MessageReceived += Body_MessageReceived; ;

            for (int i = 0; i < 10; i++)
            {
                body.EventPipline.Connection.Publish("aaa", Encoding.UTF8.GetBytes(i.ToString()));
            }
            Console.ReadLine();
            await body.StopAsync();
            body.Dispose();
            mgr.Dispose();

        }

        private static void Body_MessageReceived(object? sender, IDataMessage<string, NatsEventPipeline, Msg, NatsDataEventRequest> e)
        {
            Console.WriteLine(e.Raw.ToString());
        }
    }
}