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
        /// 
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="firstlevel">分类</param>
        /// <param name="secondlevel"></param>
        /// <param name="dataformat"></param>
        /// <param name="nation">民族</param>
        /// <param name="municipalities">地区</param>
        /// <param name="countylevelcity"></param>
        /// <param name="title">关键字搜索</param>
        /// <param name="successor"></param>
        /// <returns></returns>
        ApiResponse<PagerInfoResponse<WorldHeritage>> Search(int pageIndex, int pageSize, string firstlevel, string secondlevel, string dataformat, string nation, string municipalities, string countylevelcity, string title, string successor);
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
            return _appDbContext.WorldHeritages.Where(x => x.IsEffect == 1).OrderByDescending(x => x.CreatedUtc).ToList();
        }

        public IEnumerable<WorldHeritage> List(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.WorldHeritages
                        where p.IsEffect == 1
                        orderby p.CreatedUtc descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.WorldHeritages.Count(x => x.IsEffect == 1);
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
            return _appDbContext.WorldHeritages.FirstOrDefault(x => x.IsEffect == 1 && x.WorldHeritageId == id);
        }

        public void Import(List<WorldHeritage> list)
        {
            _appDbContext.BulkInsert(list);
            _appDbContext.BulkSaveChanges();
        }

        public ApiResponse<PagerInfoResponse<WorldHeritage>> Search(int pageIndex, int pageSize, string firstlevel, string secondlevel, string dataformat, string nation, string municipalities, string countylevelcity, string title, string successor)
        {
            var result = new ApiResponse<PagerInfoResponse<WorldHeritage>>();
            result.Data = new PagerInfoResponse<WorldHeritage>();
            result.Data.Items = new List<WorldHeritage>();
            result.Data.Size = pageSize;
            result.Data.Index = pageIndex;

            try
            {
                IQueryable<WorldHeritage> list;
                var totalCount = 0;
                if (string.IsNullOrEmpty(firstlevel) && string.IsNullOrEmpty(dataformat) && string.IsNullOrEmpty(nation) &&
                    string.IsNullOrEmpty(municipalities) && string.IsNullOrEmpty(title) && string.IsNullOrEmpty(secondlevel) && 
                    string.IsNullOrEmpty(countylevelcity) && string.IsNullOrEmpty(successor))
                {
                    //没有任何 条件 取 全部 第一页
                    list = (from p in _appDbContext.WorldHeritages
                            where p.IsEffect == 1
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    totalCount = _appDbContext.WorldHeritages.Count(x => x.IsEffect == 1);
                    result.Data.Items = list.ToList();
                    result.Data.TotalCount = totalCount;
                }
                else
                {
                    //countylevelcity successor secondlevel
                    if (!string.IsNullOrEmpty(firstlevel))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.FirstLevel.Contains(firstlevel) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.FirstLevel.Contains(firstlevel) && x.IsEffect == 1);

                        if (!string.IsNullOrEmpty(secondlevel))
                        {
                            list = list.Where(x => x.SecondLevel.Contains(secondlevel));
                            totalCount = list.Count(x => x.SecondLevel.Contains(secondlevel));
                        }
                        if (!string.IsNullOrEmpty(countylevelcity))
                        {
                            list = list.Where(x => x.CountyLevelCity.Contains(countylevelcity));
                            totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity));
                        }
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        if (!string.IsNullOrEmpty(dataformat))
                        {
                            list = list.Where(x => x.DataFormat.Contains(dataformat));
                            totalCount = list.Count(x => x.DataFormat.Contains(dataformat));
                        }
                        if (!string.IsNullOrEmpty(nation))
                        {
                            list = list.Where(x => x.Nation.Contains(nation) );
                            totalCount = list.Count(x => x.Nation.Contains(nation) );
                        }
                        if (!string.IsNullOrEmpty(municipalities))
                        {
                            list = list.Where(x => x.Municipalities.Contains(municipalities) );
                            totalCount = list.Count(x => x.Municipalities.Contains(municipalities));
                        }
                        if (!string.IsNullOrEmpty(title))
                        {
                            list = list.Where(x => x.Title.Contains(title));
                            totalCount = list.Count(x => x.Title.Contains(title));
                        }

                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }
                    if (!string.IsNullOrEmpty(dataformat))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.DataFormat.Contains(dataformat) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.DataFormat.Contains(dataformat) && x.IsEffect == 1);

                        if (!string.IsNullOrEmpty(secondlevel))
                        {
                            list = list.Where(x => x.SecondLevel.Contains(secondlevel));
                            totalCount = list.Count(x => x.SecondLevel.Contains(secondlevel));
                        }
                        if (!string.IsNullOrEmpty(countylevelcity))
                        {
                            list = list.Where(x => x.CountyLevelCity.Contains(countylevelcity));
                            totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity));
                        }
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        if (!string.IsNullOrEmpty(nation))
                        {
                            list = list.Where(x => x.Nation.Contains(nation) && x.IsEffect == 1);
                            totalCount = list.Count(x => x.Nation.Contains(nation) && x.IsEffect == 1);
                        }
                        if (!string.IsNullOrEmpty(municipalities))
                        {
                            list = list.Where(x => x.Municipalities.Contains(municipalities) && x.IsEffect == 1);
                            totalCount = list.Count(x => x.Municipalities.Contains(municipalities) && x.IsEffect == 1);
                        }
                        if (!string.IsNullOrEmpty(title))
                        {
                            list = list.Where(x => x.Title.Contains(title) && x.IsEffect == 1);
                            totalCount = list.Count(x => x.Title.Contains(title) && x.IsEffect == 1);
                        }
                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }


                    if (!string.IsNullOrEmpty(nation))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.Nation.Contains(nation) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.Nation.Contains(nation) && x.IsEffect == 1);

                        if (!string.IsNullOrEmpty(secondlevel))
                        {
                            list = list.Where(x => x.SecondLevel.Contains(secondlevel));
                            totalCount = list.Count(x => x.SecondLevel.Contains(secondlevel));
                        }
                        if (!string.IsNullOrEmpty(countylevelcity))
                        {
                            list = list.Where(x => x.CountyLevelCity.Contains(countylevelcity));
                            totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity));
                        }
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        if (!string.IsNullOrEmpty(municipalities))
                        {
                            list = list.Where(x => x.Municipalities.Contains(municipalities) && x.IsEffect == 1);
                            totalCount = list.Count(x => x.Municipalities.Contains(municipalities) && x.IsEffect == 1);
                        }
                        if (!string.IsNullOrEmpty(title))
                        {
                            list = list.Where(x => x.Title.Contains(title) && x.IsEffect == 1);
                            totalCount = list.Count(x => x.Title.Contains(title) && x.IsEffect == 1);
                        }
                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }
                    if (!string.IsNullOrEmpty(municipalities))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.Municipalities.Contains(municipalities) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.Municipalities.Contains(municipalities) && x.IsEffect == 1);

                        if (!string.IsNullOrEmpty(secondlevel))
                        {
                            list = list.Where(x => x.SecondLevel.Contains(secondlevel));
                            totalCount = list.Count(x => x.SecondLevel.Contains(secondlevel));
                        }
                        if (!string.IsNullOrEmpty(countylevelcity))
                        {
                            list = list.Where(x => x.CountyLevelCity.Contains(countylevelcity));
                            totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity));
                        }
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        if (!string.IsNullOrEmpty(title))
                        {
                            list = list.Where(x => x.Title.Contains(title) && x.IsEffect == 1);
                            totalCount = list.Count(x => x.Title.Contains(title) && x.IsEffect == 1);
                        }
                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }
                    if (!string.IsNullOrEmpty(title))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.Title.Contains(title) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.Title.Contains(title) && x.IsEffect == 1);
                        if (!string.IsNullOrEmpty(secondlevel))
                        {
                            list = list.Where(x => x.SecondLevel.Contains(secondlevel));
                            totalCount = list.Count(x => x.SecondLevel.Contains(secondlevel));
                        }
                        if (!string.IsNullOrEmpty(countylevelcity))
                        {
                            list = list.Where(x => x.CountyLevelCity.Contains(countylevelcity));
                            totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity));
                        }
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }

                    if (!string.IsNullOrEmpty(secondlevel))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.SecondLevel.Contains(secondlevel) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.SecondLevel.Contains(secondlevel) && x.IsEffect == 1);
                        if (!string.IsNullOrEmpty(countylevelcity))
                        {
                            list = list.Where(x => x.CountyLevelCity.Contains(countylevelcity));
                            totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity));
                        }
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }

                    if (!string.IsNullOrEmpty(countylevelcity))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.CountyLevelCity.Contains(countylevelcity) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.CountyLevelCity.Contains(countylevelcity) && x.IsEffect == 1);
                        if (!string.IsNullOrEmpty(successor))
                        {
                            list = list.Where(x => x.Successor.Contains(successor));
                            totalCount = list.Count(x => x.Successor.Contains(successor));
                        }

                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }
                    if (!string.IsNullOrEmpty(successor))
                    {
                        list = (from p in _appDbContext.WorldHeritages
                                where p.Successor.Contains(successor) && p.IsEffect == 1
                                orderby p.CreatedUtc descending
                                select p).AsQueryable();
                        totalCount = list.Count(x => x.Successor.Contains(successor) && x.IsEffect == 1);
                        result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        result.Data.TotalCount = totalCount;
                        result.IsSuccessful = true;
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }

                    //result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                result.Message = "全部数据！";
                result.IsSuccessful = false;
                result.StatusCode = StatusCode.ClientError;
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
