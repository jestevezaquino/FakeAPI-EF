using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevColaboration.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace DevColaboration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Geo> Geos { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
