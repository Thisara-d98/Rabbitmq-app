using Microsoft.AspNetCore.Mvc;
using GroceryShop.Models;
using RabbitMQ.Client;

[ApiController]
[Route("[controller]")]
public class ShoppingController : ControllerBase
{
    private readonly RabbitMQService _rabbitMqService;
    private readonly EmailService _emailService;

    public ShoppingController(RabbitMQService rabbitMqService, EmailService emailService)
    {
        _rabbitMqService = rabbitMqService;
        _emailService = emailService;
    }

    [HttpPost("purchase")]
    public IActionResult Purchase([FromBody] Order order)
    {
        var bill = new Bill { CustomerEmail = order.CustomerEmail };

        foreach (var orderedItem in order.Items)
        {
            // Here you would typically check inventory in a real application
            // For simplicity, we assume items are available
            bill.Items.Add(orderedItem);
            bill.TotalAmount += orderedItem.Price * orderedItem.Quantity;
        }

        _rabbitMqService.PublishOrder(order);
        _emailService.SendEmail(order.CustomerEmail, bill);

        return Ok(bill);
    }
}
