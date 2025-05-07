using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorPedidos.Infra.Mediatr;

public static class MediatrDependency
{
    public static IServiceCollection AddMediatrServices(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            AppDomain.CurrentDomain.Load("GerenciadorPedidos.Application"),
        };

        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(assemblies); });

        return services;
    }
}