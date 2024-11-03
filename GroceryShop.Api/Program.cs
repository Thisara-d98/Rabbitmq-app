var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();  // Add Controllers for API handling
builder.Services.AddSingleton<RabbitMQService>();  // Register RabbitMQService as Singleton
builder.Services.AddSingleton<EmailService>();     // Register EmailService as Singleton

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();  // Ensure authorization is configured (if needed)

// Map controllers for routing
app.MapControllers();  // Map controllers for routing requests to your API

app.Run();
