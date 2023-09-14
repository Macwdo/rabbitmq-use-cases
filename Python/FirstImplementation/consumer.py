import pika

def callback(ch, method, properties, body):
    print(f"Received message: {body}")

connection_parameters = pika.ConnectionParameters(host='localhost')

connection = pika.BlockingConnection(connection_parameters) 

channel = connection.channel()

channel.queue_declare(queue='letterbox')
channel.basic_consume(queue="letterbox", on_message_callback=callback, auto_ack=True)

print("Waiting for messages...")

channel.start_consuming()