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
        IEnumerable<WorldHeritage> List(string search, int pageIndex, int pageSize, ref int totalCount);

        IEnumerable<WorldHeritage> Release(string search, int pageIndex, int pageSize, ref int totalCount);
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
        IEnumerable<WorldHeritage> My(int id, int pageIndex, int pageSize, ref int totalCount);

        WorldHeritage FindByCode(string code);

        ApiResponse<PagerInfoResponse<WorldHeritage>> NewSearch(string key, string firstLevelOfArtClassification,
            string secondLevelOfEthnicGroup, string type, int pageSize, int pageIndex);
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

            _appDbContext.Entry(wh).State = EntityState.Added;
            _appDbContext.SaveChanges();
        }

        public void Update(WorldHeritage wh)
        {
            _appDbContext.Set<WorldHeritage>().Attach(wh);
            _appDbContext.Entry<WorldHeritage>(wh).State = System.Data.Entity.EntityState.Modified;

            //_appDbContext.Entry(wh).State = EntityState.Modified;
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
            return _appDbContext.WorldHeritages.FirstOrDefault(x => x.WorldHeritageId == id);
        }

        public void Import(List<WorldHeritage> list)
        {
            //_appDbContext.BulkInsert(list);
            //_appDbContext.BulkSaveChanges();
            _appDbContext.WorldHeritages.AddRange(list);
            _appDbContext.SaveChanges();
        }

        public ApiResponse<PagerInfoResponse<WorldHeritage>> Search(int pageIndex, int pageSize, string firstlevel,
            string dataformat, string nation,
            string municipalities, string title)
        {
            return null;
        }

        public ApiResponse<PagerInfoResponse<WorldHeritage>> NewSearch(string key, string firstLevelOfArtClassification,
            string secondLevelOfEthnicGroup, string type, int pageSize, int pageIndex)
        {
            var result = new ApiResponse<PagerInfoResponse<WorldHeritage>>();
            result.Data = new PagerInfoResponse<WorldHeritage>();
            result.Data.Items = new List<WorldHeritage>();
            result.Data.Size = pageSize;
            result.Data.Index = pageIndex;
            try
            {
                IQueryable<WorldHeritage> list = null;
                var totalCount = 0;

                //没有任何 条件 取 全部 第一页
                if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(firstLevelOfArtClassification)
                    && string.IsNullOrEmpty(secondLevelOfEthnicGroup)
                    && string.IsNullOrEmpty(type))
                {
                    list = (from p in _appDbContext.WorldHeritages
                            where p.IsEffect == 1 && p.IsShow == 1
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    totalCount = _appDbContext.WorldHeritages.Count(x => x.IsEffect == 1 && x.IsShow == 1);
                    result.Data.Items = list.ToList();
                    result.Data.TotalCount = totalCount;
                    result.IsSuccessful = true;
                    result.StatusCode = StatusCode.Success;
                    return result;
                }
                else
                {
                    list = (from p in _appDbContext.WorldHeritages
                            where p.IsEffect == 1 && p.IsShow == 1
                            orderby p.CreatedUtc descending
                            select p);

                    if (!string.IsNullOrEmpty(key))
                    {
                        //正题名 社会属性一级分类 组成要素一级分类 艺术流派 表演形式 曲牌名 板式 关键字 类型 人物/组织名称
                        list = list.Where(p => (p.TitleProper.Contains(key) || p.FirstLevelOfSocialAttributes.Contains(key) ||
                                       p.FirstLevelOfElements.Contains(key) || p.ArtSchool.Contains(key) ||
                                       p.PerformingForm.Contains(key) || p.SongPattern.Contains(key) || p.BeatsPattern.Contains(key) ||
                                       p.Name.Contains(key) || p.Keywords.Contains(key) || p.Type.Contains(key)));
                    }
                    if (!string.IsNullOrEmpty(firstLevelOfArtClassification))
                    {
                        list = list.Where(p => p.FirstLevelOfArtClassification.Contains(firstLevelOfArtClassification));
                    }
                    if (!string.IsNullOrEmpty(secondLevelOfEthnicGroup))
                    {
                        if (secondLevelOfEthnicGroup == "少数民族")
                        {
                            list = list.Where(p => p.FirstLevelOfEthnicGroup.Contains(secondLevelOfEthnicGroup));
                        }
                        else
                            list = list.Where(p => p.SecondLevelOfEthnicGroup.Contains(secondLevelOfEthnicGroup));
                    }
                    if (!string.IsNullOrEmpty(type))
                    {
                        list = list.Where(p => p.Type.Contains(type));
                    }
                }

                totalCount = list.Count();
                list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);//最后在分页
                result.Data.Items = list.ToList();
                result.Data.TotalCount = totalCount;
                result.IsSuccessful = true;
                result.StatusCode = StatusCode.Success;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = "数据错误！";
                result.StatusCode = StatusCode.ClientError;
                return result;
            }
        }

        //public ApiResponse<PagerInfoResponse<WorldHeritage>> Search(int pageIndex, int pageSize, string firstlevel, string dataformat, string nation,
        //    string municipalities, string title)
        //{
        //    var result = new ApiResponse<PagerInfoResponse<WorldHeritage>>();
        //    result.Data = new PagerInfoResponse<WorldHeritage>();
        //    result.Data.Items = new List<WorldHeritage>();
        //    result.Data.Size = pageSize;
        //    result.Data.Index = pageIndex;

        //    try
        //    {
        //        IQueryable<WorldHeritage> list;
        //        var totalCount = 0;
        //        if (string.IsNullOrEmpty(firstlevel) && string.IsNullOrEmpty(dataformat) && string.IsNullOrEmpty(nation) &&
        //            string.IsNullOrEmpty(municipalities) && string.IsNullOrEmpty(title))
        //        {
        //            //没有任何 条件 取 全部 第一页
        //            list = (from p in _appDbContext.WorldHeritages
        //                    where p.IsEffect == 1 && p.IsShow == 1
        //                    orderby p.CreatedUtc descending
        //                    select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //            totalCount = _appDbContext.WorldHeritages.Count(x => x.IsEffect == 1 && x.IsShow == 1);
        //            result.Data.Items = list.ToList();
        //            result.Data.TotalCount = totalCount;
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(firstlevel))
        //            {
        //                list = (from p in _appDbContext.WorldHeritages
        //                        where p.FirstLevel.Contains(firstlevel) && p.IsEffect == 1 && p.IsShow == 1
        //                        orderby p.CreatedUtc descending
        //                        select p).AsQueryable();
        //                totalCount = list.Count(x => x.FirstLevel.Contains(firstlevel) && x.IsEffect == 1 && x.IsShow == 1);

        //                if (!string.IsNullOrEmpty(dataformat))
        //                {
        //                    list = list.Where(x => x.DataFormat.Contains(dataformat));
        //                    totalCount = list.Count(x => x.DataFormat.Contains(dataformat));
        //                }
        //                if (!string.IsNullOrEmpty(nation))
        //                {
        //                    list = list.Where(x => x.Nation.Contains(nation));
        //                    totalCount = list.Count(x => x.Nation.Contains(nation));
        //                }
        //                if (!string.IsNullOrEmpty(municipalities))
        //                {
        //                    list = list.Where(x => x.Municipalities.Contains(municipalities));
        //                    totalCount = list.Count(x => x.Municipalities.Contains(municipalities));
        //                }
        //                if (!string.IsNullOrEmpty(title))
        //                {
        //                    list = list.Where(x => (x.TitleProper.Contains(title) || x.FirstLevel.Contains(title) || x.SecondLevel.Contains(title) || x.Nation.Contains(title) || x.Successor.Contains(title) || x.Municipalities.Contains(title) || x.CountyLevelCity.Contains(title)) && x.IsEffect == 1);
        //                    totalCount = list.Count(x => x.TitleProper.Contains(title));
        //                }

        //                result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //                result.Data.TotalCount = totalCount;
        //                result.IsSuccessful = true;
        //                result.StatusCode = StatusCode.Success;
        //                return result;
        //            }
        //            if (!string.IsNullOrEmpty(dataformat))
        //            {
        //                list = (from p in _appDbContext.WorldHeritages
        //                        where p.DataFormat.Contains(dataformat) && p.IsEffect == 1 && p.IsShow == 1
        //                        orderby p.CreatedUtc descending
        //                        select p).AsQueryable();
        //                totalCount = list.Count(x => x.DataFormat.Contains(dataformat) && x.IsEffect == 1 && x.IsShow == 1);

        //                if (!string.IsNullOrEmpty(nation))
        //                {
        //                    list = list.Where(x => x.Nation.Contains(nation));
        //                    totalCount = list.Count(x => x.Nation.Contains(nation));
        //                }
        //                if (!string.IsNullOrEmpty(municipalities))
        //                {
        //                    list = list.Where(x => x.Municipalities.Contains(municipalities));
        //                    totalCount = list.Count(x => x.Municipalities.Contains(municipalities));
        //                }
        //                if (!string.IsNullOrEmpty(title))
        //                {
        //                    list = list.Where(x => (x.TitleProper.Contains(title) || x.FirstLevel.Contains(title) || x.SecondLevel.Contains(title) || x.Nation.Contains(title) || x.Successor.Contains(title) || x.Municipalities.Contains(title) || x.CountyLevelCity.Contains(title)));
        //                    totalCount = list.Count(x => x.TitleProper.Contains(title));
        //                }
        //                result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //                result.Data.TotalCount = totalCount;
        //                result.IsSuccessful = true;
        //                result.StatusCode = StatusCode.Success;
        //                return result;
        //            }


        //            if (!string.IsNullOrEmpty(nation))
        //            {
        //                list = (from p in _appDbContext.WorldHeritages
        //                        where p.Nation.Contains(nation) && p.IsEffect == 1 && p.IsShow == 1
        //                        orderby p.CreatedUtc descending
        //                        select p).AsQueryable();
        //                totalCount = list.Count(x => x.Nation.Contains(nation) && x.IsEffect == 1 && x.IsShow == 1);

        //                if (!string.IsNullOrEmpty(municipalities))
        //                {
        //                    list = list.Where(x => x.Municipalities.Contains(municipalities));
        //                    totalCount = list.Count(x => x.Municipalities.Contains(municipalities));
        //                }
        //                if (!string.IsNullOrEmpty(title))
        //                {
        //                    list = list.Where(x => (x.Title.Contains(title) || x.FirstLevel.Contains(title) || x.SecondLevel.Contains(title) || x.Nation.Contains(title) || x.Successor.Contains(title) || x.Municipalities.Contains(title) || x.CountyLevelCity.Contains(title)));
        //                    totalCount = list.Count(x => x.Title.Contains(title));
        //                }
        //                result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //                result.Data.TotalCount = totalCount;
        //                result.IsSuccessful = true;
        //                result.StatusCode = StatusCode.Success;
        //                return result;
        //            }
        //            if (!string.IsNullOrEmpty(municipalities))
        //            {
        //                list = (from p in _appDbContext.WorldHeritages
        //                        where p.Municipalities.Contains(municipalities) && p.IsEffect == 1 && p.IsShow == 1
        //                        orderby p.CreatedUtc descending
        //                        select p).AsQueryable();
        //                totalCount = list.Count(x => x.Municipalities.Contains(municipalities) && x.IsEffect == 1 && x.IsShow == 1);

        //                if (!string.IsNullOrEmpty(title))
        //                {
        //                    list = list.Where(x => (x.Title.Contains(title) || x.FirstLevel.Contains(title) || x.SecondLevel.Contains(title) || x.Nation.Contains(title) || x.Successor.Contains(title) || x.Municipalities.Contains(title) || x.CountyLevelCity.Contains(title)));
        //                    totalCount = list.Count(x => x.Title.Contains(title) && x.IsEffect == 1);
        //                }
        //                result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //                result.Data.TotalCount = totalCount;
        //                result.IsSuccessful = true;
        //                result.StatusCode = StatusCode.Success;
        //                return result;
        //            }
        //            if (!string.IsNullOrEmpty(title))
        //            {
        //                list = (from p in _appDbContext.WorldHeritages
        //                        where (p.Title.Contains(title) || p.FirstLevel.Contains(title) || p.SecondLevel.Contains(title) || p.Nation.Contains(title) || p.Successor.Contains(title) || p.Municipalities.Contains(title) || p.CountyLevelCity.Contains(title))
        //                        orderby p.CreatedUtc descending
        //                        select p).AsQueryable();
        //                totalCount = list.Count(x => (x.Title.Contains(title) || x.FirstLevel.Contains(title) || x.SecondLevel.Contains(title) || x.Nation.Contains(title) || x.Successor.Contains(title) || x.Municipalities.Contains(title) || x.CountyLevelCity.Contains(title)));

        //                result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //                result.Data.TotalCount = totalCount;
        //                result.IsSuccessful = true;
        //                result.StatusCode = StatusCode.Success;
        //                return result;
        //            }
        //            // result.Data.Items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //        }
        //        result.Message = "全部数据！";
        //        result.IsSuccessful = false;
        //        result.StatusCode = StatusCode.ClientError;
        //        return result;
        //    }
        //    catch
        //    {
        //        result.IsSuccessful = false;
        //        result.Message = "数据错误！";
        //        result.StatusCode = StatusCode.ClientError;
        //        return result;
        //    }
        //}


        public IEnumerable<WorldHeritage> My(int id, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.WorldHeritages
                        where p.User.ManagerId == id
                        orderby p.CreatedUtc descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.WorldHeritages.Count(x => x.User.ManagerId == id);
            return list.ToList();
        }

        public WorldHeritage FindByCode(string code)
        {
            return _appDbContext.WorldHeritages.FirstOrDefault(x => x.ArtificialId == code);
        }

        public IEnumerable<WorldHeritage> List(string search, int pageIndex, int pageSize, ref int totalCount)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = (from p in _appDbContext.WorldHeritages
                            where p.TitleProper.Contains(search)
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                totalCount = _appDbContext.WorldHeritages.Count(x => x.TitleProper.Contains(search));
                return list.ToList();
            }
            else
            {
                var list = (from p in _appDbContext.WorldHeritages
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                totalCount = _appDbContext.WorldHeritages.Count();
                return list.ToList();
            }
        }
        public IEnumerable<WorldHeritage> Release(string search, int pageIndex, int pageSize, ref int totalCount)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = (from p in _appDbContext.WorldHeritages
                            where p.IsEffect == 1 && (p.TitleProper.Contains(search) || p.Note.Contains(search))
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                totalCount = _appDbContext.WorldHeritages.Count(x => x.IsEffect == 1 && (x.TitleProper.Contains(search) || x.Note.Contains(search)));
                return list.ToList();
            }
            else
            {
                var list = (from p in _appDbContext.WorldHeritages
                            where p.IsEffect == 1
                            orderby p.CreatedUtc descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                totalCount = _appDbContext.WorldHeritages.Count(x => x.IsEffect == 1);
                return list.ToList();
            }
        }
    }
}
