using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CXY.CJS.Repository
{
    public class TestRepository : CJSRepositoryBase<Test, string>, ITestRepository
    {
        private readonly IDbContextProvider<CJSDbContext> _dbContextProvider;
        private readonly IRepository<Test, string> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public TestRepository(IDbContextProvider<CJSDbContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager, IRepository<Test, string> repository) : base(dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public Test Add(Test entity)
        {
            var t = _repository.FirstOrDefault("7");

            //_repository.Insert(entity);

            //Insert(entity);

            var list = _repository.GetAll().Where(x => x.Id == "").AsNoTracking().ToListAsync().Result;

            list.ForEach(x => x.Name = x.Id);

            //_unitOfWorkManager.Current.SaveChanges();

            //using (var unitOfWork = _unitOfWorkManager.Begin())
            //{
            //    _repository.Insert(entity);
            //    //Context.SaveChanges();
            //    unitOfWork.Complete();
            //}
            return entity;
        }

        public Test GetTest(string id)
        {
            return Get(id);
        }
    }
}
