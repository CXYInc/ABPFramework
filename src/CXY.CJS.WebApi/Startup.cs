using Abp.AspNetCore;
using Abp.Dependency;
using Abp.Extensions;
using Castle.Facilities.Logging;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.SubSystems.Conversion;
using Castle.Services.Logging.SerilogIntegration;
using CXY.CJS.Configuration;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.Utils.Mail;
using CXY.CJS.Core.Utils.SMS;
using CXY.CJS.JwtAuthentication;
using CXY.CJS.Web.Core;
using CXY.CJS.Web.Core.Filter.Swagger;
using CXY.CJS.Web.Core.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CXY.CJS.WebApi
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";
        private const string _allowAnyOriginCorsPolicyName = "AllowAnyOrigin";
        private readonly JwtBearerConfig _jwtBearerConfig;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();

            _jwtBearerConfig = new JwtBearerConfig
            {
                IsEnabled = true,
                Issuer = "CXY.CJS",
                Audience = "CXY.CJS",
                ValidMinutes = 30,
                PrivateKeys = ""
            };

            _appConfiguration.GetSection("Authentication:JwtBearer").Bind(_jwtBearerConfig);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddWebApiServices(_env);

            var corsOrigins = _appConfiguration["App:CorsOrigins"]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.RemovePostFix("/"))
                .ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName,
                builder => builder.WithOrigins(corsOrigins)
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials());
            });

            services.AddCors(c =>
            {
                c.AddPolicy(_allowAnyOriginCorsPolicyName, policy =>
                {
                    policy.AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
            });

            services.AddJwtAuthentication(option =>
            {
                option.Audience = _jwtBearerConfig.Audience;
                option.ValidateAudience = true;
                option.Issuer = _jwtBearerConfig.Issuer;
                option.ValidateIssuer = true;
                option.SecurityAlgorithms = SecurityAlgorithms.HmacSha256;
                option.SigningKey = _jwtBearerConfig.PrivateKeys;
                option.ValidMinutes = _jwtBearerConfig.ValidMinutes;
                option.OnChallenge = context => JwtOptionsCallBack.OnChallenge(context);
                option.OnAuthenticationFailed = context => JwtOptionsCallBack.OnAuthenticationFailed(context);
                option.OnTokenValidated = context => JwtOptionsCallBack.OnTokenValidated(context);
                option.SecurityTokenValidator = new JwtCustomValidator();
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("WebApi", new Info { Title = "ApiDoc", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Token}",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
                options.DocumentFilter<EnumDescriptionDocumentFilter>();
                //options.SchemaRegistryOptions.CustomTypeMappings.Add(typeof(IFormFile), () => new Schema() { Type = "file", Format = "binary" });
                options.OperationFilter<FormFileOperationFilter>();
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "CXY.CJS.Application.xml");
                options.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(basePath, "CXY.CJS.Core.xml");
                options.IncludeXmlComments(xmlPath);

            });

            services.AddSingleton(_jwtBearerConfig);

            // 配置邮件发送
            var mailSendConfig = _appConfiguration.GetSection("SystemSmtpSenderConfiguration").Get<SystemSmtpSenderConfiguration>();
            services.AddSystemSmtpSender(mailSendConfig);

            //配置短信发送
            services.AddSmsSender(_appConfiguration.GetSection("SmsSenderConfiguration").Get<SmsSenderConfiguration>());

            services.Configure<ApiUrlConfig>(_appConfiguration.GetSection("ApiUrlConfig"));

            // Configure Abp and Dependency Injection
            return services.AddAbp<CJSWebApiModule>(options =>
            {
                //Serilog日志注入
                var configBuilder = new LoggerConfiguration();
                configBuilder = configBuilder.ReadFrom.Configuration(_appConfiguration);
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(logger => logger.LogUsing(new SerilogFactory(configBuilder.CreateLogger())));

                //解决属性依赖注入报错
                var propInjector = options.IocManager.IocContainer.Kernel.ComponentModelBuilder
                .Contributors.OfType<PropertiesDependenciesModelInspector>().Single();
                options.IocManager.IocContainer.Kernel.ComponentModelBuilder.RemoveContributor(propInjector);
                options.IocManager.IocContainer.Kernel.ComponentModelBuilder.AddContributor(new AbpPropertiesDependenciesModelInspector(new DefaultConversionManager()));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_allowAnyOriginCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            //Jwt认证
            app.UseJwtAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "apidoc/swagger/{documentName}/swagger.json";
            });
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "apidoc";
                c.SwaggerEndpoint($"/apidoc/swagger/WebApi/swagger.json", "WebApi");
            });
        }
    }
}
