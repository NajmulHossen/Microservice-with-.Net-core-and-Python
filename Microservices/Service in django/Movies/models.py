from django.db import models
import json

# Create your models here.
class Movie(models.Model):
    Title = models.CharField(max_length=200)
    ReleaseDate = models.CharField(max_length=200)
    Genre = models.CharField(max_length=200)
        
class MovieOrder(models.Model):
    OrderName = models.CharField(max_length=200)
    MovieId = models.PositiveIntegerField()
    MovieName = models.CharField(max_length=200)