using Application.Common.Interfaces;
using Infrastructure.Common.Configuration;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            ApplicationSetting settings,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
            ServiceLifetime contexLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifeTime = ServiceLifetime.Scoped
        )
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
                    services.AddScoped<ApplicationDbContextInitialiser>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
                    services.AddTransient<ApplicationDbContextInitialiser>();
                    break;
            }

            if (
                settings is null || 
                (settings != null && settings.DatabaseConnection is null) ||
                (settings != null && settings.DatabaseConnection != null && string.IsNullOrWhiteSpace(settings.DatabaseConnection.DbConnection))
            )
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("tkvsample"));
            }
            else if (settings != null && settings.DatabaseConnection != null && !string.IsNullOrWhiteSpace(settings.DatabaseConnection.DbConnection))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(settings.DatabaseConnection.DbConnection,
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    ),
                    contexLifetime,
                    optionsLifeTime
                );
            }
            return services;
        }
    }
}