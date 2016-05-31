using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalSystem.Core.Models;
using InternalSystem.Infrastructure;

namespace InternalSystem.Core.Repositories
{
    public class AppDbContext : DbContext, IDependencyPerRequest
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Article> Articles { get; set; }
    }
}
