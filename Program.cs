using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.InMemory;
using ExerciseLogApi.Models;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowCors",
                      policy  =>
                      {
                          policy.WithOrigins("*").AllowAnyHeader();
                      });
});
builder.Services.AddDbContext<Persistence>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.MapType<TimeSpan>(() => new OpenApiSchema{
        Type = "string", Example = new OpenApiString("00:00:00")
    })
);
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = builder.Configuration["Jwt:Issuer"];
        opt.Audience = builder.Configuration["Jwt:Audience"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
