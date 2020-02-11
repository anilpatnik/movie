using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Repositories
{
    public class MoviesRepository : BaseRepository, IMoviesRepository
    {
        public MoviesRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Models.Movie>> ListAsync()
        {
            return await _context.Movie.Include(x => x.Genre).AsNoTracking().ToListAsync();
        }

        public async Task<Models.Movie> GetAsync(int id)
        {
            return await _context.Movie.Include(x => x.Genre).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task AddAsync(Models.Movie movie)
        {
            await _context.Movie.AddAsync(movie);
        }

        public async Task<Models.Movie> FindByIdAsync(int id)
        {
            return await _context.Movie.Include(p => p.Genre).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Models.Movie movie)
        {
            _context.Movie.Update(movie);
        }

        public void Remove(Models.Movie movie)
        {
            _context.Movie.Remove(movie);
        }
    }
}
