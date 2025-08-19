using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.EntityFrameworkCore;
using Produtos.Api.Application.Services;
using Produtos.Api.Infrastructure.Data;
using Produtos.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ProdutosDb"));
// DI (DIP)

builder.Services.AddScoped<IProdutoRepositoryReader, ProdutoRepository>();
builder.Services.AddScoped<IProdutoRepositoryWriter, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

var allowedOrigins = new[]
{ "https://produtosweb-h5h7hxetb0ezctgw.brazilsouth-01.azurewebsites.net","http://localhost:5173", "http://localhost:3000" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {

// }

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!ctx.Produtos.Any())
    {
        ctx.Produtos.AddRange(
            new Produtos.Api.Domain.Entities.Produto { Nome = "Teclado Mecânico", Valor = 299.90m, Categoria = "Periféricos" },
            new Produtos.Api.Domain.Entities.Produto { Nome = "Microondas", Valor = 299.90m, Categoria = "Eletrodomésticos" },
            new Produtos.Api.Domain.Entities.Produto { Nome = "Mouse Gamer", Valor = 159.00m, Categoria = "Periféricos" }
        );
        ctx.SaveChanges();
    }
}

app.Run();
