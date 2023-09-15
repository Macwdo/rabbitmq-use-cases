using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory{ HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "letterbox",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null     
);

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);;

var consumer = new EventingBasicConsumer(channel);

var random = new Random();

consumer.Received += (model, ea) => {
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    var timeToProcess = random.Next(1, 3);
        
    Console.WriteLine($"Received: {message}");
    Task.Delay(TimeSpan.FromSeconds(timeToProcess)).Wait();
    Console.WriteLine($"Finished: {timeToProcess}");

    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};

channel.BasicConsume(
    queue: "letterbox",
    autoAck: false,
    consumer: consumer
);

Console.ReadKey();

