using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using ExerciseLogApi.Models;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<Persistence>(opt =>
    opt.UseInMemoryDatabase("ExerciseLog"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.MapType<TimeSpan>(() => new OpenApiSchema{
        Type = "string", Example = new OpenApiString("00:00:00")
    })
);

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
