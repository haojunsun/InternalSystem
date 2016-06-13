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

    public interface IWorldHeritageService : IDependency
    {
        IEnumerable<WorldHeritage> List();
        IEnumerable<WorldHeritage> List(int pageIndex, int pageSize, ref int totalCount);
        void Add(WorldHeritage manager);
        void Update(WorldHeritage manager);
        void Delete(int id);
        WorldHeritage Get(int id);
    }

    public class WorldHeritageService : IWorldHeritageService
    {
         private readonly AppDbContext _appDbContext;
         public WorldHeritageService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

         public IEnumerable<WorldHeritage> List()
         {
             return _appDbContext.WorldHeritages.OrderByDescending(x => x.CreatedUtc).ToList();
         }

         public IEnumerable<WorldHeritage> List(int pageIndex, int pageSize, ref int totalCount)
         {
             var list = (from p in _appDbContext.WorldHeritages
                         orderby p.CreatedUtc descending
                         select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
             totalCount = _appDbContext.WorldHeritages.Count();
             return list.ToList();

         }

         public void Add(WorldHeritage wh)
         {
             _appDbContext.WorldHeritages.Add(wh);
             _appDbContext.SaveChanges();
         }

         public void Update(WorldHeritage wh)
         {
             _appDbContext.Entry(wh).State = EntityState.Modified;
             _appDbContext.SaveChanges();
         }

         public void Delete(int id)
         {
             var wh = _appDbContext.WorldHeritages.Find(id);

             if (wh == null)
             {
                 return;
             }

             _appDbContext.WorldHeritages.Remove(wh);
             _appDbContext.SaveChanges();
         }

         public WorldHeritage Get(int id)
         {
             return _appDbContext.WorldHeritages.Find(id);
         }

    }
}
