using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json; // Add this if it's missing
using GroceryShop.Models;
public class Program {
    public static void Main(string[] args) {
        var factory = new ConnectionFactory() {HostName = "localhost"};
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "inventoryQueue",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null
        );

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received +=(model,ea) => {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var order = JsonSerializer.Deserialize<Order>(message);

            var InventoryService = new InventoryService();
            foreach (var item in order.Items) {
                InventoryService.ReduceItem(item.Id, item.Quantity);
            }
        };

        channel.BasicConsume(queue: "inventoryQueue",
                             autoAck: true,
                             consumer: consumer);
        
        Console.WriteLine("Inventory Service is Running");
        Console.ReadLine();

    }
}