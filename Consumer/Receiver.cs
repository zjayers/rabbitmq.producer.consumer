using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    class Receiver
    {
        static void Main(string[] args)
        {
            // Create a Rabbit MQ Connection Factory
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "0okmNJI9",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest", false, false, false, null);

                    // Setup the consumer
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine("Received message: {0}...", message);
                    };

                    channel.BasicConsume("BasicTest", true, consumer);

                    Console.WriteLine("Press any key to exit the Receiver App...");
                    Console.ReadLine();
                }
            }
        }
    }
}