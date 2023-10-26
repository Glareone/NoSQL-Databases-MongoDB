using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MongoDB.WebAPI;
using MongoDB.WebAPI.MongoDBConfigurations;
using MongoDB.WebAPI.ProblemDetailsExecutor;
using MongoDB.WebAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// To Handle Error according IETF Standard
builder.Services.AddSingleton<IActionResultExecutor<ObjectResult>, ProblemDetailsResultExecutor>();

builder.Services.Configure<MongoDBConfig>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<CoursesConfig>(builder.Configuration.GetSection("CoursesDBConfig"));
builder.Services.AddSingleton<CourseService>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// register all endpoints using reflection
app.MapAllEndpoints(Assembly.GetExecutingAssembly());


app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();