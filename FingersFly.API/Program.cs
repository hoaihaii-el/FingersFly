
using FingersFly.API.Middlewares;
using FingersFly.Domain.Entities;
using FingersFly.Domain.Interfaces;
using FingersFly.Infrastructure.Data;
using FingersFly.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200") // Add your production origins as needed
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(cf =>
            {
                var conStr = builder.Configuration.GetConnectionString("Redis");
                if (conStr == null)
                {
                    throw new Exception("Can not get Redis connection string!");
                }
                var configuration = ConfigurationOptions.Parse(conStr, true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<AppUser>()
                .AddEntityFrameworkStores<StoreContext>();

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

            app.MapGroup("api").MapIdentityApi<AppUser>();

            app.UseMiddleware<ExceptionMiddleware>();
            app.Run();
        }
    }
}
