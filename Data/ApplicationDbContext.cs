using Microsoft.EntityFrameworkCore;

namespace GraphQLProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasIndex(a => a.UserName)
                .IsUnique();

            // Many-to-many: Book <-> Author
            modelBuilder
                .Entity<BookAuthor>()
                .HasKey(ca => new { ca.BookId, ca.AuthorId });

            // Many-to-many: Book <-> Author
            modelBuilder
                .Entity<BookPublisher>()
                .HasKey(ca => new { ca.BookId, ca.PublisherId });

        }

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<Author> Authors { get; set; } = default!;

        public DbSet<Publisher> Publishers { get; set; } = default!;


    }
}