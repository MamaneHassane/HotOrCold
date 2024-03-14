using System.Text.Json.Serialization;
using HotOrCold.Datas;
using HotOrCold.Repositories;
using HotOrCold.Repositories.Implementations;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Ajouter les contrôleurs
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Ajouter un convertisseur d'énumérations
    var enumConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(enumConverter);
    // Ignorer les champs non renseignés
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Ajouter endpoinexplorer et Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurer l'injection de dépendances pour le contexte de la base de données
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<ApplicationDbContext>(ConnectionString);

// Ajouter les répositories
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<IDrinkCopyRepository, DrinkCopyRepository>();
// Construire l'application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

// Mapper les contrôleurs
app.MapControllers();

app.Run();

