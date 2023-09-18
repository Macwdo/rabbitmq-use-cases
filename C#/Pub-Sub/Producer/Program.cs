using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory{ HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "pub-sub", type: ExchangeType.Fanout);

var message = "Hello i want broadcast this message!";
var encodedMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "pub-sub", routingKey: "", basicProperties: null, body: encodedMessage);

Console.WriteLine($"Published Message '{message}'");