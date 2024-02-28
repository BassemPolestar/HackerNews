using System.Text.Json.Serialization;
using HackerNews.Application.Services;
using HackerNews.Infrastructure.Clients;
using HackerNews.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheClient, MemoryCacheClient>();
builder.Services.AddHttpClient<IHackerNewsClient, HackerNewsClient>();
builder.Services.AddSingleton<IHackerNewsService, HackerNewsService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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