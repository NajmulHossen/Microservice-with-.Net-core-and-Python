using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using OrderService.Repositories;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieOrderController : ControllerBase
    {
        private IMovieOrderRepository _repository;
        private readonly IConnection connection;
        private readonly IModel channel;
        public MovieOrderController(IMovieOrderRepository repository)
        {
            _repository = repository;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        [HttpPost("PostMovieOrder")]
        public async Task<ActionResult<MovieOrder>> PostBookOrder(MovieOrder order)
        {
            try
            {
                order.State = OrderState.Submitted;
                var message = JsonSerializer.Serialize(order);
                try
                {
                    order.State = OrderState.Accepted;
                    Publish("PostMovieOrder", message);
                    Dispose();
                }
                catch
                {
                    order.State = OrderState.Submitted;
                }
                return await Task.FromResult(_repository.PostMovieOrder(order));
            }
            catch
            {
                order.State = OrderState.UnSubmitted;
                _repository.PostMovieOrder(order);
                return BadRequest(order);
            }

        }
        [HttpPut("PutMovieOrder")]
        public async Task<ActionResult<MovieOrder>> PutMovieOrder(MovieOrder order)
        {
            try
            {
                order.State = OrderState.Submitted;
                var message = JsonSerializer.Serialize(order);
                try
                {
                    order.State = OrderState.Accepted;
                    Publish("PutMovieOrder", message);
                    Dispose();
                }
                catch
                {
                    order.State = OrderState.Submitted;
                }
                
                return await Task.FromResult(_repository.PutMovieOrder(order));
            }
            catch
            {
                order.State = OrderState.UnSubmitted;
                _repository.PutMovieOrder(order);
                return BadRequest(order);
            }
        }

        private void Publish(string propertie, string message)
        {
            
            //channel.ExchangeDeclare(exchange: "",
            //                        type: "direct");
            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.ContentType = propertie;
            channel.BasicPublish(exchange: "",
                                 routingKey: "MovieOrder",
                                 basicProperties: properties,
                                 body: body);
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
