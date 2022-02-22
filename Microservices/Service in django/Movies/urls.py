
# from django.contrib import admin
from django.urls import path
from .models import Movie
from .views import MovieViewSet

urlpatterns = [
    path('movies', MovieViewSet.as_view({
        'get': 'list',
        'post': 'create'
        })),
    path('movies/<str:pk>', MovieViewSet.as_view({
        'get': 'retrieve',
        'put': 'update',
        'delete': 'destroy'
        })),
]
