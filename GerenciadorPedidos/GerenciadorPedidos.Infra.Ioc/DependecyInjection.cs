using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Mappings;
using GerenciadorPedidos.Application.Services;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Infra.Data.Context;
using GerenciadorPedidos.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorPedidos.Infra.Ioc;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

        //Repositories
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        //Services
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IPedidoService, PedidoService>();

        return services;
    }
}