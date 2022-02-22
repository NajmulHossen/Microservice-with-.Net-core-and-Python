using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;
        public MovieController()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
            channel.BasicConsume(
            consumer: consumer,
            queue: replyQueueName,
            autoAck: true);
        }

        [HttpGet("GetMovieById/{id}")]
        public ActionResult GetMovieById(int id)
        {
            Movie movie = new ();
            movie.Id = id;
            var message = JsonSerializer.Serialize(movie);
            SendMessage(message);
            return Ok(respQueue.Take());
        }
        [HttpGet("GetMovie")]
        public ActionResult GetMovie()
        {
            var message = JsonSerializer.Serialize("GetAllMovie");
            SendMessage(message);
            return Ok(respQueue.Take());
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
            exchange: "",
            routingKey: "movie_queue",
            basicProperties: props,
            body: body);
            Console.WriteLine($"--> We have sent {message}");
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
        private void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (channel.IsOpen)
            {
                channel.Close();
                connection.Close();
            }
        }
    }
}
