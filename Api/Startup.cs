using System;
using Api.Enums;
using API.Attributes;
using Api.Extensions;
using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Npgsql;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using Contact = Swashbuckle.AspNetCore.Swagger.Contact;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        private IHostingEnvironment _env { get; }
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                // This must be last do not change this order unless you understand the 
                // way variable overwriting occurs
                .AddEnvironmentVariables();

            _configuration = API.Attributes.Root.SetConfiguration(builder.Build());
            
            // hold environment variable
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Enforces a global enforcement policy on authenticating a user
            services.AddMvc(config =>
            {
                // this is used to handle model state error and prevent additional action if model state is invalid
                config.Filters.Add(typeof(ModelStateValidationActionFilterAttribute));

                // this is used to custom handle business logic and other generic exceptions
                config.Filters.Add(typeof(ExceptionFilterAttribute));
                
                // if environment is localhost, then all anonymous, no token or authentication
                if (_env.IsLocalHost())
                {
                    config.Filters.Add(new AllowAnonymousFilter());
                }
                
            }).AddJsonOptions(x =>
            {
                // if environment name is development, then returned json
                // is formatted (and indented) so it would be more readable
                if (_env.IsLocalHost())
                {
                    x.SerializerSettings.Formatting = Formatting.Indented;
                }
            });
            
            services.AddAutoMapper();
            
            //StructureMap Container
            var container = new Container();

            // Add swagger json generation
            services.AddSwaggerGen(c =>
            {
                //Metadata and configuration for the swagger client
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Title",
                    Description = "Description",
                    TermsOfService = "TermsOfService",
                    Contact = new Contact
                    {
                        Name = "Name",
                        Email = "Email",
                        Url = "Url"
                    },
                    Version = "v1"
                });

               // var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APIDocumentation.xml");
                //c.IncludeXmlComments(filePath);
                
                c.DescribeAllEnumsAsStrings();
            });

            
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    // Registering to allow for Interfaces to be dynamically mapped
                    _.AssemblyContainingType(typeof(Startup));
                    _.Assembly("DomainLogic");
                    _.Assembly("DataAccessLayer");
                    _.Assembly("Models");
                    _.WithDefaultConventions();
                });
                
                config.Populate(services);

                // Sqlite connection string
                var sqliteConnectionString = new SqliteConnectionStringBuilder()
                {
                    DataSource = "db.sqlite",
                    Mode = SqliteOpenMode.ReadWriteCreate,
                    Cache = SqliteCacheMode.Private
                }.ToString();
                
                var postgreConnectionStringBuilder = (Func<string, string>)(connectionUrl =>
                {
                    var tokens = connectionUrl.ToConnectionTokens();

                    return new NpgsqlConnectionStringBuilder
                    {
                        Port = short.TryParse(tokens[ConnectionTokens.Port], out var port) ? port : 12345,
                        Host = tokens[ConnectionTokens.Host],
                        Username = tokens[ConnectionTokens.Username],
                        Password = tokens[ConnectionTokens.Password],
                        Database = tokens[ConnectionTokens.Database]
                    }.ToString();
                });

                var entityDbContext = new EntityDbContext(new DbContextOptionsBuilder(), x =>
                {
                    if (Environment.GetEnvironmentVariables().Contains("DATABASE_URL"))
                    {
                        x.UseNpgsql(postgreConnectionStringBuilder(Environment.GetEnvironmentVariable("DATABASE_URL")));
                    }
                    else
                    {
                        x.UseSqlite(sqliteConnectionString);
                    }
                });
                
                config.For<EntityDbContext>().Use(entityDbContext);
            });
                        
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            
            app.UseCors(x => { x.AllowAnyOrigin(); });
            
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}"); });
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}