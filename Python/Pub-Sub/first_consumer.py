import pika
from pika.exchange_type import ExchangeType


def callback(ch, method, properties, body):
    print(f"First Consumer - Received message: {body}")

connection_parameters = pika.ConnectionParameters(host='localhost')

connection = pika.BlockingConnection(connection_parameters) 

channel = connection.channel()

channel.exchange_declare(exchange='pub-sub',exchange_type=ExchangeType.fanout)

queue = channel.queue_declare(queue='', exclusive=True)
queue_name = queue.method.queue

channel.queue_bind(exchange='pub-sub', queue=queue_name)

channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)

print("Waiting for messages...")

channel.start_consuming()
