using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Models.Movie>> ListAsync();

        Task<Models.Movie> GetAsync(int id);

        Task<MovieResponse> SaveAsync(Models.Movie movie);
        
        Task<MovieResponse> UpdateAsync(int id, Models.Movie movie);
        
        Task<MovieResponse> DeleteAsync(int id);
    }
}
