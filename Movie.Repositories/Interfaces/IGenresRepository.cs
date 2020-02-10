using Movie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Repositories
{
    public interface IGenresRepository
    {
        Task<IEnumerable<Genre>> ListAsync();
        
        Task AddAsync(Genre genre);
        
        Task<Genre> FindByIdAsync(int id);
        
        void Update(Genre genre);
        
        void Remove(Genre genre);
    }
}
