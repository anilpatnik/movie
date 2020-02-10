using System.ComponentModel.DataAnnotations;

namespace Movie.Resources
{
    public class MovieDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }                
        
        public string Slug { get; set; }
        
        public string Code { get; set; }
        
        public GenreDto Genre { get; set; }
    }
}
