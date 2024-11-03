using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using GroceryShop.Models;

public class RabbitMQService {
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService() {
        var factory = new ConnectionFactory() {HostName = "localhost"};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "inventoryQueue",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }
    public void PublishOrder(Order order) {
        var message = JsonSerializer.Serialize(order);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
                routingKey: "inventoryQueue",
                basicProperties: null,
                body: body
        );
    }
}