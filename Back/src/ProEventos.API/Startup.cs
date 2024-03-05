using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProEventos.Application;
using ProEventos.Application.Contratos;
using ProEventos.Persistence;
using ProEventos.Persistence.Contratos;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace ProEventos.API
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
      services.AddDbContext<ProEventosContext>(
          context => context.UseSqlite(Configuration.GetConnectionString("Default"))
      );

      services.AddIdentityCore<User>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
      })
      .AddRoles<Role>()
      .AddRoleManager<RoleManager<Role>>()
      .AddSignInManager<SignInManager<User>>()
      .AddRoleValidator<RoleValidator<Role>>()
      .AddEntityFrameworkStores<ProEventosContext>()
      .AddDefaultTokenProviders();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                  ValidateIssuer = false,
                  ValidateAudience = false
                };
              });

      services.AddControllers()
              .AddJsonOptions(options =>
                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
              )
              .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling =
                  Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddScoped<IEventoService, EventoService>();
      services.AddScoped<ILoteService, LoteService>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IAccountService, AccountService>();
      services.AddScoped<IGeralPersist, GeralPersist>();
      services.AddScoped<IEventoPersist, EventoPersist>();
      services.AddScoped<ILotePersist, LotePersist>();
      services.AddScoped<IUserPersist, UserPersist>();

      services.AddCors();
      services.AddSwaggerGen(option =>
      {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espa�o] ent�o coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });

        option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseCors(x => x.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin());

      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
        RequestPath = new PathString("/Resources")
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
