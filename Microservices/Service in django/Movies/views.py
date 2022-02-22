from django.shortcuts import render
from rest_framework import viewsets,status
from rest_framework.response import Response
from rest_framework.views import APIView
from .serializers import MovieSerlizer,MovieOrderSerlizer
from .models import Movie,MovieOrder
import pika, json
# Create your views here.
connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))



class MovieViewSet(viewsets.ViewSet):
    #api/movies
    def list (self, request):
        movies = Movie.objects.all()
        serializer = MovieSerlizer(movies,many=True)
        return Response(serializer.data)
    #api/movies
    def create(self, request):
        channel = connection.channel()
        serializer = MovieSerlizer(data=request.data)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data,status=status.HTTP_201_CREATED)
    #api/movie/<str:id>
    def retrieve(self, request, pk = None):
        movie = Movie.objects.get(id=pk)
        serializer = MovieSerlizer(movie)
        return Response(serializer.data)
    #api/movie/<str:id>
    def update(self, request, pk = None):
        channel = connection.channel()
        movie = Movie.objects.get(id=pk)
        serializer = MovieSerlizer(instance=movie,data=request.data)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data,status=status.HTTP_202_ACCEPTED)
    #api/movie/<str:id>
    def destroy(self, request, pk = None):
        channel = connection.channel()
        movie = Movie.objects.get(id=pk)
        movie.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)

#MovieOrder
class MovieOrderViewSet(viewsets.ViewSet):
    #api/movieorders
    def list (self, request):
        movieorders = MovieOrder.objects.all()
        serializer = MovieOrderSerlizer(movieorders,many=True)
        return Response(serializer.data)
    #api/movieorders
    def create(self, request):
        channel = connection.channel()
        serializer = MovieOrderSerlizer(data=request.data)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data,status=status.HTTP_201_CREATED)
    #api/movie/<str:id>
    def retrieve(self, request, pk = None):
        movie = MovieOrder.objects.get(id=pk)
        serializer = MovieOrderSerlizer(movie)
        return Response(serializer.data)
    #api/movie/<str:id>
    def update(self, request, pk = None):
        channel = connection.channel()
        movie = MovieOrder.objects.get(id=pk)
        serializer = MovieOrderSerlizer(instance=movie,data=request.data)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data,status=status.HTTP_202_ACCEPTED)
    #api/movie/<str:id>
    def destroy(self, request, pk = None):
        channel = connection.channel()
        movie = MovieOrder.objects.get(id=pk)
        movie.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)
