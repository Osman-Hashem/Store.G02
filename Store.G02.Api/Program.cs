
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstractions;
using System.Threading.Tasks;
using Services.MappingProfiles;
using AssemblyMapping = Services.AssemblyReference;


namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(builder.Configuration["ConnectionStrings : DefaultConnection"]);
            });





            builder.Services.AddScoped<IDbInitializer, DbInitializer>(); // Allow DI For DbInitializer

            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(AssemblyMapping).Assembly);

            ////builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            builder.Services.AddScoped<IServiceManager, ServiceManager>();








            var app = builder.Build();

            #region Seeding
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // ASK CLR Create From DbInitializer

            await dbInitializer.InitializeAsync();
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseStaticFiles();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
