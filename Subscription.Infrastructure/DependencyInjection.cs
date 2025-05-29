using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscription.Application;
using Subscription.Application.UseCases;
using Subscription.Core.Application;
using Subscription.Core.Domain;
using Subscription.Infrastructure.RabbitMq;
using Subscription.Infrastructure.RabbitMQ;

namespace Subscription.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMessageBrokerConnection, RabbitMqConnection>();
            services.AddSingleton<RabbitMqPublisher>();
            services.AddSingleton<SubscriptionPublisher>();
            services.AddScoped<ISendSubscriptionUseCase, SendSubscriptionUseCase>();


            return services;
        }
    }
}
