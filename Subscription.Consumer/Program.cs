using Microsoft.EntityFrameworkCore;
using Subscription.Consumer;
using Subscription.Infrastructure.Persistence;
using Subscription.Infrastructure.Configuration;
using Subscription.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHostedService<SubscriptionWorker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    Console.WriteLine("Connection string usada: " + config.GetConnectionString("DefaultConnection"));
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

host.Run();