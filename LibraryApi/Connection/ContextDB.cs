using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Connection
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions options) : base(options) { }
        public DbSet<GenreBook> GenreBooks { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<HistoryRentalBook> HistoryRentalBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreBook>()
               .HasIndex(g => g.Name)
               .IsUnique();
        }
    }
}
