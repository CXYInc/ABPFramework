using System;
using System.Threading.Tasks;
using Abp.TestBase;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Tests.TestDatas;

namespace CXY.CJS.Tests
{
    public class CJSTestBase : AbpIntegratedTestBase<CJSTestModule>
    {
        public CJSTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<CJSDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<CJSDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<CJSDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<CJSDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<CJSDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<CJSDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<CJSDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<CJSDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
