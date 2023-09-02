using BookStore.User.Context;
using BookStore.User.Interface;
using BookStore.User.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BookStore.User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //Swagger Authorization
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.User V1", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Using Authorization header with the beare schema",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",

                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                option.AddSecurityDefinition("Bearer", securitySchema);
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySchema , new[] { "Bearer" } }
                });

            });
            //Migration
            builder.Services.AddDbContext<UserDBContext>(ops =>
            {
                ops.UseSqlServer(builder.Configuration.GetConnectionString("UserDB"));
            });
            //Service for interface and sercice
            builder.Services.AddTransient<IUserService, UserService>();

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
            

            app.MapControllers();

            app.Run();
        }
    }
}