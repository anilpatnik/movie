using Movie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> ListAsync();
        
        Task<GenreResponse> SaveAsync(Genre genre);
        
        Task<GenreResponse> UpdateAsync(int id, Genre genre);
        
        Task<GenreResponse> DeleteAsync(int id);
    }
}
