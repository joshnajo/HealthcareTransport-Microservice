using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TripService.EventProcessing;
using System.Reflection;

namespace TripService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly IEventProcessor _eventProcessor;

        private IConnection _connection;
        private IChannel _channel;
        private string _queueName;

        public MessageBusSubscriber(
            IConfiguration config,
            IEventProcessor eventProcessor)
        {
            _config = config;
            _eventProcessor = eventProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeRabbitMQ();

            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            // Subscription part....
            consumer.ReceivedAsync += async (ModuleHandle, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Event received: {message}");

                    _eventProcessor.ProcessEvent(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }

            };

            await _channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: true,
                consumer: consumer
            );
            
            await Task.CompletedTask;
            Console.WriteLine("Listening on the message bus...");

            // // Keep the background service alive
            // await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(
                exchange: "trigger",
                type: ExchangeType.Fanout
            );

            var queueDeclare = await _channel.QueueDeclareAsync(
                durable: false,
                exclusive: false,
                autoDelete: true
            );

            _queueName = queueDeclare.QueueName;

            await _channel.QueueBindAsync(
                queue: _queueName,
                exchange: "trigger",
                routingKey: ""
            );

            _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;
        }

        private Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine(
                $"RabbitMQ connection shutdown. " +
                $"Initiator: {args.Initiator}, " +
                $"Code: {args.ReplyCode}, " +
                $"Text: {args.ReplyText}");

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
