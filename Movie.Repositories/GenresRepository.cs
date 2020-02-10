using Microsoft.EntityFrameworkCore;
using Movie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Repositories
{
    public class GenresRepository : BaseRepository, IGenresRepository
    {
        public GenresRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Genre>> ListAsync()
        {
            return await _context.Genre.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Genre genre)
        {
            await _context.Genre.AddAsync(genre);
        }

        public async Task<Genre> FindByIdAsync(int id)
        {
            return await _context.Genre.FindAsync(id);
        }

        public void Update(Genre genre)
        {
            _context.Genre.Update(genre);
        }

        public void Remove(Genre genre)
        {
            _context.Genre.Remove(genre);
        }
    }
}
