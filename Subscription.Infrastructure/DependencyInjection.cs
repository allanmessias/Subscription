using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscription.Application;
using Subscription.Application.Use_Cases;
using Subscription.Core.Application;
using Subscription.Core.Domain;
using Subscription.Core.Interfaces;
using Subscription.Core.Use_Cases;
using Subscription.Infrastructure.Persistence;
using Subscription.Infrastructure.RabbitMq;
using Subscription.Infrastructure.RabbitMQ;
using Subscription.Infrastructure.Repositories;

namespace Subscription.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IMessageBrokerConnection, RabbitMqConnection>();
            services.AddSingleton<RabbitMqPublisher>();
            services.AddSingleton<SubscriptionPublisher>();
            services.AddScoped<IActivateSubscriptionUseCase, ActivateSubscriptionUseCase>();
            services.AddScoped<IDeactivateSubscriptionUseCase, DeactivateSubscriptionUseCase>();
            services.AddScoped<IRestoreSubscriptionUseCase, RestoreSubscriptionUseCase>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IEventHistoryRepository, EventHistoryRepository>();
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();


            return services;
        }
    }
}
