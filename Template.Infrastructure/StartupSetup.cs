using $ext_safeprojectname$.Infrastructure.Data;
using $ext_safeprojectname$.SharedKernel.Util;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace $ext_safeprojectname$.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(AmbienteUtil.GetValue("CONNECTION_STRING_PGSQL")));

        public static void AddMassTransit(this IServiceCollection services) =>
            services.AddMassTransit(x =>
            {
                x.AddBus((provider) => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(AmbienteUtil.GetValue("CONNECTION_STRING_RABBIT_MQ")), h =>
                    {
                        h.Username(AmbienteUtil.GetValue("RABBIT_MQ_USER"));
                        h.Password(AmbienteUtil.GetValue("RABBIT_MQ_PASSWORD"));
                    });

                    config.ConfigureEndpoints(provider);
                }));
            });

        public static void AddMassTransitConsumer<T>(this IServiceCollection services, string queue, int limit) where T : class, IConsumer =>
            services.AddMassTransit(x =>
            {
                x.AddConsumer<T>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(AmbienteUtil.GetValue("CONNECTION_STRING_RABBIT_MQ")), h =>
                    {
                        h.Username(AmbienteUtil.GetValue("RABBIT_MQ_USER"));
                        h.Password(AmbienteUtil.GetValue("RABBIT_MQ_PASSWORD"));
                    });

                    cfg.ReceiveEndpoint(queue, e =>
                    {
                        e.ConfigureConsumer<T>(context);
                        e.ConcurrentMessageLimit = limit;
                    });
                });
            });
    }
}
