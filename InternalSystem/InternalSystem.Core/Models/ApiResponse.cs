using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace InternalSystem.Core.Models
{
    public interface IPagerInfo
    {
        /// <summary>
        /// 页码(从1起始)
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPaged { get; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount { get; }


    }

    [DataContract]
    public class PagerInfoResponse : IPagerInfo
    {
        /// <summary>
        /// 索引
        /// </summary>
        [DataMember(Name = "pageindex")]
        public int Index { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        [DataMember(Name = "pagesize")]
        public int Size { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        [DataMember(Name = "totalcount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember(Name = "totalpaged")]
        public int TotalPaged
        {
            get
            {
                if (this.Size <= 0)
                    return 0;
                return (int)Math.Ceiling(this.TotalCount / (double)this.Size);
            }
            set { }
        }

        /// <summary>
        /// 是否存在分页
        /// </summary>
        [DataMember(Name = "ispaged")]
        public bool IsPaged
        {
            get
            {
                return Size < TotalCount;
            }
            set { }
        }
    }



    [DataContract]
    public class PagerInfoResponse<T> : PagerInfoResponse
    {
        [DataMember(Name = "items")]
        public List<T> Items
        {
            get { return _items; }

            set
            {
                if (value == null)
                {
                    _items = new List<T>();
                }
                else
                {
                    _items = value;
                }
            }
        }

        public List<T> _items;
    }

    public class ApiResponse<T> : ApiResponse
    {
        [DataMember(Name = "data")]
        public T Data { get; set; }
    }

    public interface IResponse
    {

    }

    [DataContract(Name = "result")]
    public class ApiResponse : IResponse
    {
        [DataMember(Name = "isSuccessful", Order = 1)]
        public bool IsSuccessful { get; set; }

        [DataMember(Name = "message", Order = 3)]
        public string Message { get; set; }

        [DataMember(Name = "statusCode", Order = 2)]
        public StatusCode StatusCode { get; set; }
    }

    [DataContract(Name = "statusCode")]
    public enum StatusCode
    {
        [EnumMember]
        UnKnow = 0,
        [EnumMember]
        Success = 200,
        [EnumMember]
        ClientError = 400,
        [EnumMember]
        Unauthorized = 401,
        [EnumMember]
        NotFound = 404,
        [EnumMember]
        RequestTimeout = 408,
        [EnumMember]
        BlacklistUser = 409,
        [EnumMember]
        InternalServerError = 500,
        [EnumMember]
        ServiceUnavailable = 503,
    }
}
