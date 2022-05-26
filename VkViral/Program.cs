using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VkViral.Data;
using VkViral.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.IgnoreNullValues = true;
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("Postgres")));


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<GroupsService>();
builder.Services.AddScoped<PostsService>();
builder.Services.AddScoped<VkService>();
builder.Services.AddScoped<PredictionService>();
builder.Services.AddScoped<SortingService>();

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