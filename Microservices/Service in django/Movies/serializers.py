from rest_framework import serializers
from .models import Movie,MovieOrder

class MovieSerlizer(serializers.ModelSerializer):
    class Meta:
        model = Movie
        fields = '__all__'

class MovieOrderSerlizer(serializers.ModelSerializer):
    class Meta:
        model = MovieOrder
        fields = '__all__'