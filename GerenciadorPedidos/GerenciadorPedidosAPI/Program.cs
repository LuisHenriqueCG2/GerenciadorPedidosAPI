using GerenciadorPedidosAPI.Infra.Repositories;
using GerenciadorPedidosAPI.Interfaces;
using GerenciadorPedidosAPI.Mappings;
using GerenciadorPedidosAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GerenciadorPedidosContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));

// Cria o app depois de registrar todos os serviços
var app = builder.Build();

// Cria o banco de dados automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GerenciadorPedidosContext>();
    context.Database.Migrate();
}

// Configurações do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
