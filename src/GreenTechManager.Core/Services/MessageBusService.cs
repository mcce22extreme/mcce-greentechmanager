using System.Text;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;

namespace GreenTechManager.Core.Services
{
    public interface IMessageBusService
    {
        void PublishMessage<TMessage>(TMessage message) where TMessage : IEventMessage;
    }

    public class MessageBusService : IMessageBusService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusService(string hostName, int port)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    Port = port
                };

                Log.Information("Connecting to message bus...");

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: MessageBusConstants.EXCHANGE_ENTITY_EVENT, type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Log.Information("Successfully connected to message bus!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Could not connect to message bus!");
            }
        }

        public void PublishMessage<TMessage>(TMessage message)
            where TMessage : IEventMessage
        {
            if (_connection?.IsOpen == true)
            {
                Log.Debug("Sending message to message bus...");

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                _channel.BasicPublish(
                    exchange: MessageBusConstants.EXCHANGE_ENTITY_EVENT,
                    routingKey: string.Empty,
                    basicProperties: null,
                    body: body);

                Log.Debug("Successfully sent message to message bus.");
            }
            else
            {
                Log.Error("Could not send message to message bus. Connection to message bus is not active!");
            }
        }

        public void Dispose()
        {
            Log.Debug("Disposing message bus service");

            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Log.Information("Message bus connection shut down");
        }
    }
}
