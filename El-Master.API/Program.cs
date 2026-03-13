using El_Master.API.Middlewares;
using El_Master.Application.Common.Behaviors;
using El_Master.Application.Common.Results;
using El_Master.Application.DependencyInjection;
using El_Master.Application.Features.Auth.Commands.RegisterCommand;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Application.Settings;
using El_Master.Domain.Entities;
using El_Master.Infrastructure.DependencyInjection;
using El_Master.Infrastructure.Presistence;
using El_Master.Infrastructure.Presistence.Repositories;
using El_Master.Infrastructure.ervices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace El_Master.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Disable Automatic Validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            await SeedRolesAsync(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }

        static async Task SeedRolesAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "User", "Teacher", "Admin", "SuperAdmin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}