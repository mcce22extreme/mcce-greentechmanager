using System.Text;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using GreenTechManager.Core.Messages;

namespace GreenTechManager.Core.Processors
{
    public abstract class EventProcessorBase : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        private readonly IDictionary<string, EventProcessorHandler> _eventHandlers = new Dictionary<string, EventProcessorHandler>();

        public EventProcessorBase(string hostName, int port)
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
                _queueName = _channel.QueueDeclare().QueueName;

                _channel.QueueBind(queue: _queueName,
                    exchange: "trigger",
                    routingKey: "");

                Log.Information("Listenting on message bus!");

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Could not connect to message bus!");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Log.Information("Message bus connection shut down");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnMessageReceived;

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            var eventMessage = JsonConvert.DeserializeObject<EventMessage>(notificationMessage);

            if (_eventHandlers.TryGetValue(eventMessage.Event, out var handler))
            {
                handler.HandleEvent(notificationMessage);
            }
        }

        protected void RegisterEventHandler<T>(string eventType, Action<T> handler)
            where T : class
        {
            _eventHandlers.Add(eventType, new EventProcessorHandler<T>(handler));
        }

        private class EventMessage
        {
            public string Event { get; set; }
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
