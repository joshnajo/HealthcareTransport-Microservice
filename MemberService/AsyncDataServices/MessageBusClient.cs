using System.Text;
using System.Text.Json;
using MemberService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MemberService.AsyncDataServices
{
    public class MessageBusClient :  IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        //Set up connection to message bus   
        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new NullReferenceException();
            string hostName = _configuration["RabbitMQHost"] ?? throw new InvalidOperationException("RabbitMQHost is not configured");
            string port = _configuration["RabbitMQPort"] ?? throw new InvalidOperationException("RabbitMQPort is not configured");
            
            //RabbitMQ - connection
            var connectionFactory = new ConnectionFactory()
                { 
                    HostName = hostName, 
                    Port = int.Parse(port)
                };

            try
            {
                // Create connection, channel and exchange
                _connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
                _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
                _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout).GetAwaiter().GetResult();
                
                //subscribe to connection shutdown event
                _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("Connected to message Bus.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not connet to Message Bus: {ex.Message}");
            }
        }

        // Once the member is created event has to be published and sent to message bus
        public void PublishNewMember(MemberPublishedDto memberPublishedDto)
        {
            // throw new NotImplementedException();

            // create a message object which should be serialized version of member publish dto 
            var message = JsonSerializer.Serialize<MemberPublishedDto>(memberPublishedDto);
            if(_connection.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection open, sending message...");
                SendMessage(message);
            }
            else{
                Console.WriteLine("RabbitMQ Connection is closed, not sending message...");
            }

        }

        /// <summary>
        /// send message to rabbimq
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            // Create properties
            var props = new BasicProperties
            {
                Persistent = true // Optional: ensures message survives broker restart
            };

            var body = Encoding.UTF8.GetBytes(message);    
            _channel.BasicPublishAsync(
                        exchange: "trigger",
                        routingKey: "",
                        mandatory: false,
                        basicProperties: props,
                        body:body
                        );
            Console.WriteLine($"Message {message} sent to RabbitMQ.");
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

        // private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
        // {
        //     Console.WriteLine(
        //         $"RabbitMQ connection shutdown. " +
        //         $"Initiator: {args.Initiator}, " +
        //         $"Code: {args.ReplyCode}, " +
        //         $"Text: {args.ReplyText}"
        //     );

        //     // example: delay / reconnect / notify
        //     await Task.Delay(1000);
        // }

        public void Dispose()
        {
             Console.WriteLine("MessageBus disposed");
             if(_channel.IsOpen)
             {
                _channel.CloseAsync();
                _connection.CloseAsync();
            }
        }
    }
}