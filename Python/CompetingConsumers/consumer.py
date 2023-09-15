import pika
from time import sleep
from random import randint


def callback(ch, method, properties, body):
    processing_time= randint(1, 5)
    print("----")
    print(f"Processing time: {processing_time}")
    print(f"{body}")
    sleep(processing_time)
    ch.basic_ack(delivery_tag=method.delivery_tag)
    print(f"Finished processing the message")

connection_parameters = pika.ConnectionParameters(host='localhost')

connection = pika.BlockingConnection(connection_parameters) 

channel = connection.channel()

channel.queue_declare(queue='letterbox')

channel.basic_qos(prefetch_count=1)

channel.basic_consume(queue="letterbox", on_message_callback=callback)

print("Waiting for messages...")

channel.start_consuming()