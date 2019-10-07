using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using spaceBoundAPI.Models;

namespace spaceBoundAPI.Models
{
    public class spaceBoundAPIContext : DbContext
    {
        public spaceBoundAPIContext (DbContextOptions<spaceBoundAPIContext> options)
            : base(options)
        {
        }

        public DbSet<spaceBoundAPI.Models.Orders> Orders { get; set; }

        public DbSet<spaceBoundAPI.Models.Currencies> Currencies { get; set; }
    }
}
