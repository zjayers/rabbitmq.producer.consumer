using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    class Sender
    {
        public static void Main(string[] args)
        {
            // Create a Rabbit MQ Connection Factory
            var factory = new ConnectionFactory() {
                HostName = "localhost",
                UserName = "user",
                Password = "0okmNJI9",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };

            // Create the RabbitMQ Connection & Channel
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);

                // Create a basic message
                string message = "Getting started with .NET Core RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", "BasicTest", null, body);
                Console.WriteLine("Send message {0}...", message);
            }

            Console.WriteLine("Press any key to exit the Sender App...");
            Console.ReadLine();
        }
    }
}