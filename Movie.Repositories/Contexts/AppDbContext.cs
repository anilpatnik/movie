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
                new Models.Movie { Id = 101, Name = "Avengers Endgame", Code = "TT4154796", Slug = "https://www.imdumb.com/title/tt4154796", GenreId = 101 }, // Id set manually due to in-memory provider
                new Models.Movie { Id = 102, Name = "The Witcher", Code = "TT4154334", Slug = "https://www.imdumb.com/title/tt4154334", GenreId = 107 },
                new Models.Movie { Id = 103, Name = "The Good Place", Code = "TT41456896", Slug = "https://www.imdumb.com/title/tt41456896", GenreId = 108 },
                new Models.Movie { Id = 104, Name = "Friends", Code = "TT0108778", Slug = "https://www.imdumb.com/title/tt0108778", GenreId = 101 },
                new Models.Movie { Id = 105, Name = "The Good Place", Code = "TT22244796", Slug = "https://www.imdumb.com/title/tt22244796", GenreId = 105 },
                new Models.Movie { Id = 106, Name = "The Lion King", Code = "TT12455796", Slug = "https://www.imdumb.com/title/tt12455796", GenreId = 110 },
                new Models.Movie { Id = 107, Name = "The Witcher", Code = "TT4176696", Slug = "https://www.imdumb.com/title/tt4176696", GenreId = 101 },
                new Models.Movie { Id = 108, Name = "Avengers Endgame", Code = "TT9974596", Slug = "https://www.imdumb.com/title/tt9974596", GenreId = 102 },
                new Models.Movie { Id = 109, Name = "The Witcher", Code = "TT6676096", Slug = "https://www.imdumb.com/title/tt6676096", GenreId = 107 },
                new Models.Movie { Id = 110, Name = "The Outsider", Code = "TT40076596", Slug = "https://www.imdumb.com/title/tt40076596", GenreId = 104 }
            );
        }
    }
}
