
using FingersFly.API.Middlewares;
using FingersFly.Domain.Interfaces;
using FingersFly.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FingersFly.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
            });

            builder.Services.AddScoped(typeof(IProductRepo), typeof(ProductRepo));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Add your production origins as needed
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddleware<ExceptionMiddleware>();
            app.Run();
        }
    }
}
