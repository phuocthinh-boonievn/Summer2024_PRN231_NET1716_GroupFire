using Business_Layer.AutoMapper;
using Business_Layer.DataAccess;
using Business_Layer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", build => build.AllowAnyMethod()
                .AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(hostName => true).Build());
            });


            services.AddAutoMapper(typeof(ApplicationMapper));

            InjectServices(services);

        }

        private void InjectServices(IServiceCollection services)
        {
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //string ConnectionStr = config.GetConnectionString("Db");
            //services.AddDbContext<EShopDBContext>(option => option.UseSqlServer(ConnectionStr));

            services.AddDbContext<FastFoodDeliveryDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DB"));
            });
            // Add services repository pattern
            services.AddTransient<IMenuFoodItemRepository, MenuItemFoodRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
