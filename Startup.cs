using System;
using AutoMapper;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using game_store_api.Data;
using game_store_api.Helper;
using game_store_api.Services;
using game_store_api.Repository;
using game_store_api.Interfaces;

namespace game_store_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddScoped<IGameStorage, GameStorage>();
            services.AddScoped<IPurchasedGamesStorage, PurchasedGamesStorage>();
            services.AddScoped<IUserStorage, UserStorage>();
            services.AddScoped<ITokenStorage, TokenStorage>();

            services.AddScoped<AuthHelper>();
            services.AddScoped<AvailableGamesService>();
            services.AddScoped<BuyGameService>();
            services.AddScoped<GameService>();
            services.AddScoped<LoginService>();
            services.AddScoped<PurchasedGamesService>();
            services.AddScoped<UserService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(opts => opts.UseMySql(Configuration.GetConnectionString("Connection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("Connection"))));
            services.AddControllers();

            services.AddCors();

            var key = Encoding.ASCII.GetBytes(Secret.Word);
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "game_store_api", Version = "v1" });
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "game_store_api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
