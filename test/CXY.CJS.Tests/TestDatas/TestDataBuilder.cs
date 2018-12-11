using CXY.CJS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CJSDbContext _context;

        public TestDataBuilder(CJSDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.Database.Migrate();

        }
    }
}