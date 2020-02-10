using Microsoft.EntityFrameworkCore;

namespace Movie.Repositories
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Models.Genre> Genre { get; set; }
        public virtual DbSet<Models.Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Genre>().ToTable("Genre");
            modelBuilder.Entity<Models.Genre>().HasKey(p => p.Id);
            modelBuilder.Entity<Models.Genre>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Genre>().Property(p => p.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Models.Genre>().HasMany(p => p.Movies).WithOne(p => p.Genre).HasForeignKey(p => p.GenreId);

            modelBuilder.Entity<Models.Genre>().HasData
            (
                new Models.Genre { Id = 101, Name = "Action" }, // Id set manually due to in-memory provider
                new Models.Genre { Id = 102, Name = "Adventure" },
                new Models.Genre { Id = 103, Name = "Comedy" },
                new Models.Genre { Id = 104, Name = "Crime" },
                new Models.Genre { Id = 105, Name = "Drama" },
                new Models.Genre { Id = 106, Name = "Fiction" },
                new Models.Genre { Id = 107, Name = "Horror" },
                new Models.Genre { Id = 108, Name = "Romance" },
                new Models.Genre { Id = 109, Name = "Thriller" },
                new Models.Genre { Id = 110, Name = "Animation" }
            );

            modelBuilder.Entity<Models.Movie>().ToTable("Movie");
            modelBuilder.Entity<Models.Movie>().HasKey(p => p.Id);
            modelBuilder.Entity<Models.Movie>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Movie>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Models.Movie>().Property(p => p.Slug).HasMaxLength(500);
            modelBuilder.Entity<Models.Movie>().Property(p => p.Code).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Models.Movie>().HasData
            (
                new Models.Movie { Id = 101, Name = "Avengers Endgame", Code = "AAA", Slug = "movie-101", GenreId = 101 }, // Id set manually due to in-memory provider
                new Models.Movie { Id = 102, Name = "The Witcher", Code = "BBB", Slug = "movie-102", GenreId = 107 },
                new Models.Movie { Id = 103, Name = "The Good Place", Code = "CCC", Slug = "movie-103", GenreId = 108 },
                new Models.Movie { Id = 104, Name = "Friends", Code = "DDD", Slug = "movie-104", GenreId = 101 },
                new Models.Movie { Id = 105, Name = "The Good Place", Code = "EEE", Slug = "movie-105", GenreId = 105 },
                new Models.Movie { Id = 106, Name = "The Lion King", Code = "FFF", Slug = "movie-106", GenreId = 110 },
                new Models.Movie { Id = 107, Name = "The Witcher", Code = "GGG", Slug = "movie-107", GenreId = 101 },
                new Models.Movie { Id = 108, Name = "Avengers Endgame", Code = "HHH", Slug = "movie-108", GenreId = 102 },
                new Models.Movie { Id = 109, Name = "The Witcher", Code = "III", Slug = "movie-109", GenreId = 107 },
                new Models.Movie { Id = 110, Name = "The Outsider", Code = "JJJ", Slug = "movie-110", GenreId = 104 }
            );
        }
    }
}
