using BusinessLogicLayer.AWS;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.UOW;
using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.IRepositories;
using BusinessModels;
using DataAccessLayer.Repositories;
using BusinessModels.Mapper;
using System.Reflection;

namespace EcommerceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<EcomContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
          );

            var tokenKey = Configuration.GetValue<string>("TokenKey");
            
            var key = Encoding.ASCII.GetBytes(tokenKey);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceAPI", Version = "v1" });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(tokenKey));

            services.AddAutoMapper(typeof(ModelMapper).GetTypeInfo().Assembly);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(x =>
                 {
                     x.RequireHttpsMetadata = false;
                     x.SaveToken = true;
                     x.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(key),
                         ValidateIssuer = false,
                         ValidateAudience = false
                     };
                 });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
