using MailKit.Net.Smtp;
using MimeKit;
using GroceryShop.Models;

public class EmailService
{
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _username = "thisarad582@gmail.com"; // Gmail address
    private readonly string _password = "rjqj fioe octi xslh"; // App Password (not regular password)

    public void SendEmail(string toEmail, Bill bill)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your Name", _username));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = "Your Bill";
        
        // Add the bill or body content here
        var bodyText = "Here is your bill details...\n";
        for (int i = 0; i < bill.Items.Count; i++)
        {
            bodyText+="aas";
            bodyText += $"{i + 1}. 009 {bill.Items[i].Name} - {bill.Items[i].Price}\n";
        }
        bodyText += $"Total: {bill.TotalAmount}";
        Console.WriteLine(bodyText);
        message.Body = new TextPart("plain")
        {
            Text = bodyText
        };

        using (var client = new SmtpClient())
        {
            client.Connect(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(_username, _password); // App Password here
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
