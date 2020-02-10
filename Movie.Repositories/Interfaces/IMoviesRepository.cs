using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Repositories
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Models.Movie>> ListAsync();
        
        Task AddAsync(Models.Movie movie);
        
        Task<Models.Movie> FindByIdAsync(int id);
        
        void Update(Models.Movie movie);
        
        void Remove(Models.Movie movie);
    }
}
