using System.Threading.Tasks;

namespace Movie.Repositories
{
    public class SaveRepository : BaseRepository, ISaveRepository
    {        
        public SaveRepository(AppDbContext context) : base(context) { }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }        
    }
}
