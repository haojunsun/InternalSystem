using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalSystem.Core.Models;
using InternalSystem.Core.Repositories;
using InternalSystem.Infrastructure;

namespace InternalSystem.Core.Services
{
    public interface IManagerService : IDependency
    {
        IEnumerable<Manager> List();
        void Add(Manager manager);
        void Update(Manager manager);
        void Delete(int id);
        Manager Get(int id);
    }

    public class ManagerService : IManagerService
    {
          private readonly AppDbContext _appDbContext;
          public ManagerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Manager> List()
        {
            return _appDbContext.Managers.OrderByDescending(x => x.CreatedUtc).ToList();
        }

        public void Add(Manager manager)
        {
            _appDbContext.Managers.Add(manager);
            _appDbContext.SaveChanges();
        }

        public void Update(Manager manager)
        {
            _appDbContext.Entry(manager).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var article = _appDbContext.Managers.Find(id);

            if (article == null)
            {
                return;
            }

            _appDbContext.Managers.Remove(article);
            _appDbContext.SaveChanges();
        }

        public Manager Get(int id)
        {
            return _appDbContext.Managers.Find(id);
        }

      
    }
}
