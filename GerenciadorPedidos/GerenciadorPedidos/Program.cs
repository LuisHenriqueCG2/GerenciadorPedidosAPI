using System.Globalization;
using GerenciadorPedidos.Infra.Ioc;
using GerenciadorPedidos.Infra.Mediatr;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Acessa a configura��o diretamente do builder fazendo a inje��o de depend�ncia para registrar os servi�os;
builder.Services.AddInfrastructure(builder.Configuration);

// Adiciona os servi�os ao container.
builder.Services.AddMediatrServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API para Gerenciamento de Pedidos de uma Loja",
        Description = "Um projeto desenvolvido em ASP.NET Core",
        TermsOfService = new Uri("https://insidesistemas.com.br"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://insidesistemas.com.br")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://insidesistemas.com.br")
        }
    });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciador de Pedidos V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
