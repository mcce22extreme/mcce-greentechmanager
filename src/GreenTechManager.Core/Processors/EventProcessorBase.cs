using System.Text;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Enums;
using GreenTechManager.Core.Messages;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace GreenTechManager.Core.Processors
{
    public abstract class EventProcessorBase : BackgroundService
    {
        private const int MAX_CONNECTION_ATTEMPTS = 3;

        private readonly IDictionary<EventType, EventProcessorHandler> _eventHandlers = new Dictionary<EventType, EventProcessorHandler>();
        private readonly string _hostName;
        private readonly int _port;

        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public EventProcessorBase(string hostName, int port)
        {
            _hostName = hostName;
            _port = port;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Log.Information("Message bus connection shut down");
        }

        private async Task<bool> EstablishConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    Port = _port
                };
                var retryCount = 0;

                do
                {
                    try
                    {
                        retryCount++;

                        Log.Information($"Connecting to message bus (attempt {retryCount}/{MAX_CONNECTION_ATTEMPTS}...");

                        _connection = factory.CreateConnection();
                        _channel = _connection.CreateModel();

                        _channel.ExchangeDeclare(exchange: MessageBusConstants.EXCHANGE_ENTITY_EVENT, type: ExchangeType.Fanout);
                        _queueName = _channel.QueueDeclare().QueueName;

                        _channel.QueueBind(
                            queue: _queueName,
                            exchange: MessageBusConstants.EXCHANGE_ENTITY_EVENT,
                            routingKey: string.Empty);

                        Log.Information("Successfully connected to message bus!");

                        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Could not connect to message bus!");
                        await Task.Delay(3000);
                    }
                } while (retryCount < MAX_CONNECTION_ATTEMPTS);

                return false;
            }

            return true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (await EstablishConnection())
            {
                stoppingToken.ThrowIfCancellationRequested();

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += OnMessageReceived;

                _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
        }

        protected virtual void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            var eventMessage = JsonConvert.DeserializeObject<EventMessage>(notificationMessage);

            if (_eventHandlers.TryGetValue(eventMessage.EventType, out var handler))
            {
                handler.HandleEvent(notificationMessage);
            }
        }

        protected void RegisterEventHandler<T>(EventType eventType, Action<T> handler)
            where T : class
        {
            _eventHandlers.Add(eventType, new EventProcessorHandler<T>(handler));
        }

        private abstract class EventProcessorHandler
        {
            public abstract void HandleEvent(string message);
        }

        private class EventProcessorHandler<T> : EventProcessorHandler
        {
            private readonly Action<T> _action;

            public EventProcessorHandler(Action<T> action)
            {
                _action = action;
            }

            public override void HandleEvent(string message)
            {
                var obj = JsonConvert.DeserializeObject<T>(message);
                _action(obj);
            }
        }
    }
}
