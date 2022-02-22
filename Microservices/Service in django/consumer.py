import pika, json, os, django

os.environ.setdefault("DJANGO_SETTINGS_MODULE", "MovieSerivce.settings")
django.setup()

from Movies.models import Movie,MovieOrder
from Movies.serializers import MovieSerlizer,MovieOrderSerlizer
# params = pika.URLParameters('amqp://guest:guest@localhost:5672')
params = pika.ConnectionParameters('localhost')

connection = pika.BlockingConnection(params)

channel = connection.channel()

channel.queue_declare(queue='movie_queue')
def on_request(ch, method, props, body):
    
    body=json.loads(body)
    if method.routing_key == "movie_queue" and body == "GetAllMovie":
        movie = Movie.objects.all()
        serializer = MovieSerlizer(movie,many=True)
        ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(correlation_id = props.correlation_id),
                     body=json.dumps(serializer.data))
        ch.basic_ack(delivery_tag=method.delivery_tag)
    elif method.routing_key == "movie_queue" and body["Id"]:
        movie = Movie.objects.get(id = body["Id"])
        serializer = MovieSerlizer(movie)
        finaldata =json.dumps(serializer.data, ensure_ascii=False).encode('utf-8')
        ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(correlation_id = props.correlation_id),
                     body=finaldata)
        ch.basic_ack(delivery_tag=method.delivery_tag)
    
channel.basic_qos(prefetch_count=1)
channel.basic_consume(queue='movie_queue', on_message_callback=on_request)


channel.queue_declare(queue='MovieOrder')
def callback(ch, method, properties, body):
        print('Received in MovieService')
        
        data = json.loads(body)
        
        if properties.content_type == 'PostMovieOrder':
            movie = Movie.objects.get(id=data['MovieId'])
            serializer = MovieSerlizer(movie)
            channel = connection.channel()
            movieorder = {
                "OrderName" : data['OrderName'],
                "MovieId" : data['MovieId'],
                "MovieName" : serializer.data["Title"]
                }
            serializer = MovieOrderSerlizer(data=movieorder)
            serializer.is_valid(raise_exception=True)
            serializer.save()
            print('Movie Order Created')

        elif properties.content_type == 'PutMovieOrder':
            movie = Movie.objects.get(id=data['MovieId'])
            serializer = MovieSerlizer(movie)
            channel = connection.channel()
            movieorder = {
                "id": data["Id"],
                "OrderName" : data['OrderName'],
                "MovieId" : data['MovieId'],
                "MovieName" : serializer.data["Title"]
                }
            if MovieOrder.objects.filter(id=data["Id"]).exists():
                movie = MovieOrder.objects.get(id=data["Id"])
                serializer = MovieOrderSerlizer(instance=movie,data=movieorder)
                serializer.is_valid(raise_exception=True)
                serializer.save()
                print('Movie Order Updated')
            else:
                serializer = MovieOrderSerlizer(data=movieorder)
                serializer.is_valid(raise_exception=True)
                serializer.save()
                print('Movie Order Created')


        elif properties.content_type == 'DeleteMovieOrder':
            channel = connection.channel()
            movie = MovieOrder.objects.get(id=data["Id"])
            movie.delete()
            print('Movie Order Deleted')

channel.basic_consume(queue='MovieOrder', on_message_callback=callback, auto_ack=True)

print('Started RPC and fanout Consuming')

channel.start_consuming()

channel.close()