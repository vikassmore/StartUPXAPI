using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http.Features;

using Microsoft.AspNetCore.Cors.Infrastructure;
using StartUpX.Entity.DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StartUpX.Model;
using StartUpX.Business.Interface;
using StartUpX.Business.Implementation;
using Amazon.S3.Model;

namespace StartUpX.API
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
            services.AddOptions();
            services.AddControllers();
            services.AddCors();
            services.AddDbContext<StartUpDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //Register all injecting interfaces with implemented class
              services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuditLogService, UserAuditLogService>();
            services.AddScoped<IFundingService, FundingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IStartupDetailsService, StartupDetailsService>();
            services.AddScoped<IFounderDetailService, FounderDetailService>();
            services.AddScoped<IFundingDetailsServices, FundingDetailsService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IInvestorDetailService, InvestorDetailServices>();
            services.AddScoped<IInvestmentDetailService, InvestmentDetailService>();
            services.AddScoped<IFounderTypeServicecs, FounderTypeService>();
            services.AddScoped<ITrustedContactPersonService, TrustedContactPersonService>();
            services.AddScoped<IBankDetailService, BankDetailService>();
            services.AddScoped<ISuitabilityQuestionService, SuitabilityQuestionService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IFounderVerificationService, FounderVerificationService>();
            services.AddScoped<IInvestorVerificationService, InvestorVerificationService>();
            services.AddScoped<IInvestorInvestmentService, InvestorInvestmentService>();
            services.AddScoped<INotableInvestorService, NotableInvestorService>();
            services.AddScoped<ICreateOTPandSendMessage, CreateOTPandSendMessageService>();
            services.AddScoped<ISocialMediaLogin, SocialMediaLoginService>();
            services.AddScoped<IInvestmentOpportunityDetailsService, InvestmentOpportunityDetailsService>();
            services.AddScoped<IUserAuditLogService, UserAuditLogService>();
            services.AddScoped<IFounderInvestorDocumentService, FounderInvestorDocumentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAccreditedInvestor, AccreditedInvestorService>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<ConfigurationModel>(Configuration.GetSection("ConfigurationModel"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
  
            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Configuration["APIInfo:Title"],
                    Description = Configuration["APIInfo:Description"]
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = Configuration["APISecurityDefinition:Description"],
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
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
            }

            app.UseCors(builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .WithHeaders("authorization", "accept", "content-type", "origin"));

            //Accesing Physical Files like img, pdf

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Resources", "FounderDocument")),
                RequestPath = "/FounderDocument",
                ServeUnknownFileTypes = true,
                DefaultContentType = "Pdf/image"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Resources", "InvestorDocument")),
                RequestPath = "/InvestorDocument",
                ServeUnknownFileTypes = true,
                DefaultContentType = "Pdf/image"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ServicePortFolioDocument")),
                RequestPath = "/ServicePortFolioDocument",
                ServeUnknownFileTypes = true,
                DefaultContentType = "Pdf/image"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ServiceInvoiceDocument")),
                RequestPath = "/ServiceInvoiceDocument",
                ServeUnknownFileTypes = true,
                DefaultContentType = "Pdf/image"
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET 5 Web API v1"));
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
