using LibraryProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Infrastructure.EntityFramework
{
    public class LibraryProjectDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public LibraryProjectDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Title).IsRequired();
                b.Property(x => x.Author).IsRequired();
            });

            modelBuilder.Entity<Loan>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Status).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
