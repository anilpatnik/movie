using System.ComponentModel.DataAnnotations;

namespace Movie.Resources
{
    public class SaveGenreDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
