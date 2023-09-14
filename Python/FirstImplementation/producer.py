import pika

connection_parameters = pika.ConnectionParameters(host='localhost')

connection = pika.BlockingConnection(connection_parameters) 

channel = connection.channel()

channel.queue_declare(queue='letterbox')
message = 'Hello World!'

channel.basic_publish(
    exchange='',
    routing_key='letterbox',
    body=message
)

print(f"Sent message: {message}")

connection.close()
