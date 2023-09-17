import pika
from pika.exchange_type import ExchangeType

connection_parameters = pika.ConnectionParameters(host='localhost')

connection = pika.BlockingConnection(connection_parameters) 

channel = connection.channel()

channel.exchange_declare(exchange='pub-sub',exchange_type=ExchangeType.fanout)

message = 'Hello i want to broadcast this message!'

channel.basic_publish(
    exchange='pub-sub',
    routing_key='',
    body=message
)

print(f"Sent message: {message}")

connection.close()
