using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessageProducer
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://edmorder.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=qdClUmM1r1mUtPJKkAOPeY/1O1OhChLJSPEs2QjhWr8=";
        const string QueueName = "edm.order.zohocrm.job";
        static IQueueClient queueClient;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            await queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes("5005")));

            Console.ReadKey();



            await queueClient.CloseAsync();
        }
    }
}
