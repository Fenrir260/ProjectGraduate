using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class ContextDb : DbContext
    {
        public DbSet<Goods> GoodsTable { get; set; }   
        public DbSet<Users> UsersTable { get; set; }


        public ContextDb(DbContextOptions<ContextDb> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
