using System.ComponentModel.DataAnnotations;

namespace Movie.Resources
{
    public class SaveMovieDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Slug { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}
