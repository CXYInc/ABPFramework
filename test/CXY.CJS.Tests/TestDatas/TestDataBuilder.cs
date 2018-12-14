using System.Collections.Generic;
using System.Threading.Tasks;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CJSDbContext _context;

        private static readonly List<Task> _actionTasks = new List<Task>();

        public TestDataBuilder(CJSDbContext context)
        {
            _context = context;
        }

        public async Task Build()
        {
            await _context.Database.MigrateAsync();

            InitWebSite();
            InitMenu();
            InitUser();

            await Task.WhenAll(_actionTasks);
        }

        private void InitUser()
        {
            AddTask(_context.Users.AddAsync(UserDatas.SuperWebSiteMasterUser));
        }

        private void InitMenu()
        {
            AddTask(_context.Menus.AddAsync(MenuDatas.UserModule));
            AddTask(_context.Menus.AddAsync(MenuDatas.DedeletedModuleMenu));
            AddTask(_context.Menus.AddAsync(MenuDatas.WillBeDedeletedMenu));
            
        }

        private void InitWebSite()
        {
            AddTask(_context.WebSites.AddAsync(WebSiteDatas.SuperWebSite));
            AddTask(_context.WebSites.AddAsync(WebSiteDatas.DedeletedWebSite));
        }

        private void AddTask(Task task)
        {
            _actionTasks.Add(task);
        }
    }
}