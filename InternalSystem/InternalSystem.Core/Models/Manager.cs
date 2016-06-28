using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.Core.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Pass { get; set; }
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// 权限越小 权利越大 管理员1 超管 为0 用户2 发布者管理员3
        /// </summary>
        public int Authority { get; set; }

        /// <summary>
        /// 是否无效 0 有效 1 无效
        /// </summary>
        public int Invalid { get; set; }
    }
}
