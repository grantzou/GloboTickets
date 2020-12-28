using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Purchasing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host("rabbitmq://localhost");
                busConfig.ReceiveEndpoint("GloboTicket.Purchasing", endpointConfig =>
                {

                });
            });

            await bus.StartAsync();

            Console.WriteLine("Receiving messages. Press a key to stop.");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
