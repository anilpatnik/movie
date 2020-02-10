namespace Movie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Code { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
