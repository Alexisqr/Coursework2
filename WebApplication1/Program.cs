
using Microsoft.EntityFrameworkCore;
using Practice.Application.Contracts.Persistence;
using StrayHome.Infrastructure.Data;
using WebApplication1.ONNX;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Practice.Application.Features.Queries.GetAllAnimal;
using MediatR;
using Practice.Application.Features.Commands.DeleteAnimal;
using Practice.Application.Features.Commands.UpdateAnimal;
using Practice.Application.Features.Queries.GetByIdAnimal;
using Practice.Application.Features.Commands.CreateAnimal;
using Practice.Application.Mappings;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IStrayHomeContext, StrayHomeContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("StrayHomeDbContext"),
        new MySqlServerVersion(new Version(8, 0, 32)));
});

builder.Services.AddMemoryCache();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowAllOrigins", options => options.AllowAnyOrigin().AllowAnyMethod()
     .AllowAnyHeader());
});

builder.Services.AddMediatR(typeof(CreateAnimalCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteAnimalCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateAnimalCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetByIdAnimalQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllAnimalQueryHandler).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<PredictionService>();
builder.Services.AddScoped<PredictionCatService>();
//JSON Serializer
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapFallbackToFile("index.html");
app.UseStaticFiles();
app.UseDefaultFiles();

app.MapControllers();

app.Run();
