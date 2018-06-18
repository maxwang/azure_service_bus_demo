using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessageProducer
{
    class Program
    {
        
        const string QueueName = "edm.order.zohocrm.job";
        static IQueueClient queueClient;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var environmentName = Environment.GetEnvironmentVariable("CORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true);

            var configuration = builder.Build();

            var serviceBusConnectionString = configuration["ServiceBusConnectionString"];
            queueClient = new QueueClient(serviceBusConnectionString, QueueName);

            var message = new Message();
            message.UserProperties.Add("OrderId", "9005");
            message.Body = Encoding.UTF8.GetBytes("5005");
            
            await queueClient.SendAsync(message);

            Console.ReadKey();
            
            await queueClient.CloseAsync();
        }
    }
}
