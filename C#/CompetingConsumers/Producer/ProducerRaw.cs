using System;
using System.Text;
using RabbitMQ.Client;

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

int messageID = 0;
var random =  new Random();


while (true){

    var publishingTime = random.Next(1, 3);

    var message = $"Sending message: MessageID[{messageID}]";
    var encodedMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("", "letterbox", null, encodedMessage);
    Console.WriteLine($"Send: {message}");
    
    Task.Delay(TimeSpan.FromSeconds(publishingTime)).Wait();

    messageID++;
}

