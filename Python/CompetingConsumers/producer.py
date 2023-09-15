from random import randint
from time import sleep
import pika

connection_parameters = pika.ConnectionParameters(host='localhost')

connection = pika.BlockingConnection(connection_parameters) 

channel = connection.channel()

channel.queue_declare(queue='letterbox')

message_id = 0

while True:
    message = f'MessageID[{message_id}] Sending message...'

    channel.basic_publish(
        exchange='',
        routing_key='letterbox',
        body=message
    )

    print(f"Sent message: {message}")
    sleep(randint(1, 2))

    message_id += 1