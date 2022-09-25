using System.Text;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;

namespace GreenTechManager.Core.Services
{
    public interface IMessageBusService
    {
        void PublishMessage<TMessage>(TMessage message);
    }

    public class MessageBusService : IMessageBusService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusService(string hostName, int port)
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                Port = port
            };
            try
            {
                Log.Information("Connecting to message bus...");

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Log.Information("Successfully connected to message bus!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Could not connect to message bus!");
            }
        }

        public void PublishMessage<TMessage>(TMessage message)
        {
            var json = JsonConvert.SerializeObject(message);

            if (_connection.IsOpen)
            {
                Log.Debug("Sending message to message bus...");

                SendMessage(json);

                Log.Debug("Successfully sent message to message bus.");
            }
            else
            {
                Log.Error("Could not send message to message bus. Connection to message bus is not active!");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);

            Log.Debug($"Message: {message}");

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
