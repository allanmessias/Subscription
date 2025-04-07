using Subscription.Consumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<SubscriptionWorker>();

var host = builder.Build();
host.Run();
