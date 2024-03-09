using LuceneAcco.Business.Contracts;
using LuceneAcco.Business.Services;
using LuceneAcco.Data.Abstractions;
using LuceneAcco.Data.Repositories;
using LuceneAcco.RedisCache.Abstractions;
using LuceneAcco.RedisCache.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccommodationsService, AccommodationsService>();
builder.Services.AddScoped<IAccommodationsRepository, AccommodationsRepository>();
builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddScoped<IRedisCache, RedisCache>();
builder.Services.AddScoped<ILuceneEngineRepository, LuceneEngineRepository>();
builder.Services.AddScoped<ILuceneSearchEngine,LuceneSearchEngine>();


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
