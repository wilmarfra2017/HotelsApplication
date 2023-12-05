using HotelsApplication.Domain.Ports;
using HotelsApplication.Domain.Services;
using HotelsApplication.Infrastructure.Adapters;
using HotelsApplication.Infrastructure.InfrastructureServices;
using HotelsApplication.Infrastructure.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelsApplication.Infrastructure.Extensions
{
    public static class AutoLoadServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INotificationService, EmailNotificationService>(provider =>
            new EmailNotificationService(
                configuration["EmailSettings:SmtpServer"] ?? throw new InvalidOperationException("SMTP server must be configured."),
                int.Parse(configuration["EmailSettings:SmtpPort"] ?? "25"),
                configuration["EmailSettings:FromEmail"] ?? throw new InvalidOperationException("From email must be configured."),
                configuration["EmailSettings:SmtpUsername"] ?? throw new InvalidOperationException("SMTP username must be configured."),
                configuration["EmailSettings:SmtpPassword"] ?? throw new InvalidOperationException("SMTP password must be configured.")
            ));

            services.AddTransient<IReservationService, ReservationService>();

            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            var _services = AppDomain.CurrentDomain.GetAssemblies()
                  .Where(assembly =>
                  {
                      return (assembly.FullName is null) || assembly.FullName.Contains("Domain", StringComparison.InvariantCulture);
                  })
                  .SelectMany(s => s.GetTypes())
                  .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

            
            var _repositories = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                {
                    return (assembly.FullName is null) || assembly.FullName.Contains("Infrastructure", StringComparison.InvariantCulture);
                })
                .SelectMany(s => s.GetTypes())
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(RepositoryAttribute)));

            
            foreach (var service in _services)
            {
                services.AddTransient(service);
            }

            
            foreach (var repo in _repositories)
            {
                Type? iface = repo.GetInterfaces().SingleOrDefault();
                if (iface == null)
                {
                    throw new InvalidOperationException($"El tipo {repo.Name} no implementa ninguna interfaz o implementa más de una.");
                }
                services.AddTransient(iface, repo);

            }

            return services;
        }

    }
}
