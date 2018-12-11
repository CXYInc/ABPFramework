using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Dependency;
using Castle.MicroKernel.Registration;
using CXY.CJS.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CXY.CJS.Web.Tests
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
           //services.AddEntityFrameworkInMemoryDatabase();

            services.AddMvc();

            //Configure Abp and Dependency Injection
            return services.AddAbp<CJSWebTestModule>(options =>
            {
                options.SetupTest();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            UseInMemoryDb(app.ApplicationServices);

            app.UseAbp(); //Initializes ABP framework.

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }

        private void UseInMemoryDb(IServiceProvider serviceProvider)
        {
            var builder = new DbContextOptionsBuilder<CJSDbContext>();
            //builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);
            //var options = builder.Options;

            //var iocManager = serviceProvider.GetRequiredService<IIocManager>();

            //iocManager.IocContainer
            //    .Register(
            //        Component.For<DbContextOptions<CJSDbContext>>()
            //        .Instance(options)
            //        .LifestyleSingleton()
            //    );
        }
    }
}
