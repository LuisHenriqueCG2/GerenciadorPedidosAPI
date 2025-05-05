using System.Globalization;
using GerenciadorPedidos.Infra.Ioc;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Acessa a configuração diretamente do builder fazendo a injeção de dependência para registrar os serviços;
builder.Services.AddInfrastructure(builder.Configuration);

// Adiciona os serviços ao container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API para Gerenciamento de Pedidos de uma Loja",
        Description = "Um projeto desenvolvido em ASP.NET Core",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
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
