using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; 
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using Banking.Api.Repositories.Interfaces;
using Banking.Api.Repositories.Services;
using System.Reflection;
using System.IO;

namespace Banking.Api
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
            services.AddSwaggerGen(option =>
            {
      
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking.Api", Version = "v1" });
                


                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });


                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            services.AddSingleton<IUnitOfWork, UnitOfWork>(); 
            services.AddSingleton<IDapperContext, DapperContext>();
            
            
            // services.AddSingleton<ILog4NetRepository, Log4NetRepository>();
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://192.168.12.60:99", "http://192.168.12.55:99", "http://facebook.com", "http://darimana.com", "http://192.168.3.120")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }
            ));

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue; // <-- ! long.MaxValue
                options.MultipartBoundaryLengthLimit = int.MaxValue;
                options.MultipartHeadersCountLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    // using Microsoft.IdentityModel.Tokens;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    // string[] dt = {"Admin", "admin"};
                    // policy.RequireClaim(ClaimTypes.Role, dt);
                });
            });


            // services.AddScoped<IUnitOfWorks, CountryRepositoryGUI>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddSingleton<ILog, ILog>();
            services.AddControllers(
                opt =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                }
            ).AddNewtonsoftJson(
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()
            );

            //  Fluent Validation
            services.AddControllers()
                .AddFluentValidation(s => 
                { 
                    s.RegisterValidatorsFromAssemblyContaining<Startup>(); 
                    // s.RunDefaultMvcValidationAfterFluentValidationExecutes = false; 
                    s.DisableDataAnnotationsValidation = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking.Api v1"));
            }else{
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking.Api v1"));
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            app.UseCors("CorsPolicy");

            app.UseRouting(); 
            app.UseAuthorization();
            app.UseAuthentication();
            

            // app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
