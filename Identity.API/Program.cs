
using Identity.Application;
using Identity.Domain.Entities.Models;
using Identity.Infrastructure;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContextOptions(builder.Configuration);

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyHeader()
                            .AllowAnyOrigin()
                                .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddApplication();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
