using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


var factory = new ConnectionFactory{ HostName = "localhost"};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "pub-sub", ExchangeType.Fanout);

var queue = channel.QueueDeclare(queue: "", exclusive: true);
var queueName = queue.QueueName;

channel.QueueBind(queue: queueName, exchange: "pub-sub", routingKey: "");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) => {
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"First Consumer - Received: {message}");
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer);
Console.ReadKey();
