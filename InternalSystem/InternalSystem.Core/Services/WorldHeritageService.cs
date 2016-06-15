using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalSystem.Core.Models;
using InternalSystem.Core.Repositories;
using InternalSystem.Infrastructure;

using Z.EntityFramework.Extensions;
using Z.BulkOperations;

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

        void Import(List<WorldHeritage> list);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="firstlevel">分类 firstlevel</param>
        /// <param name="dataformat">类型 dataformat</param>
        /// <param name="nation">民族 nation</param>
        /// <param name="municipalities">地区 municipalities</param>
        /// <param name="title">关键字搜索 title</param>
        /// <returns></returns>
        ApiResponse<PagerInfoResponse<WorldHeritage>> Search(int pageIndex, int pageSize, string firstlevel, string dataformat, string nation, string municipalities, string title);
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

        public void Import(List<WorldHeritage> list)
        {
            _appDbContext.BulkInsert(list);
            _appDbContext.BulkSaveChanges();
        }

        public ApiResponse<PagerInfoResponse<WorldHeritage>> Search(int pageIndex, int pageSize, string firstlevel, string dataformat, string nation,
            string municipalities, string title)
        {
            var result = new ApiResponse<PagerInfoResponse<WorldHeritage>>();

            result.Data = new PagerInfoResponse<WorldHeritage>();
            result.Data.Items = new List<WorldHeritage>();

            result.Data.Size = pageSize;
            result.Data.Index = pageIndex;

            try
            {
                var list = (from p in _appDbContext.WorldHeritages
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var totalCount = _appDbContext.WorldHeritages.Count();
                var aa = list.Count();

                if (!string.IsNullOrEmpty(firstlevel))
                {
                    list = list.Where(x => x.FirstLevel.Contains(firstlevel));
                    totalCount = list.Count(x => x.FirstLevel.Contains(firstlevel));
                }
                if (!string.IsNullOrEmpty(dataformat))
                {
                    list = list.Where(x => x.DataFormat.Contains(dataformat));
                    totalCount = list.Count(x => x.DataFormat.Contains(dataformat)); 
                }
                if (!string.IsNullOrEmpty(nation))
                {
                    list = list.Where(x => x.Nation.Contains(nation));
                    totalCount = list.Count(x => x.Nation.Contains(nation));
                }
                if (!string.IsNullOrEmpty(municipalities))
                {
                    list = list.Where(x => x.Municipalities.Contains(municipalities));
                    totalCount = list.Count(x => x.Municipalities.Contains(municipalities));
                }
                if (!string.IsNullOrEmpty(title))
                {
                    list = list.Where(x => x.Title.Contains(title));
                    totalCount = list.Count(x => x.Title.Contains(title));
                }

                result.Data.Items = list.ToList();
                result.Data.TotalCount = totalCount;
                result.IsSuccessful = true;
                result.StatusCode = StatusCode.Success;
                return result;
            }
            catch
            {
                result.IsSuccessful = false;
                result.Message = "数据错误！";
                result.StatusCode = StatusCode.ClientError;
                return result;
            }
        }
    }
}
