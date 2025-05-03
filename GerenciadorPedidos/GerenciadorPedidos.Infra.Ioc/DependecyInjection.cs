using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Mappings;
using GerenciadorPedidos.Application.Services;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Infra.Data.Context;
using GerenciadorPedidos.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorPedidos.Infra.Ioc
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            //Se der tempo, será implementado métodos de autenticação.

            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}
            //).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,

            //        ValidIssuer = "",
            //        ValidAudience = ""
            //    };


            //});

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            //Repositories
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            //Services
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IPedidoService, PedidoService>();

            return services;
        }
    }
}
