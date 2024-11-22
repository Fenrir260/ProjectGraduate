using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories;

namespace Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ContextDb>(options => options.UseSqlServer("Server=DESKTOP-DA47G2J;Database=ProductsProject;Trusted_Connection=True;TrustServerCertificate=True"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "http://localhost:58435", "http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            // Creating DI (dependency injection) 
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IUsersRepository, UserRepository>();
            


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("CORS");

            app.MapControllers();

            app.Run();

        }
    }
}

