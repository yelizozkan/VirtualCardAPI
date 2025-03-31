using Microsoft.EntityFrameworkCore;
using VirtualCardAPI.Models;

namespace VirtualCardAPI.Context
{
    public class VirtualCardDbContext : DbContext
    {
        public VirtualCardDbContext(DbContextOptions<VirtualCardDbContext> options) : base(options) { }

        public DbSet<VirtualCard> VirtualCards { get; set; }
    }

}



