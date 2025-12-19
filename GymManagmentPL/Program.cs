using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Data.DataSeeding;
using GymManagmentDAL.Repositories.Classes;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Tell Clr to Create object if i create it in class
            //Clr responsible to create this object
            builder.Services.AddDbContext<GymDBContext>(option =>
            {
                //option.UseSqlServer(builder.Configuration.GetSection("ConnectionString")["DefaultConnection"]);
                //option.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]);
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();

            var app = builder.Build();
            #region DataSeed
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDBContext>();
            var pendingMigration = dbContext.Database.GetPendingMigrations();

            if (pendingMigration?.Any() ?? false)
            {
                dbContext.Database.Migrate();
            }

            GymDbContextSeeding.SeedData(dbContext, app.Environment.ContentRootPath);
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
