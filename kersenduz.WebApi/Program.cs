using kersenduz.Core.Repository.IoC;
using kersenduz.DataAccess.Context;
using kersenduz.DataAccess.UnitOfWork.Implement;
using kersenduz.DataAccess.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using kersenduz.WebApi.Service.IoC;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUnitOfWork, UnitOWork>();
builder.Services.AddScopedRepository();
builder.Services.AddScopedService();

var connectionString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

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
