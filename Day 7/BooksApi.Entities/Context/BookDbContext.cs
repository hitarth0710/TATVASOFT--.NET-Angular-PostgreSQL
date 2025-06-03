using BooksApi.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Entities.Context
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<User>  Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User() 
            {
               Id = 1,
               Name = "Hitarth",
               Email = "hitarthsoni947@gmail.com",
               Role = "Admin",
               Password = "root"               
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
