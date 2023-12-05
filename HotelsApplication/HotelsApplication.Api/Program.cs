using FluentValidation;
using FluentValidation.AspNetCore;
using HotelsApplication.Api.ApiHandlers;
using HotelsApplication.Api.Middleware;
using HotelsApplication.Infrastructure.DataSource;
using HotelsApplication.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;
        // Add services to the container.

        builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);


        builder.Services.AddDbContext<DataContext>(opts =>
        {
            opts.UseSqlServer(config.GetConnectionString("db"));
        });

        //builder.Services.AddDomainServices();
        builder.Services.AddDomainServices(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(Assembly.Load("HotelsApplication.Application"), typeof(Program).Assembly);


        builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<RequestValidatorHotel>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<AppExceptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.MapGroup("/api/hotels").MapHotels();

        app.Run();
    }
}
