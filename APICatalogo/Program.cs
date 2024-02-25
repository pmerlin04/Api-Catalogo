using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//vai ignorar as referencias ciclicas nos métodos nos controllers
//a referencia ciclica é porque Categoria referencia uma LISTA de Produto
//e Produto referencia Categoria
builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions
                        .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//conectando com o banco de dados com  a string de conexão já criada
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//useMySql = provedor do banco de dados MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
