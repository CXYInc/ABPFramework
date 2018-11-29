using Abp.Application.Services;
using CXY.CJS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace CXY.CJS.Service
{
    public class TestService : ITestService
    {
        [HttpPost]
        public bool Add(Test entity)
        {
            return true;
        }
    }
}
