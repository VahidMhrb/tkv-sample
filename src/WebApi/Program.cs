using Infrastructure.Common.Configuration;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Serilog;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Interfaces.BusinessRules;
using Infrastructure.Services.BusinessRules;
using Infrastructure.Persistence;
using Infrastructure;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("serilogconfig.json")
                    .Build())
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithCorrelationId()
                .CreateLogger();
            try
            {

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Host terminated unexpectedly");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            var builder = WebApplication.CreateBuilder(args);
            ApplicationSetting settings = builder.Configuration.GetSection("ApplicationSetting").Get<ApplicationSetting>() ?? new();
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.UseCamelCasing(true);
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddMemoryCache();

            builder.Services.Configure<ApplicationSetting>(builder.Configuration.GetSection("ApplicationSetting"));
            builder.Services.AddInfrastructureServices(settings);

            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });


            builder.Services.AddMvcCore().AddApiExplorer();
            builder.Services.AddHttpContextAccessor();


            //ConfigureApiBehaviorOptions
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            //AddApiVersioning
            builder.Services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            builder.Services.AddScoped<IInquiryBusinessRule, InquiryBusinessRule>();
            
            builder.Services.AddOptions();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("IsTestApp") == "true")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();

                // Initialize and seed database
                using var scope = app.Services.CreateScope();
                var dbInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                await dbInitialiser.InitialiseAsync();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });
            app.UseRouting();
            app.UseCors("AllowAnyOrigin");
            app.MapControllers();

            app.Run();
        }
    }
}